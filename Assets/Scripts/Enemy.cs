using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITENCE
public class Enemy : Unit
{
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
        rb.AddForce(transform.forward * movementSpeed * Time.deltaTime, ForceMode.Acceleration);        
    }

    void OnCollisionEnter(Collision collision)
    {
        // Handle player collision
        if (collision.gameObject.CompareTag("Player"))
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.SetColor("_Color", Color.white);
            isFleeing = true;
            GameManager.gold -= 1;
            rb.velocity = Vector3.zero;
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;            
        }
    }

    public override void Die()
    {         
        GameManager.enemiesKilled += 1;        
        GameManager.gold += 1;
        Destroy(gameObject);
    }
}
