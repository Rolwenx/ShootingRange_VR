using UnityEngine;

public class DebugAutoWalk : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("speed", 0.5f); 
    }
}
