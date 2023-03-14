using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITENCE
public class Enemy : Unit
{
    public float attackCooldown = 0.5f;
    public GameObject[] loot;

    float attackCooldownTimer = 0;
    bool isFleeing = false;
    Rigidbody rb; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");        
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDead();
        if (!GameManager.isPaused)
        {
            HandleMovement();
            HandleTimers();
        }
    }    

    void HandleTimers()
    {
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    }

    void HandleMovement()
    {
        // Move in direction of player if not fleeing
        transform.LookAt(player.transform);
        if (isFleeing)
        {
            transform.RotateAround(transform.position, transform.up, 180f);            
        } 
        rb.AddForce(transform.forward * movementSpeed * Time.deltaTime, ForceMode.Acceleration);

        // Move by speed
        transform.position -= Vector3.forward * Time.deltaTime * GameManager.speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        CheckPlayerCollision(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        CheckPlayerCollision(collision);
    }

    void CheckPlayerCollision(Collision collision)
    {
        // Handle player collision
        if (collision.gameObject.CompareTag("Player"))
        {
            // Steal coin and flee if player has any
            if (GameManager.coin > 0)
            {
                Renderer renderer = GetComponent<Renderer>();
                renderer.material.SetColor("_Color", Color.white);
                isFleeing = true;
                GameManager.coin -= 1;
                rb.velocity = Vector3.zero;
                Collider col = GetComponent<Collider>();
                col.isTrigger = true;
            }
            // Hit player and reset attackCooldownTimer
            else if (attackCooldownTimer <= 0)
            {
                GameManager.hp--;
                attackCooldownTimer = attackCooldown;
            }
        }
    }

    public override void Die()
    {         
        GameManager.enemiesKilled += 1;
        // TODO: Add coin drops
        Instantiate(loot[0], transform.position, loot[0].transform.rotation);
        Destroy(gameObject);
    }
}
