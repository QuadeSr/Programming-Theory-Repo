using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;    
    public static bool isGameOver = false;
    public static int enemiesKilled = 0;
    public static int waveNumber = 0;
    public static float gold = 5;
    public static float startingGold = 5;
    public static float wood = 0;
    public static float startingWood = 0;
        
    private void Awake()
    {
        // Make sure this is a singleton and that it survives between scene changes
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    // Starts a new game
    public static void StartGame()
    {
        isGameOver = false;
        enemiesKilled = 0;
        waveNumber = 0;
        gold = startingGold;
        wood = startingWood;
        SceneManager.LoadScene(1);        
    }    
}
