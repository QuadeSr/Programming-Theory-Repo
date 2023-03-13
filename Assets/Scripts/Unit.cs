using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{    
    public float hp;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
