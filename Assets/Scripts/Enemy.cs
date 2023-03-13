using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITENCE
public class Enemy : Unit
{
    private bool isFleeing = false;

    private Rigidbody rb;
    private GameObject player;
    PlayerController playerController;    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDead();
        HandleMovement();
    }
    void HandleMovement()
    {
        // Move in direction of player if not fleeing
        transform.LookAt(player.transform);
        if (isFleeing)
        {
            transform.RotateAround(transform.position, transform.up, 180f);            
        } 
        rb.AddForce(transform.forward * movementSpeed, ForceMode.Acceleration);        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle player collision
        if (collision.gameObject.CompareTag("Player"))
        {
            isFleeing = true;
            playerController.gold -= 1;
            rb.velocity = Vector3.zero;
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;            
        }
    }

    override public void Die()
    {         
        playerController.enemiesKilled += 1;
        playerController.gold += 1;
        Destroy(gameObject);
    }
}
