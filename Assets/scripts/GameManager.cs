using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum State { Start, Playing, Win, Lose }
    public State CurrentState;

    [Header("UI Panels")]
    public GameObject startPanel;
    public GameObject hudPanel;
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("UI Text")]
    public Text scoreText;
    public Text missText;
    public Text winFinalText;
    public Text loseFinalText;
    public Text levelText;

    [Header("Rules")]
    public int baseScoreToWin = 10;     // score die je nodig hebt op level 1
    public int scoreIncreasePerLevel = 3;
    public int maxMisses = 5;

    [Header("Difficulty (Falling speed)")]
    public int level = 1;
    public float baseFallSpeed = 3.5f;
    public float fallSpeedIncreasePerLevel = 0.6f;

    [HideInInspector] public int score = 0;
    [HideInInspector] public int misses = 0;

    public Spawner spawner;

    int scoreToWin;

    void Start()
    {
        scoreToWin = baseScoreToWin;
        SetState(State.Start);
        UpdateHUD();
    }

    public float GetCurrentFallSpeed()
    {
        return baseFallSpeed + (level - 1) * fallSpeedIncreasePerLevel;
    }

    public void StartGame()
    {
        // Start op huidig level
        ResetRoundOnly();
        SetState(State.Playing);
    }

    // Button op WinPanel: Next level
    public void NextLevel()
    {
        level++;
        scoreToWin = baseScoreToWin + (level - 1) * scoreIncreasePerLevel;

        ResetRoundOnly();
        SetState(State.Playing);
    }

    // Button op LosePanel: opnieuw zelfde level
    public void RestartSameLevel()
    {
        ResetRoundOnly();
        SetState(State.Playing);
    }

    // (optioneel) helemaal opnieuw
    public void RestartFromLevel1()
    {
        level = 1;
        scoreToWin = baseScoreToWin;

        ResetRoundOnly();
        SetState(State.Playing);
    }

    public void AddScore(int amount)
    {
        if (CurrentState != State.Playing) return;

        score += amount;
        UpdateHUD();

        if (score >= scoreToWin)
            SetState(State.Win);
    }

    public void AddMiss()
    {
        if (CurrentState != State.Playing) return;

        misses += 1;
        UpdateHUD();

        if (misses >= maxMisses)
            SetState(State.Lose);
    }

    void SetState(State newState)
    {
        CurrentState = newState;

        if (startPanel) startPanel.SetActive(newState == State.Start);
        if (hudPanel) hudPanel.SetActive(newState == State.Playing);
        if (winPanel) winPanel.SetActive(newState == State.Win);
        if (losePanel) losePanel.SetActive(newState == State.Lose);

        if (spawner != null)
        {
            spawner.enabled = (newState == State.Playing);
            if (newState == State.Playing)
                spawner.RestartSpawning();
        }

        if (newState == State.Win)
        {
            if (winFinalText) winFinalText.text = $"Level {level} Complete!\nFinal Score: {score}/{scoreToWin}";
            DestroyAllBooks();
        }

        if (newState == State.Lose)
        {
            if (loseFinalText) loseFinalText.text = $"Game Over\nFinal Score: {score}/{scoreToWin}";
            DestroyAllBooks();
        }

        UpdateHUD();
    }

    void ResetRoundOnly()
    {
        score = 0;
        misses = 0;
        DestroyAllBooks();
        UpdateHUD();
    }

    void DestroyAllBooks()
    {
        // Zorg dat je Book prefab tag echt "Book" heet in Unity
        var books = GameObject.FindGameObjectsWithTag("Book");
        foreach (var b in books) Destroy(b);
    }

    void UpdateHUD()
    {
        if (scoreText) scoreText.text = $"Score: {score}/{scoreToWin}";
        if (missText) missText.text = $"Misses: {misses}/{maxMisses}";
        if (levelText) levelText.text = $"Level: {level}";
    }
}