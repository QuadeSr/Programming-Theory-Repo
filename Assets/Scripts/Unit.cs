using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{    
    public float hp;
    public float movementSpeed;
    public GameObject player;

    float despawnDistance = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // ABSTRACTION
        CheckIfDead();
    }

    public virtual void DamagedFor(float damage)
    {
        hp -= damage;
    }

    public virtual void CheckIfDead()
    {
        if (hp <= 0)
        {
            Die();
        }
        if (transform.position.magnitude - player.transform.position.magnitude > despawnDistance)
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
}
