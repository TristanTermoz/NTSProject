using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int pointValue; 
    public List<Material> materials; 

    private void Start()
    {
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<SphereCollider>().isTrigger = true;
        }
    }

    public void AssignRandomMaterial()
    {
        if (materials.Count > 0)
        {
            int randomIndex = Random.Range(0, materials.Count);
            GetComponent<MeshRenderer>().material = materials[randomIndex];

            
            pointValue = (randomIndex + 1) * 10;  
        }
    }
}

