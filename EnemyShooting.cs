using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet; // The projectile to be launched
    public Transform bulletPos; // Position where the projectile spawns

    private float timer;

    void Start()
    {
        // Attempt to find bulletPos if it hasn’t been set in the Inspector
        if (bulletPos == null)
        {
            bulletPos = transform.Find("BulletPos");
            if (bulletPos == null)
            {
                Debug.LogError("bulletPos not assigned and could not be found on " + gameObject.name);
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Shoot every 2 seconds
        if (timer > 2f)
        {
            timer = 0f;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPos != null)
        {
            // Instantiate the bullet at the bulletPos
            GameObject bulletInstance = Instantiate(bullet, bulletPos.position, Quaternion.identity);

            // Get the EnemyBoltCollision script from the instantiated bullet
            EnemyBoltCollision enemyBoltScript = bulletInstance.GetComponent<EnemyBoltCollision>();
            if (enemyBoltScript != null)
            {
                // Set the launching asteroid
                enemyBoltScript.SetLaunchingAsteroid(gameObject);
            }
            else
            {
                Debug.LogError("EnemyBoltCollision script not found on the instantiated bullet object.");
            }
        }
        else
        {
            Debug.LogError("Bullet position not set for " + gameObject.name);
        }
    }
}
