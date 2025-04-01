using UnityEngine;

public class Coin : MonoBehaviour
{
    public int pointValue = 10;  // Chaque piece vaut 10 points

    private void Start()
    {
        if (GetComponent<MeshRenderer>() == null)
        {
            Debug.LogError("Coin is missing MeshRenderer!");
        }

        if (GetComponent<Collider>() == null)
        {
            SphereCollider collider = gameObject.AddComponent<SphereCollider>();
            collider.isTrigger = true;  
        }

        gameObject.tag = "Coin";  // Assigne le bon tag pour le raycast
    }
}

