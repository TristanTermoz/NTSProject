using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    public GameObject CoinPrefab;  // Prefab de la piece a instancier
    public Transform camTransform; // Position de la cam pour le spawn des pieces
    public int CoinNumber = 50;    // Nombre total de pieces a generer
    public float SpawnRange = 2f;  // Zone autour de la cam ou spawn les pieces

    private List<GameObject> spawnedCoins;  // Liste des pieces generees

    void Start()
    {
        spawnedCoins = new List<GameObject>();  // Initialise
        SpawnCoins();  // Lance la generation des pieces
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < CoinNumber; i++)
        {
            // Generation de coordonnees aleatoires autour de la cam
            float x = camTransform.position.x + Random.Range(-SpawnRange, SpawnRange);
            float y = camTransform.position.y - 0.5f;  // Ajustement pour pas que les pieces aille jusqu'au plafond
            float z = camTransform.position.z + Random.Range(-SpawnRange, SpawnRange);
            Vector3 spawnPos = new Vector3(x, y, z);

            // Creation de la piece
            GameObject newCoin = Instantiate(CoinPrefab, spawnPos, Quaternion.identity);
            spawnedCoins.Add(newCoin);  // Ajout a la liste des pieces existantes
        }
    }
}