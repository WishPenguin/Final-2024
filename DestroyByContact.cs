using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject Playerexplosion;

    void OnTriggerEnter(Collider other)
    {
      
        if (other.tag == "Boundary" || other.tag == "ForceField")
        {
            return;
        }

     
        if (other.CompareTag("AsteroidShooter"))
        {
            return; // Adjust this tag based on your asteroid's tag
        }

      
        Instantiate(explosion, other.transform.position, transform.rotation);

        if (other.CompareTag("Player"))
        {
            Instantiate(Playerexplosion, other.transform.position, other.transform.rotation);
        }

      
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
