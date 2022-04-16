using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject npcPrefab;

    [SerializeField] float spawnTime = 10f;
    float nextTimeToSpawn;

    bool isSpawnPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnNPC", 1f);
        nextTimeToSpawn = Time.time + spawnTime;
    }

    void Update()
    {
        if(isSpawnPaused)
        {
            nextTimeToSpawn = Time.time + spawnTime;
        }

        if(Time.time >= nextTimeToSpawn)
        {
            SpawnNPC();
            nextTimeToSpawn = Time.time + spawnTime;
        }
    }

    void SpawnNPC()
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(npcPrefab, spawnPoints[randomSpawnIndex].position, spawnPoints[randomSpawnIndex].rotation);
    }

    public void PauseSpawning()
    {
        isSpawnPaused = true;
    }

    public void ResumeSpawning()
    {
        isSpawnPaused = false;
    }
}
