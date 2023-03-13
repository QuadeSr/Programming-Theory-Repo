using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;    
    public GameObject[] enemyPrefab;
    public bool isGameOver = false;
    
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
        CheckIfGameOver();
    }
    // Starts a new game
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        StartCoroutine("SpawnDelay");
    }
    
    private void CheckIfGameOver()
    {
        if (isGameOver)
        {
            isGameOver = false;
            SceneManager.LoadScene(0);
            //TODO: add game over screen here
        }
    }
    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(1);
        SpawnMonster();
        SpawnMonster();
        SpawnMonster();
        SpawnMonster();
        SpawnMonster();        
    }
    public void SpawnMonster()
    {
        Instantiate(enemyPrefab[0], new Vector3(Random.Range(-10.0f, 10.0f), 0.5f, Random.Range(-10.0f, 10.0f)), enemyPrefab[0].transform.rotation);
    }
}
