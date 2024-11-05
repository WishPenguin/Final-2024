using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet; 
    public Transform bulletPos; 

    private float timer;

    void Start()
    {
       
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
          
            GameObject bulletInstance = Instantiate(bullet, bulletPos.position, Quaternion.identity);

  
            EnemyBoltCollision enemyBoltScript = bulletInstance.GetComponent<EnemyBoltCollision>();
            if (enemyBoltScript != null)
            {
              
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
