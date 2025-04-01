using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Scene2Manager : MonoBehaviour
{
    public PlayerInput PlayerInput;
    public GameObject CoinExplosionPrefab; 
    public TMP_Text scoreText;  

    private InputAction touchPressAction;
    private InputAction touchPosAction;
    private int score;

    void Start()
    {
        if (PlayerInput == null)
        {
            Debug.LogError("PlayerInput is not assigned!");
            return;
        }

        PlayerInput.enabled = true;

        touchPressAction = PlayerInput.actions["TouchPress"];
        touchPosAction = PlayerInput.actions["TouchPos"];

        score = 0;

        if (touchPressAction == null || touchPosAction == null)
        {
            Debug.LogError("TouchPress or TouchPos action is missing from PlayerInput!");
        }
    }

    void Update()
    {
        if (touchPressAction.WasPerformedThisFrame())  
        {
            TryCollectCoin(touchPosAction.ReadValue<Vector2>());
        }
    }

    public void TryCollectCoin(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.CompareTag("Coin"))
            {
                CollectCoin(hit.collider.gameObject);
            }
        }
    }

    private void CollectCoin(GameObject coin)
    {
        Coin coinScript = coin.GetComponent<Coin>();
        if (coinScript != null)
        {
            score += coinScript.pointValue;
            scoreText.text = "Score: " + score;
        }

        if (CoinExplosionPrefab != null)
        {
            GameObject explosion = Instantiate(CoinExplosionPrefab, coin.transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
        }

        Destroy(coin);
    }
}