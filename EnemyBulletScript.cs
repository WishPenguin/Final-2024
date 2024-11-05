using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody rb;
    public float force;

 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player");


        Vector3 direction = (Player.transform.position - transform.position).normalized;
        rb.velocity = direction * force;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
