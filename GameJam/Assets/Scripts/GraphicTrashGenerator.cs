using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicTrashGenerator : Singleton<GraphicTrashGenerator> {

    public GameObject[] airborneTrash;
    public Transform[] airborneSpawnPoints;
    public float maxAirborneSpawnTimer;
    public float minAirborneSpawnTimer;

    public GameObject[] waterborneTrash;
    public Transform[] waterborneSpawnPoints;
    public float maxWaterborneSpawnTimer;
    public float minWaterborneSpawnTimer;

    public GameObject[] underseaTrash;
    public Transform[] underseaSpawnPoints;
    public float maxUnderseaSpawnTimer;
    public float minUnderseaSpawnTimer;

    protected override void Awake ()
    {
        base.Awake();

        BearScript.PlayerRevived += OnPlayerRevived;

        StartCoroutine(AirborneSpawning());
        StartCoroutine(WaterborneSpawning());
        StartCoroutine(UnderseaSpawning());
    }

    private void OnPlayerRevived()
    {
        StopAllCoroutines();

        StartCoroutine(AirborneSpawning());
        StartCoroutine(WaterborneSpawning());
        StartCoroutine(UnderseaSpawning());
    }

    IEnumerator AirborneSpawning()
    {
        float timer = Random.Range(minAirborneSpawnTimer, maxAirborneSpawnTimer);
        while (true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = Random.Range(minAirborneSpawnTimer, maxAirborneSpawnTimer);
                AirborneSpawn();
            }
            yield return null;
        }
    }

    IEnumerator WaterborneSpawning()
    {
        float timer = Random.Range(minWaterborneSpawnTimer, maxWaterborneSpawnTimer);
        while (true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = Random.Range(minWaterborneSpawnTimer, maxWaterborneSpawnTimer);
                WaterborneSpawn();
            }
            yield return null;
        }
    }

    IEnumerator UnderseaSpawning()
    {
        float timer = Random.Range(minUnderseaSpawnTimer, maxUnderseaSpawnTimer);
        while (true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = Random.Range(minUnderseaSpawnTimer, maxUnderseaSpawnTimer);
                UnderseaSpawn();
            }
            yield return null;
        }
    }

    void AirborneSpawn()
    {
        if (airborneTrash.Length == 0 || airborneSpawnPoints.Length == 0)
        {
            Debug.LogWarning("No airborne trash or spawn points, baka!");
            return;
        }
        GameObject spawnedTrash = airborneTrash[Random.Range(0, airborneTrash.Length)];
        int selector = Random.Range(0, airborneSpawnPoints.Length);
        spawnedTrash.Spawn(airborneSpawnPoints[selector].position, airborneSpawnPoints[selector].rotation);
    }

    void WaterborneSpawn()
    {
        if (waterborneTrash.Length == 0 || waterborneSpawnPoints.Length == 0)
        {
            Debug.LogWarning("No waterborne trash or spawn points, baka!");
            return;
        }
        GameObject spawnedTrash = waterborneTrash[Random.Range(0, waterborneTrash.Length)];
        int selector = Random.Range(0, waterborneSpawnPoints.Length);
        spawnedTrash.Spawn(waterborneSpawnPoints[selector].position, waterborneSpawnPoints[selector].rotation);
    }

    void UnderseaSpawn()
    {
        if (underseaTrash.Length == 0 || underseaSpawnPoints.Length == 0)
        {
            Debug.LogWarning("No undersea trash or spawn points, baka!");
            return;
        }
        GameObject spawnedTrash = underseaTrash[Random.Range(0, underseaTrash.Length)];
        int selector = Random.Range(0, underseaSpawnPoints.Length);
        spawnedTrash.Spawn(underseaSpawnPoints[selector].position, underseaSpawnPoints[selector].rotation);
    }    
}
