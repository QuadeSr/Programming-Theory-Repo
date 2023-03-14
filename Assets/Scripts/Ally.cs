using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ally : Unit
{
    public float damage;
    public float knockback;    

    public float hitCooldown;
    private float hitCooldownTimer = 0;

    public float range = 15.0f;
    public float minRange = 0.25f;

    Rigidbody rb;
    Vector3 target;
    GameObject resourceTargeted;    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();        
        GetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        if (!GameManager.isPaused)
        {            
            HandleTimers();
        }
    }    

    void HandleMovement()
    {
        // Get a target when you left click
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0))
        {
            GetTarget();
        }

        // If targeting a resource, target it again since its position changes over time
        if (resourceTargeted != null)
        {          
            target = resourceTargeted.transform.position;
            target.y = transform.position.y;
        }

        // If it's too far from player, drop target and target player       
        if (Vector3.Distance(transform.position, player.transform.position) > range)
        {
            resourceTargeted = null;
            Debug.Log("Target Dropped");
            target = player.transform.position + Vector3.forward * 2;
        }

        // If distance to target > minRange, look at target and apply movementSpeed acceleration towards it 
        if (Vector3.Distance(transform.position, target) > minRange)
        {
            transform.LookAt(target);
            rb.AddForce(transform.forward * movementSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    void HandleTimers()
    {
        if (hitCooldownTimer > 0)
        {
            hitCooldownTimer -= Time.deltaTime;
        }        
    }

    void GetTarget()
    {
        // ENCAPSULATION        
        float distance;
        Plane plane = new Plane(Vector3.up, 0);        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Get the position of the mouse on the ground and set it as target
        if (plane.Raycast(ray, out distance))
        {
            target = ray.GetPoint(distance);
            target.y = transform.position.y;            
            resourceTargeted = null;            
            Debug.Log("Ground Targeted");            
        }

        // If you click on a resource, set that as the target
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            if (raycastHit.transform.gameObject.CompareTag("Resource") || raycastHit.transform.gameObject.CompareTag("Enemy"))
            {
                target = raycastHit.transform.position;
                target.y = transform.position.y;
                resourceTargeted = raycastHit.transform.gameObject;                
                Debug.Log("Resource Targeted");
            }
        }           
    }

    void OnTriggerEnter(Collider other)
    {
        TestEnemyHit(other);
    }
    void OnCollisionEnter(Collision collision)
    {
        TestEnemyHit(collision.collider);
    }
    
    void OnTriggerStay(Collider other)
    {
        TestGatherHit(other);
    }

    void TestGatherHit(Collider other)
    {
        if (other.CompareTag("Resource"))
        {
            Resource resource = other.gameObject.GetComponent<Resource>();
            Debug.Log("Gathering resource: " + resource.type);
            resource.Gather();
        }
    }

    void TestEnemyHit(Collider other)
    {        
        if (other.CompareTag("Enemy") && hitCooldownTimer <= 0)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            Rigidbody enemyRb = other.GetComponent<Rigidbody>();

            // Handle damage
            enemy.DamagedFor(damage);

            // Handle knockback
            Vector3 enemyDirection = (enemy.transform.position - transform.position).normalized;
            enemyRb.AddForce(enemyDirection * knockback, ForceMode.Impulse);

            // Handle cooldown reset
            hitCooldownTimer = hitCooldown;
        }
    }
}
