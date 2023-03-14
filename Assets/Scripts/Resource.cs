using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public string type;
    public float amount = 1;
    public float gatherTime = 1.0f;
    public float despawnDistance = 20.0f;

    public float gatherTimer = 0.0f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDead();
        // Timer
        if (gatherTimer < 0)
        {
            gatherTimer = 0;
        }
        // Movement
        if (!GameManager.isPaused)
        {
            // Move by speed
            transform.position -= Vector3.forward * Time.deltaTime * GameManager.speed;
            gatherTimer -= Time.deltaTime;
            
        }
    }
    
    public virtual void CheckIfDead()
    {
        if (amount <= 0)
        {
            Die();
        }
        if (Vector3.Distance(transform.position, player.transform.position) > despawnDistance)
        {
            Despawn();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
    public virtual void Despawn()
    {
        Destroy(gameObject);
    }

    public void Gather()
    {
        gatherTimer += 2 * Time.deltaTime;
        if (gatherTimer >= gatherTime)
        {
            gatherTimer = 0;
            GatherOne();
        }        
    }

    public void GatherOne()
    {        
        if (type == "Coin")
        {
            GameManager.coin += 1;
            Debug.Log("Coin Gathered");
        }
        else if (type == "Food")
        {
            GameManager.food += 1;
            Debug.Log("Food Gathered");
        }
        else if (type == "Wood")
        {
            GameManager.wood += 1;
            Debug.Log("Wood Gathered");
        }
        else if (type == "Stone")
        {
            GameManager.stone += 1;
            Debug.Log("Stone Gathered");
        }
        else if (type == "Metal")
        {
            GameManager.metal += 1;
            Debug.Log("Metal Gathered");
        }
        amount -= 1;        
    }
}
