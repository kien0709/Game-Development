using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn")]
    // Prefab van het boek dat gespawned wordt
    public GameObject bookPrefab;
    // Horizontale spawn grenzen
    public float minX = -7.5f;
    public float maxX = 7.5f;
    // Hoogte waar het boek verschijnt
    public float spawnY = 4.5f;

    [Header("Difficulty")]
    // Basis tijd tussen spawns op level 1
    public float baseSpawnDelay = 1.0f;     // delay op level 1
    public float delayMultiplierPerLevel = 0.90f; // 0.90 = 10% sneller per level
    public float minSpawnDelay = 0.25f;     // nooit sneller dan dit

    // Referentie naar de GameManager
    GameManager gm;

    void Awake()
    {
        // Zoek de GameManager in de scene
        gm = FindObjectOfType<GameManager>();
    }

    void OnEnable()
    {
        // Wanneer de spawner actief wordt, begin met boeken spawnen
        RestartSpawning();
    }

    void OnDisable()
    {
        // Stop het spawnen wanneer de spawner wordt uitgeschakeld
        CancelInvoke(nameof(SpawnBook));
    }
    // Start het spawnen opnieuw (bijvoorbeeld bij een nieuw level)
    public void RestartSpawning()
    {
        // Stop eerst eventuele oude spawn timers
        CancelInvoke(nameof(SpawnBook));
        // Haal het huidige level op uit de GameManager
        int level = (gm != null) ? gm.level : 1;
        // Bereken hoe snel boeken moeten spawnen op dit level
        float delay = baseSpawnDelay * Mathf.Pow(delayMultiplierPerLevel, level - 1);
        // Zorg dat het niet sneller wordt dan de minimum delay
        delay = Mathf.Max(delay, minSpawnDelay);
        // Laat Unity herhaaldelijk SpawnBook aanroepen
        InvokeRepeating(nameof(SpawnBook), 0.5f, delay);
    }
    // Maakt een nieuw boek object
    void SpawnBook()
    {
        // Als er geen prefab is ingesteld, doe niets
        if (bookPrefab == null) return;
        // Kies een willekeurige X positie tussen minX en maxX
        float x = Random.Range(minX, maxX);
        // Kies een willekeurige X positie tussen minX en maxX
        Instantiate(bookPrefab, new Vector3(x, spawnY, 0f), Quaternion.identity);
    }
}

// referentie = een link naar een ander object of script zodat dit script ermee kan werken.