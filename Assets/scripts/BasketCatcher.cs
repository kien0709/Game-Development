using UnityEngine;

public class BasketCatcher : MonoBehaviour
{
    [SerializeField] private int pointsPerBook = 1;

    private GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Optie A: check op tag
        if (other.CompareTag("Book"))
        {
            if (gm != null) gm.AddScore(pointsPerBook);
            Destroy(other.gameObject);
        }
    }
}