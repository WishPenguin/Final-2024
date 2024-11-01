using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody rb;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player");

        // Calculate the direction in 3D space
        Vector3 direction = (Player.transform.position - transform.position).normalized;

        // Apply force in the direction of the player
        rb.velocity = direction * force;

        // Calculate rotation for 3D by using Quaternion.LookRotation
        transform.rotation = Quaternion.LookRotation(direction);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
