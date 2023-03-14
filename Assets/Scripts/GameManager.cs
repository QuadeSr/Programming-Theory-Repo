using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;    
    public static bool isGameOver = false;
    public static bool isPaused = false;
    public static int enemiesKilled = 0;
    public static float speed = 0;
    public static float distanceTraveled = 0;    
    public static float wealth = 0;

    public static float hp = 10;
    public static float maxHp = 10;
    public static float startingHp = 10;
    public static float coin = 5;
    public static float startingCoin = 5;
    public static float food = 0;
    public static float startingFood = 0;
    public static float wood = 0;
    public static float startingWood = 0;
    public static float stone = 0;
    public static float startingStone = 0;
    public static float metal = 0;
    public static float startingMetal = 0;

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
        HandleInput();
        if (!GameManager.isPaused)
        {
            distanceTraveled += speed * Time.deltaTime;
        }        
    }
    // Starts a new game
    public static void StartGame()
    {
        isGameOver = false;
        isPaused = false;
        enemiesKilled = 0;        
        distanceTraveled = 0;        
        wealth = 0;
        speed = 0;

        hp = startingHp;
        maxHp = startingHp;
        coin = startingCoin;
        wood = startingWood;
        SceneManager.LoadScene(1);        
    }
    private void HandleInput()
    {
        // Handle right mouse click
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isGameOver)
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
