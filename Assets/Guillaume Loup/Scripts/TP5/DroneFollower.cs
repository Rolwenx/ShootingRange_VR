using UnityEngine;
using System.Collections.Generic;

// Code fait avec chatgpt 

public class DroneFollower : MonoBehaviour
{
    // true = normal mode | false = cautious mode
    public bool currentMode = false;

    [Header("Target")]
    public Transform followAnchor;

    [Header("Sampling")]
    public int directionsCount = 32;
    public float coneAngle = 60f;

    [Header("Collision")]
    public float lookAheadBase = 2f;
    public float lookAheadSpeedFactor = 1f;
    public float clearanceRadius = 0.3f;
    public LayerMask obstacleMask;

    [Header("Motion")]
    public float maxSpeed = 3f;
    public float smoothing = 8f;

    Vector3 currentVelocity;
    Vector3 lastDirection;

    [Header("B3 Scoring Weights")]
    public float wFollow = 1.0f;
    public float wDynamic = 0.5f;
    public float wSafety = 2.0f;

    [Header("B4 Stabilisation")]
    public float hysteresisFactor = 0.9f; 
    public float directionSmoothing = 6f;

    float lastBestScore = float.MaxValue;

    [Header("B5 Fallback")]
    public float cautiousSpeedFactor = 0.3f;
    public float hoverDuration = 0.5f;

    float hoverTimer = 0f;
    bool isHovering = false;

    [Header("Follow Constraint")]
    public float desiredDistance = 2.0f;     // distance voulue au joueur
    public float followStrength = 2.5f;       // force de rappel


    void Update()
    {

        // B5 — gestion du hover temporaire
        if (isHovering)
        {
            hoverTimer -= Time.deltaTime;
            if (hoverTimer <= 0f)
            {
                isHovering = false; // on retente normalement
            }
        }
        if (!followAnchor) return;

        Vector3 toAnchor = followAnchor.position - transform.position;
        Vector3 baseDir = toAnchor.normalized;

        List<Vector3> validDirs = new List<Vector3>();

        float speed = currentVelocity.magnitude;
        float lookAhead = lookAheadBase + lookAheadSpeedFactor * speed;
        Vector3 d0 = (followAnchor.position - transform.position).normalized;

        // B1 : génération des directions candidates dans le cône
        List<Vector3> candidates = GenerateConeDirections(d0, directionsCount, coneAngle);

        // B2 : filtrage par SphereCast
        for (int i = 0; i < candidates.Count; i++)
        {
            Vector3 dir = candidates[i];

            if (!Physics.SphereCast(
                    transform.position,
                    clearanceRadius,
                    dir,
                    out _,
                    lookAhead,
                    obstacleMask))
            {
                validDirs.Add(dir);
            }
        }

        if (validDirs.Count < 2)
        {
            // B5 — fallback
            currentMode = false; // cautious

            // démarrer le hover si pas déjà en cours
            if (!isHovering)
            {
                isHovering = true;
                hoverTimer = hoverDuration;
            }

            // pendant le hover : ralentir fortement
            currentVelocity = Vector3.Lerp(
                currentVelocity,
                Vector3.zero,
                Time.deltaTime * 5f);

            transform.position += currentVelocity * Time.deltaTime;
            return;
        }
        currentMode = validDirs.Count < directionsCount * 0.8f
    ? false
    : true;

        float bestScore;
        Vector3 candidateDir = ChooseBestDirection(
            validDirs,
            baseDir,
            lookAhead,
            out bestScore);

        // B4 — hystérésis : on ne change que si gain significatif
        if (bestScore < lastBestScore * hysteresisFactor)
        {
            lastDirection = candidateDir;
            lastBestScore = bestScore;
        }

        if (lastDirection == Vector3.zero)
        {
            lastDirection = candidateDir;
        }

        Vector3 smoothedDir = Vector3.Lerp(
            lastDirection,
            candidateDir,
            Time.deltaTime * directionSmoothing);
        float speedFactor = currentMode ? 1f : cautiousSpeedFactor;
                    float distToTarget = Vector3.Distance(transform.position, followAnchor.position);
            float distanceError = distToTarget - desiredDistance;
        Vector3 followForce = baseDir * distanceError * followStrength;

currentVelocity =
    (smoothedDir * maxSpeed + followForce)
    * speedFactor;
        transform.position += currentVelocity * Time.deltaTime;
    }

    Vector3 ChooseBestDirection(
    List<Vector3> dirs,
    Vector3 baseDir,
    float lookAhead,
    out float bestScore)
    {
        bestScore = float.MaxValue;
        Vector3 bestDir = baseDir;

        foreach (var dir in dirs)
        {
            float score = ScoreDirection(
                dir,
                baseDir,
                lastDirection,
                lookAhead);

            if (score < bestScore)
            {
                bestScore = score;
                bestDir = dir;
            }
        }

        return bestDir;
    }

    List<Vector3> GenerateConeDirections(Vector3 d0, int N, float coneAngleDeg)
    {
        List<Vector3> dirs = new List<Vector3>(N);

        // construire une base orthonormée autour de d0
        Vector3 up = Vector3.up;
        if (Vector3.Dot(up, d0) > 0.95f) up = Vector3.right;   // éviter colinéarité
        Vector3 right = Vector3.Normalize(Vector3.Cross(up, d0));
        Vector3 forward = Vector3.Normalize(Vector3.Cross(d0, right)); // second axe

        float coneRad = coneAngleDeg * Mathf.Deg2Rad;

        // on inclut d0 lui-même
        dirs.Add(d0);

        // Répartition en spirale (azimut) + variation d'angle (élévation)
        for (int i = 1; i < N; i++)
        {
            float t = (float)i / (N - 1);          // 0..1
            float theta = t * coneRad;             // angle depuis d0
            float phi = i * 2.39996323f;           // golden angle (en radians)

            float sinTheta = Mathf.Sin(theta);
            float cosTheta = Mathf.Cos(theta);

            Vector3 dir =
                cosTheta * d0 +
                sinTheta * (Mathf.Cos(phi) * right + Mathf.Sin(phi) * forward);

            dirs.Add(dir.normalized);
        }

        return dirs;
    }

    float ScoreDirection(
    Vector3 dir,
    Vector3 baseDir,
    Vector3 lastDir,
    float lookAhead)
    {
        float score = 0f;

        // Suivi : rester proche de la direction vers la cible
        float followScore = Vector3.Angle(dir, baseDir);
        score += wFollow * followScore;

        // Dynamique : éviter les changements brusques
        if (lastDir != Vector3.zero)
        {
            float dynamicScore = Vector3.Angle(dir, lastDir);
            score += wDynamic * dynamicScore;
        }

        // Sécurité soft : pénalité si obstacle proche (frôlement)
        if (Physics.SphereCast(
            transform.position,
            clearanceRadius,
            dir,
            out RaycastHit hit,
            lookAhead,
            obstacleMask))
        {
            float proximity = 1f - (hit.distance / lookAhead); // 0 loin → 1 proche
            score += wSafety * proximity * 20f;
        }

        return score;
    }
}
