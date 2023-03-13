using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITENCE
public class PlayerController : MonoBehaviour
{
    public GameObject weaponPrefab;
    public float gold = 10;
    public int enemiesKilled = 0;

    public float weaponCooldown = 1.0f;
    public float weaponCooldownTimer = 0.0f;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDead();
        HandleInput();        
    }

    private void HandleInput()
    {
        // Handle weaponCooldown
        if (weaponCooldownTimer > 0)
        {
            weaponCooldownTimer -= Time.deltaTime;
        }
        else
        {
            // Handle left mouse click
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                FireWeapon();
            }
        }
    }

    private void FireWeapon()
    {
        // Spawn weapon prefab at player's location
        Instantiate(weaponPrefab, transform.position, weaponPrefab.transform.rotation);
        weaponCooldownTimer = weaponCooldown;
    }
    private void CheckIfDead()
    {
        if (gold <= 0)
        {
            gameManager.isGameOver = true;
        }
    }
}
