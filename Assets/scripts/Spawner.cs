using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn")]
    public GameObject bookPrefab;
    public float minX = -7.5f;
    public float maxX = 7.5f;
    public float spawnY = 4.5f;

    [Header("Difficulty")]
    public float baseSpawnDelay = 1.0f;     // delay op level 1
    public float delayMultiplierPerLevel = 0.90f; // 0.90 = 10% sneller per level
    public float minSpawnDelay = 0.25f;     // nooit sneller dan dit

    GameManager gm;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void OnEnable()
    {
        RestartSpawning();
    }

    void OnDisable()
    {
        CancelInvoke(nameof(SpawnBook));
    }

    public void RestartSpawning()
    {
        CancelInvoke(nameof(SpawnBook));

        int level = (gm != null) ? gm.level : 1;
        float delay = baseSpawnDelay * Mathf.Pow(delayMultiplierPerLevel, level - 1);
        delay = Mathf.Max(delay, minSpawnDelay);

        InvokeRepeating(nameof(SpawnBook), 0.5f, delay);
    }

    void SpawnBook()
    {
        if (bookPrefab == null) return;

        float x = Random.Range(minX, maxX);
        Instantiate(bookPrefab, new Vector3(x, spawnY, 0f), Quaternion.identity);
    }
}