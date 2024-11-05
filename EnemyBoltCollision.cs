using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoltCollision : MonoBehaviour
{
    public GameObject explosion;
    public GameObject Playerexplosion;

    private GameObject launchingAsteroid;

   
    public void SetLaunchingAsteroid(GameObject asteroidShooter)
    {
        launchingAsteroid = asteroidShooter;
    }

    private void Start()
    {
        StartCoroutine(IgnoredCollisionCoroutine());
    }

    private IEnumerator IgnoredCollisionCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        Physics.IgnoreCollision(GetComponent<Collider>(), launchingAsteroid.GetComponent<Collider>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("ForceField"))
        {
            return;
        }

        if (other.gameObject == launchingAsteroid)
        {
            return; // Ignore collision with the launching asteroid
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
