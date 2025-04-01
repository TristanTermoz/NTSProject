using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject coinExplosionPrefab;
    private int score = 0;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Coin"))  
                {
                    CollectCoin(hit.collider.gameObject);
                }
            }
        }
    }

    void CollectCoin(GameObject coin)
    {
        Coin coinScript = coin.GetComponent<Coin>();
        if (coinScript != null)
        {
            score += coinScript.pointValue;
            scoreText.text = "Score: " + score;
        }

        if (coinExplosionPrefab != null)
        {
            GameObject explosion = Instantiate(coinExplosionPrefab, coin.transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
        }

        Destroy(coin);
    }
}
