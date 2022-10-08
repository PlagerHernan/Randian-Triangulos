using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public delegate void VoidDelegate(); 
    public event VoidDelegate DecreasingHealth;
    public static event VoidDelegate PausingGame, PlayingGame;
    [SerializeField] GameObject _pausePanel, _resumeBtn;
    static bool _isGamePausing; public static bool IsGamePausing { get => _isGamePausing; }

    /* void Awake() 
    {
        _pausePanel.SetActive(false); 
    } */

    /* public void ShowPausePanel(string text)
    {
        Time.timeScale = 0f;
        _pausePanel.GetComponentInChildren<Text>().text = text;
        _pausePanel.SetActive(true);
        if (text != "Pause")
        {
            _resumeBtn.SetActive(false);   
        }
        else
        {
            _resumeBtn.SetActive(true);
        }
    } */

    /* public void Resume()
    {
        Time.timeScale = 1f;
        _pausePanel.SetActive(false);
    } */

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
