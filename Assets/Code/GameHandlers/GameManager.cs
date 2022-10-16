using UnityEngine;
using System.Threading;
using System.Globalization;

public class GameManager : MonoBehaviour
{
    ExerciseHandler _exerciseHandler;

    public delegate void VoidDelegate(); 
    public event VoidDelegate DecreasingHealth;
    public static event VoidDelegate PausingGame, PlayingGame;
    static bool _isGamePausing; public static bool IsGamePausing { get => _isGamePausing; }
    [SerializeField] bool _testModeAux;
    static bool _testMode = false; public static bool TestMode { get => _testMode; }

    void Awake() 
    {
        if (Application.isEditor)
        {
            _testMode = _testModeAux;
        }
        else
        {
            _testMode = false;
        }

        _exerciseHandler = new ExerciseHandler();
        new LevelHandler();
        new ScoreHandler();    
    }

    void Start() 
    {
        SetCultureInfo();
        _exerciseHandler.SetExercises();
    }

    void SetCultureInfo()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("es-AR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-AR");
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
