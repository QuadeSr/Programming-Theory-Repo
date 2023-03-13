using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;    
    public GameObject[] enemyPrefab;
    
    private float waveCheckTime = 2.0f;
    private float waveTimer;
    private float spawnBufferSize = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");        
        waveTimer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        TryNextWave();
    }

    void TryNextWave()
    {
        // Every waveCheckTime seconds, if there are no enemies alive it will spawn a new wave of enemies
        waveTimer -= Time.deltaTime;
        if (waveTimer <= 0)
        {
            waveTimer = waveCheckTime;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length <= (GameManager.waveNumber / 2))
            {
                SpawnWave();
            }
        }        
    }

    void SpawnWave()
    {
        // Pick a random corner
        Vector3 randomCorner = new Vector3(Random.Range(0, 2) * spawnBufferSize * 2 - spawnBufferSize, 0, Random.Range(0, 2) * spawnBufferSize * 2 - spawnBufferSize);

        GameManager.waveNumber++;
        for (int i = 0; i < GameManager.waveNumber; i++)
        {            
            Vector3 spawnPos = new Vector3(Random.Range(0f, 5.0f), 0.5f, Random.Range(0f, 5.0f));
            SpawnMonster(randomCorner + spawnPos);
        }        
    }

    void SpawnMonster(Vector3 pos)
    {
        Instantiate(enemyPrefab[0], pos, enemyPrefab[0].transform.rotation);
    }
}
