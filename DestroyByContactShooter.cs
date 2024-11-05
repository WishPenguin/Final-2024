using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContactShooter: MonoBehaviour
{
    public GameObject explosion;
    public GameObject Playerexplosion;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            return; 
        }

        if (other.CompareTag("AstroidShooter"))
        {
            return;
        }

        if (other.CompareTag("ForceField"))
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            Instantiate(Playerexplosion, other.transform.position, other.transform.rotation);
        }

     
        if (other.CompareTag("BoltEnemy1"))
        {
            return;
        }

        Instantiate(explosion, other.transform.position, transform.rotation);
    }
}
