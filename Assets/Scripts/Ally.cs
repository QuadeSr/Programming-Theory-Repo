using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Unit
{
    Rigidbody rb;
    Vector3 target;

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
    }

    void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GetTarget();
        }
        transform.LookAt(target);        
        {
            rb.AddForce(transform.forward * movementSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
    }
    public void GetTarget()
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
}
