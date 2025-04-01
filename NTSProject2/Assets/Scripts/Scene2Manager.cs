using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Scene2Manager : MonoBehaviour
{
    public PlayerInput PlayerInput;
    public TextMeshProUGUI scoreText;  
    public GameObject CoinExplosionPrefab;  

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

        if (scoreText == null)
        {
            Debug.LogError("ScoreText UI element is not assigned!");
            return;
        }

        PlayerInput.enabled = true;
        touchPressAction = PlayerInput.actions["TouchPress"];
        touchPosAction = PlayerInput.actions["TouchPos"];

        score = 0;
        scoreText.text = "Score: " + score;

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
            else
            {
                Debug.Log("Touched something other than a coin.");
            }
        }
        else
        {
            Debug.Log("No object detected at touch position.");
        }
    }

    private void CollectCoin(GameObject coin)
    {
        score += 10;  // ✅ Chaque pièce donne 10 points
        scoreText.text = "Score: " + score;
        Debug.Log("Collected a coin! New Score: " + score);

        if (CoinExplosionPrefab != null)
        {
            GameObject explosion = Instantiate(CoinExplosionPrefab, coin.transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
        }

        Destroy(coin);
    }
}