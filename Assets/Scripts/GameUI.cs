using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI enemiesKilledText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI waveNumberText;
    public GameObject gameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemiesKilledText.text = "Enemies Killed: " + GameManager.enemiesKilled;
        goldText.text = "Gold: " + GameManager.gold;
        waveNumberText.text = "Wave Number: " + GameManager.waveNumber;
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
