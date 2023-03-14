using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITENCE
public class PlayerController : MonoBehaviour
{
    public GameObject weaponPrefab;
    public GameObject allyPrefab;

    public float weaponCooldown = 0.5f;
    private float weaponCooldownTimer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        if (!GameManager.isPaused)
        {
            CheckIfDead();
            HandleInput();
            FireWeapon();
        }
    }

    private void HandleInput()
    {  
        // Handle left mouse click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SpawnAlly();
        }

        // Handle scroll wheel
        GameManager.speed += GetScroll();
        if (GameManager.speed < 0)
        {
            GameManager.speed = 0;
        }
    }

    public float GetScroll()
    {
        float scroll = Input.mouseScrollDelta.y;
        return scroll;
    }

    void SpawnAlly()
    {
        // Spawn ally prefab at player's location
        if (GameObject.FindGameObjectsWithTag("Ally").Length < 1)
        {
            Instantiate(allyPrefab, transform.position + Vector3.forward * 2, allyPrefab.transform.rotation);
        }
    }
    
    private void FireWeapon()
{
    // Handle weaponCooldown
    if (weaponCooldownTimer > 0)
    {
        weaponCooldownTimer -= Time.deltaTime;
    }
    else
    {
        // Spawn weapon prefab at player's location
        Instantiate(weaponPrefab, transform.position, weaponPrefab.transform.rotation);
        weaponCooldownTimer = weaponCooldown;
    }
}

    private void CheckIfDead()
    {
        if (GameManager.hp <= 0)
        {
            GameManager.isGameOver = true;
        }
    }
}
