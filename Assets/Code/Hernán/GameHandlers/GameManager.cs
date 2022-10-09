using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void VoidDelegate(); 
    public event VoidDelegate DecreasingHealth;
    public static event VoidDelegate PausingGame, PlayingGame;
    static bool _isGamePausing; public static bool IsGamePausing { get => _isGamePausing; }
    static bool _testMode = false; public static bool TestMode { get => _testMode; }

    void Awake() 
    {
        if (!Application.isEditor)
        {
            _testMode = false;
        }

        new LevelHandler();
        new ScoreHandler();    
    }

    public void OnDecreasingHealth()
    {
        DecreasingHealth?.Invoke();
        ScoreHandler.ReduceScore();
    }

    public static void OnPausingGame()
    {
        Time.timeScale = 0f;
        _isGamePausing = true;
        PausingGame?.Invoke();
    }

    public static void OnPlayingGame()
    {
        Time.timeScale = 1f;
        _isGamePausing = false;
        PlayingGame?.Invoke();
    }

    /* [ContextMenu("TestGameOver")]
    void TestGameOver()
    {
        Background[] _background = FindObjectsOfType<Background>();
        _background[0].Stop();
        _background[1].Stop();

        StartCoroutine(GameOver.ShowPanel(false));
    } */
}
