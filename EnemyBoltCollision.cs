using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoltCollision : MonoBehaviour
{
    public GameObject explosion;
    public GameObject Playerexplosion;

    private GameObject launchingAsteroid;

    // Method to set the launching asteroid
    public void SetLaunchingAsteroid(GameObject asteroidShooter)
    {
        launchingAsteroid = asteroidShooter;
    }

    private void Start()
    {
        // Start a coroutine to ignore collisions briefly
        StartCoroutine(IgnoredCollisionCoroutine());
    }

    private IEnumerator IgnoredCollisionCoroutine()
    {
        // Wait for a short time before enabling collisions
        yield return new WaitForSeconds(0.1f);
        Physics.IgnoreCollision(GetComponent<Collider>(), launchingAsteroid.GetComponent<Collider>());
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignore collisions with Boundary and ForceField
        if (other.CompareTag("Boundary") || other.CompareTag("ForceField"))
        {
            return;
        }

        // Check if the collided object is the launching asteroid
        if (other.gameObject == launchingAsteroid)
        {
            return; // Ignore collision with the launching asteroid
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
