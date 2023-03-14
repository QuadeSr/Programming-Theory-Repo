using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;    
    public GameObject[] enemyPrefab;
    public GameObject[] resourcePrefab;
    public float resourceFrequency = 10;

    public float waveFrequency = 10;
    public float enemiesToSpawn = 0;
    public float distancePerEnemy = 60;

    private float resourceBufferSize = 8.0f;
    private float nextResourcesIn = 0.0f;
    
    public float waveTimer;
    private float spawnBufferSize = 10.0f;    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");       
    }

    // Update is called once per frame
    // TODO: rework the methods below to work with distance traveled and wealth
    void Update()
    {
        if (!GameManager.isPaused)
        {
            TryNextWave();
            SpawnResources();
        }
    }

    // Spawn Resources every resourceFrequency distance traveled
    void SpawnResources()
    {
        nextResourcesIn -= GameManager.speed * Time.deltaTime;
        if (nextResourcesIn <= 0)
        {
            nextResourcesIn = resourceFrequency;            

            for (int i = 0; i < resourcePrefab.Length; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-4.0f, 4.0f), 0.5f, Random.Range(-4.0f, 4.0f));
                // Pick a top random corner
                Vector3 randomCorner = new Vector3(Random.Range(0, 2) * resourceBufferSize * 2 - resourceBufferSize, 0, Random.Range(30.0f, 40.0f));
                Instantiate(resourcePrefab[i], pos + randomCorner, resourcePrefab[i].transform.rotation);
            }
        }
    }
    
    void TryNextWave()
    {
        enemiesToSpawn = (int)(GameManager.distanceTraveled / distancePerEnemy);
        // Every waveFrequency seconds, it will spawn a new wave of enemies
        waveTimer -= Time.deltaTime;
        if (waveTimer <= 0)
        {
            waveTimer = waveFrequency;            
            SpawnWave();            
        }        
    }

    void SpawnWave()
    {
        // Pick a random corner for this wave to spawn at
        Vector3 randomCorner = new Vector3(Random.Range(0, 2) * spawnBufferSize * 2 - spawnBufferSize, 0, Random.Range(0, 2) * spawnBufferSize * 2 - spawnBufferSize);
        
        // Spawn enemies
        for (int i = 0; i < enemiesToSpawn; i++)
        {            
            Vector3 spawnPos = new Vector3(Random.Range(0f, 5.0f), 0.5f, Random.Range(0f, 5.0f));
            SpawnMonster(spawnPos + randomCorner);
        }        
    }    

    void SpawnMonster(Vector3 pos)
    {
        GameObject monster = Instantiate(enemyPrefab[0], pos, enemyPrefab[0].transform.rotation);        
    }
}
