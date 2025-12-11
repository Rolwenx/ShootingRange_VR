using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetDestruction : MonoBehaviour
{

   public GameObject destructionEffect;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }

        // activate object explosition
        if(destructionEffect != null)
        {
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
        }

        // Inform target manager
        TargetManager.instance.TargetDestroyed();

    }
    
}
