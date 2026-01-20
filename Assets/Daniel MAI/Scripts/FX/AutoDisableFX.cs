using UnityEngine;
using System.Collections;

public class AutoDisableFX : MonoBehaviour
{

    public float lifetime = 2f;
    private void OnEnable()
    {
        StartCoroutine(AutoDisableRoutine());
    }

    IEnumerator AutoDisableRoutine()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
}
