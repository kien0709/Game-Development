using UnityEngine;

public class BasketCatcher : MonoBehaviour
{
    // Hoeveel punten de speler krijgt per gevangen boek
    [SerializeField] private int pointsPerBook = 1;
    // Referentie naar de GameManager om score bij te houden
    private GameManager gm;

    private void Awake()
    {
        // Zoek de GameManager in de scene
        gm = FindObjectOfType<GameManager>();
    }

    public AudioClip catchSound;

    // Wordt automatisch aangeroepen wanneer een collider de basket raakt
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Optie A: check op tag
        if (other.CompareTag("Book"))
        {
            AudioSource.PlayClipAtPoint(catchSound, transform.position);
            // Voeg punten toe aan de score via de GameManager
            if (gm != null) gm.AddScore(pointsPerBook);
            // Verwijder het boek uit de scene
            Destroy(other.gameObject);
        }
    }
}

// referentie = een link naar een ander object of script zodat dit script ermee kan werken.