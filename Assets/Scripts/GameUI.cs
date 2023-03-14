using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI distanceTraveledText;
    public TextMeshProUGUI enemiesKilledText;
    public TextMeshProUGUI enemiesPerWaveText;
    public TextMeshProUGUI nextWaveInText;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI stoneText;
    public TextMeshProUGUI metalText;

    public GameObject gameOver;
    private SpawnManager spawnManager;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesKilledText.text = "Enemies killed: " + GameManager.enemiesKilled;
        distanceTraveledText.text = "Distance traveled: " + (int)GameManager.distanceTraveled + " meters";
        enemiesPerWaveText.text = "Enemies per wave: " + (int)spawnManager.enemiesToSpawn;
        nextWaveInText.text = "Next wave in: " + (int)spawnManager.waveTimer;

        hpText.text = "HP: " + GameManager.hp + "/" + GameManager.maxHp;
        coinText.text = "Coins: " + GameManager.coin;
        foodText.text = "Food: " + GameManager.food;
        woodText.text = "Wood: " + GameManager.wood;
        stoneText.text = "Stone: " + GameManager.stone;
        metalText.text = "Metal: " + GameManager.metal;

        if (GameManager.isGameOver)
        {
            gameOver.SetActive(true);
        }
        else
        {
            gameOver.SetActive(false);
        }
    }
    public void StartGame()
    {
        GameManager.StartGame();
    }
}
