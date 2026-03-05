using UnityEngine;

public class Book : MonoBehaviour
{
    // Hoogte waar het boek wordt verwijderd als het gemist wordt
    [Header("Movement")]
    public float destroyY = -5.5f;

    [Header("Sprites")]
    // Mogelijke sprites (verschillende kleuren boeken)
    public Sprite[] possibleSprites;

    // Snelheid waarmee het boek naar beneden valt
    float fallSpeed;
    GameManager gm;
    SpriteRenderer sr;

    void Start()
    {
        // Zoek de GameManager in de scene
        gm = FindObjectOfType<GameManager>();
        // Haal de SpriteRenderer van dit object op
        sr = GetComponent<SpriteRenderer>();

        // fall speed uit GameManager (level-based)
        fallSpeed = (gm != null) ? gm.GetCurrentFallSpeed() : 3.5f;

        // random sprite (kleur/boek variant)
        if (sr != null && possibleSprites != null && possibleSprites.Length > 0)
        {
            sr.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
        }
    }

    void Update()
    {
        // Laat het boek naar beneden bewegen
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        // Als het boek onder de onderkant van het scherm komt
        if (transform.position.y < destroyY)
        {
            // Tel een miss bij de GameManager
            if (gm != null) gm.AddMiss();
            // Verwijder het boek uit de scene
            Destroy(gameObject);
        }
    }
}

// referentie = een link naar een ander object of script zodat dit script ermee kan werken.