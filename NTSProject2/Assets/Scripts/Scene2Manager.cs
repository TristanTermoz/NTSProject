using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Scene2Manager : MonoBehaviour
{
    public PlayerInput PlayerInput;  // Gestion des actions
    public TextMeshProUGUI scoreText;  // Affichage du score
    public GameObject CoinExplosionPrefab;  // Effet de particules lors de la collecte d'une piece

    private InputAction touchPressAction;  // Action de pression sur l'ecran
    private InputAction touchPosAction;    // Position du toucher sur l'ecran
    private int score;  // Score du joueur

    void Start()
    {
        // Verifie que les elements sont bien dans l'Inspector
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

        // Active les controles du joueur
        PlayerInput.enabled = true;
        touchPressAction = PlayerInput.actions["TouchPress"];
        touchPosAction = PlayerInput.actions["TouchPos"];

        // Initialise le score
        score = 0;
        scoreText.text = "Score: " + score;

        // Verifie que les actions existent
        if (touchPressAction == null || touchPosAction == null)
        {
            Debug.LogError("TouchPress or TouchPos action is missing from PlayerInput!");
        }
    }

    void Update()
    {
        // Verifie si le joueur a touche l'ecran pendant la frame
        if (touchPressAction.WasPerformedThisFrame())  
        {
            TryCollectCoin(touchPosAction.ReadValue<Vector2>());
        }
    }

    // Verifie si un objet est touche et tente de recuperer une piece
    public void TryCollectCoin(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.CompareTag("Coin"))  // Verifie si c'est une piece
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
        score += 10;  // Chaque piece donne 10 points
        scoreText.text = "Score: " + score;
        Debug.Log("Collected a coin! New Score: " + score);

        // Genere une explosion si le prefab est assigner
        if (CoinExplosionPrefab != null)
        {
            GameObject explosion = Instantiate(CoinExplosionPrefab, coin.transform.position, Quaternion.identity);
            Destroy(explosion, 1f);  // Plus d'effet apres une seconde
        }

        Destroy(coin);  // Supprime la piece après la collecte
    }
}