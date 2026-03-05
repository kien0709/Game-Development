using UnityEngine;

public class Book : MonoBehaviour
{
    [Header("Movement")]
    public float destroyY = -5.5f;

    [Header("Sprites")]
    public Sprite[] possibleSprites;

    float fallSpeed;
    GameManager gm;
    SpriteRenderer sr;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
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
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < destroyY)
        {
            if (gm != null) gm.AddMiss();
            Destroy(gameObject);
        }
    }
}