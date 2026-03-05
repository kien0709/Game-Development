using UnityEngine;
using UnityEngine.UI;

// De GameManager regelt de hele flow van de game
public class GameManager : MonoBehaviour
{

    // Mogelijke game states
    public enum State { Start, Playing, Win, Lose }
    public State CurrentState;

    // Referenties naar de verschillende panels
    [Header("UI Panels")]
    public GameObject startPanel;
    public GameObject hudPanel;
    public GameObject winPanel;
    public GameObject losePanel;

    // Teksten die tijdens het spelen worden aangepast
    [Header("UI Text")]
    public Text scoreText;
    public Text missText;
    public Text winFinalText;
    public Text loseFinalText;
    public Text levelText;

    [Header("Rules")]
    // Basisregels van de game
    public int baseScoreToWin = 10;     // score die je nodig hebt op level 1
    public int scoreIncreasePerLevel = 3; // extra punten voor wanneer je wint
    public int maxMisses = 5; // maximaal aantal gemiste boeken

    [Header("Difficulty (Falling speed)")]
    // moeilijkheid van de game
    public int level = 1;
    public float baseFallSpeed = 3.5f; //snelheid op level 1 
    public float fallSpeedIncreasePerLevel = 0.6f; // snelheid neemt toe per level

    // variabelen die tijdens het spelen veranderd  
    [HideInInspector] public int score = 0;
    [HideInInspector] public int misses = 0;

    // Maakt nieuwe boeken aan
    public Spawner spawner;

    // Aantal puntne die je nodig hebt om te winnen
    int scoreToWin;

    void Start()
    {

        scoreToWin = baseScoreToWin;
        // begin in startpanel
        SetState(State.Start);
        // update de hudpanel
        UpdateHUD();
    }

    // Bepaalt hoe snel boeken vallen kwa level
    public float GetCurrentFallSpeed()
    {
        return baseFallSpeed + (level - 1) * fallSpeedIncreasePerLevel;
    }

    // start de game
    public void StartGame()
    {
        // start op huidig level
        ResetRoundOnly();
        SetState(State.Playing);
    }

    // button op WinPanel next level
    public void NextLevel()
    {
        level++;
        scoreToWin = baseScoreToWin + (level - 1) * scoreIncreasePerLevel;

        ResetRoundOnly();
        SetState(State.Playing);
    }

    // Button op LosePanel opnieuw zelfde level
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

    // word aangeroepen als speler een boek vangt
    public void AddScore(int amount)
    {
        // alleen score oppakken wanneer game actief is
        if (CurrentState != State.Playing) return;

        score += amount;
        UpdateHUD();
        // check of de speler heeft gewonnen
        if (score >= scoreToWin)
            SetState(State.Win);
    }

    public void AddMiss()
    {
        if (CurrentState != State.Playing) return;

        misses += 1;
        UpdateHUD();
        // check of de player heeft veloren
        if (misses >= maxMisses)
            SetState(State.Lose);
    }

    // veranderd huide game stats
    void SetState(State newState)
    {
        CurrentState = newState;
        // toon het juiste panel
        if (startPanel) startPanel.SetActive(newState == State.Start);
        if (hudPanel) hudPanel.SetActive(newState == State.Playing);
        if (winPanel) winPanel.SetActive(newState == State.Win);
        if (losePanel) losePanel.SetActive(newState == State.Lose);

        // zet spawner uit of aan
        if (spawner != null)
        {
            // start opnieuw met spawnen als game begint
            spawner.enabled = (newState == State.Playing);
            if (newState == State.Playing)
                spawner.RestartSpawning();
        }

        // als speler wint
        if (newState == State.Win)
        {
            if (winFinalText) winFinalText.text = $"Level {level} Complete!\nFinal Score: {score}/{scoreToWin}";
            DestroyAllBooks();
        }

        // als speler verliest
        if (newState == State.Lose)
        {
            if (loseFinalText) loseFinalText.text = $"Game Over\nFinal Score: {score}/{scoreToWin}";
            DestroyAllBooks();
        }

        UpdateHUD();
    }
    // reset score en misses voor ene nieuwe ronde
    void ResetRoundOnly()
    {
        score = 0;
        misses = 0;
        DestroyAllBooks();
        UpdateHUD();
    }
    // verwijder alle boeken van de scene 
    void DestroyAllBooks()
    {
        // zoekt alle objecten met de tag "Book"
        var books = GameObject.FindGameObjectsWithTag("Book");
        // verwijder elk boek
        foreach (var b in books) Destroy(b);
    }
    // update de hud tekst: scoretext, missText en levelText
    void UpdateHUD()
    {
        if (scoreText) scoreText.text = $"Score: {score}/{scoreToWin}";
        if (missText) missText.text = $"Misses: {misses}/{maxMisses}";
        if (levelText) levelText.text = $"Level: {level}";
    }
}

// referentie = een link naar een ander object of script zodat dit script ermee kan werken.