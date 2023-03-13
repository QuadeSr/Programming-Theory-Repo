using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Unit
{
    public float damage;    
    public float knockback;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        // ABSTRACTION
        Fire();        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDead();
    }

    // Add force of speed in direction of mouse
    private void Fire()
    {
        // ENCAPSULATION
        Vector3 worldPos;
        float distance;
        Rigidbody rb;
        Plane plane = new Plane(Vector3.up, 0);        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Get the position of the mouse on the ground
        if (plane.Raycast(ray, out distance))
        {
            worldPos = ray.GetPoint(distance);
            worldPos.y = transform.position.y;
            transform.LookAt(worldPos);
            rb = GetComponent<Rigidbody>();
            // Fire weapon towards mouse
            rb.AddForce(transform.forward * movementSpeed, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle collisions with enemy
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            Rigidbody enemyRb = other.GetComponent<Rigidbody>();

            // Handle damage
            enemy.DamagedFor(damage);

            // Handle knockback
            Vector3 enemyDirection = (enemy.transform.position - player.transform.position).normalized;
            enemyRb.AddForce(enemyDirection * knockback, ForceMode.Impulse);

            // Handle what happens to this weapon
            hp -= 1;
        }
    }
}
