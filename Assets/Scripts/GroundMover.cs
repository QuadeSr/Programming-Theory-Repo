using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMover : MonoBehaviour{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isPaused)
        {
            // Move by speed
            transform.position -= Vector3.forward * Time.deltaTime * GameManager.speed;

            // Reset position when too far south
            if (transform.position.z < -GetComponent<BoxCollider>().size.z * transform.localScale.z)
            {
                Vector3 resetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + GetComponent<BoxCollider>().size.z * transform.localScale.z * 2);
                transform.position = resetPosition;
            }
        }
    }      
}


