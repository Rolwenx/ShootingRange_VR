using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetDestruction : MonoBehaviour
{

   public GameObject destructionEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
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

            // destroy target
            Destroy(gameObject);

            // Optionnel: détruire la balle
    }
}
