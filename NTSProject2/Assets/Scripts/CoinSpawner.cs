using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int maxCoins = 10;
    public List<GameObject> spawnedCoins = new List<GameObject>();
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public Material[] coinMaterials; 

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (spawnedCoins.Count < maxCoins)
        {
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            if (raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinBounds))
            {
                Pose hitPose = hits[0].pose;
                GameObject newCoin = Instantiate(coinPrefab, hitPose.position, Quaternion.identity);
                spawnedCoins.Add(newCoin);

                int randomIndex = Random.Range(0, coinMaterials.Length);
                newCoin.GetComponent<Renderer>().material = coinMaterials[randomIndex];
                spawnedCoins.Add(newCoin);
            }
        }
    }
}