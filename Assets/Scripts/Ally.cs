using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Unit
{
    public float damage;
    public float knockback;
    public float hitCooldown;
    private float hitCooldownTimer = 0;
    public float gatherCooldown;
    public float gatherCooldownTimer;
    Rigidbody rb;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        gatherCooldownTimer = gatherCooldown;
        GetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleTimers();
    }

    void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKey(KeyCode.Mouse1))
        {
            GetTarget();
        }
        transform.LookAt(target);
        {
            rb.AddForce(transform.forward * movementSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    void HandleTimers()
    {
        if (hitCooldownTimer > 0)
        {
            hitCooldownTimer -= Time.deltaTime;
        }
        if (gatherCooldownTimer < gatherCooldown)
        {
            gatherCooldownTimer += Time.deltaTime;
        }
    }

    void GetTarget()
    {
        // ENCAPSULATION        
        float distance;
        Plane plane = new Plane(Vector3.up, 0);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Get the position of the mouse on the ground
        if (plane.Raycast(ray, out distance))
        {
            target = ray.GetPoint(distance);
            target.y = transform.position.y;
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

    private void OnCollisionStay(Collision collision)
    {
        TestGatherHit(collision.collider);
    }

    void TestGatherHit(Collider other)
    {
        if (other.CompareTag("Resource"))
        {
            gatherCooldownTimer -= Time.deltaTime * 2;
            if (gatherCooldownTimer <= 0)
            {
                Debug.Log("Resourse Gathered");
                GameManager.wood += 1;
                gatherCooldownTimer = gatherCooldown;
            }
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
