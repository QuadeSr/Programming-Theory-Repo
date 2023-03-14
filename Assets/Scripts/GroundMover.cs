using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMover : MonoBehaviour
{
    public float speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed += GetScroll();        
    }

    private void FixedUpdate()
    {
        transform.position -= Vector3.forward * Time.deltaTime * speed;
    }

    public float GetScroll()
    {
        float scroll = Input.mouseScrollDelta.y;        
        return scroll;
    }
}


