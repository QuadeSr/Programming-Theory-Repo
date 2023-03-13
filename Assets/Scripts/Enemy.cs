using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITENCE
public class Enemy : Unit
{
    private Rigidbody rb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDead();
        HandleMovement();
    }
    void HandleMovement()
    {
        // Move in direction of player
        transform.LookAt(player.transform);    
        rb.AddForce(transform.forward * movementSpeed, ForceMode.Acceleration);        
    }
    override public void Die()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.enemiesKilled += 1;
        playerController.gold += 1;
        Destroy(gameObject);
    }
}
