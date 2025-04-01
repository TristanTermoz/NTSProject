using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    public GameObject CoinPrefab; 
    public Transform camTransform; 
    public int CoinNumber = 50; 
    public float SpawnRange = 2f; 

    private List<GameObject> spawnedCoins;

    void Start()
    {
        spawnedCoins = new List<GameObject>();
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < CoinNumber; i++)
        {
            float x = camTransform.position.x + Random.Range(-SpawnRange, SpawnRange);
            float y = camTransform.position.y - 0.5f; 
            float z = camTransform.position.z + Random.Range(-SpawnRange, SpawnRange);
            Vector3 spawnPos = new Vector3(x, y, z);

            GameObject newCoin = Instantiate(CoinPrefab, spawnPos, Quaternion.identity);
            spawnedCoins.Add(newCoin);
        }
    }
}