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
            return; // Do nothing on boundary collision
        }

        if (other.CompareTag("AstroidShooter"))
        {
            return; // Do nothing on boundary collision
        }

        if (other.CompareTag("ForceField"))
        {
            return; // Do nothing on force field collision
        }

        if (other.CompareTag("Player"))
        {
            Instantiate(Playerexplosion, other.transform.position, other.transform.rotation);
        }

        // Destroy the asteroid on collision, but not the enemy bolt
        if (other.CompareTag("BoltEnemy1"))
        {
            return;
        }

        Instantiate(explosion, other.transform.position, transform.rotation);
    }
}
