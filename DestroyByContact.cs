using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject Playerexplosion;

    void OnTriggerEnter(Collider other)
    {
        // Ignore collisions with Boundary and ForceField
        if (other.tag == "Boundary" || other.tag == "ForceField")
        {
            return;
        }

        // Prevent destroying the asteroid the bolt came from
        if (other.CompareTag("AsteroidShooter"))
        {
            return; // Adjust this tag based on your asteroid's tag
        }

        // Instantiate explosion effects
        Instantiate(explosion, other.transform.position, transform.rotation);

        if (other.CompareTag("Player"))
        {
            Instantiate(Playerexplosion, other.transform.position, other.transform.rotation);
        }

        // Destroy the collided object and the bolt itself
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
