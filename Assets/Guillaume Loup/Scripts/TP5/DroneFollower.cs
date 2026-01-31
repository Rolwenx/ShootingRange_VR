using UnityEngine;
using System.Collections.Generic;

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


    void Update()
    {

        if (!followAnchor) return;

        Vector3 toAnchor = followAnchor.position - transform.position;
        Vector3 baseDir = toAnchor.normalized;

        List<Vector3> validDirs = new List<Vector3>();

        float speed = currentVelocity.magnitude;
        float lookAhead = lookAheadBase + lookAheadSpeedFactor * speed;

        for (int i = 0; i < directionsCount; i++)
        {
            Vector3 dir = SampleDirection(baseDir);
            if (!Physics.SphereCast(transform.position, clearanceRadius, dir, out _, lookAhead, obstacleMask))
            {
                validDirs.Add(dir);
            }
        }

        if (validDirs.Count == 0)
        {
            currentMode = false;
            currentVelocity = Vector3.zero;
            return;
        }
        currentMode = validDirs.Count < directionsCount * 0.8f
    ? false
    : true;

        Vector3 bestDir = ChooseBestDirection(validDirs, baseDir);
        lastDirection = Vector3.Lerp(lastDirection, bestDir, Time.deltaTime * smoothing);

        currentVelocity = lastDirection * maxSpeed;
        transform.position += currentVelocity * Time.deltaTime;
    }

    Vector3 SampleDirection(Vector3 baseDir)
    {
        float angle = Random.Range(0f, coneAngle);
        Vector3 axis = Random.onUnitSphere;
        return Quaternion.AngleAxis(angle, axis) * baseDir;
    }

    Vector3 ChooseBestDirection(List<Vector3> dirs, Vector3 baseDir)
    {
        float bestScore = float.MaxValue;
        Vector3 best = baseDir;

        foreach (var d in dirs)
        {
            float score = Vector3.Angle(d, baseDir);
            if (score < bestScore)
            {
                bestScore = score;
                best = d;
            }
        }
        return best;
    }
}
