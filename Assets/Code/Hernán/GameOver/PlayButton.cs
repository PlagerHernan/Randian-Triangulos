using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    static Button _button;
    static Text _text;

    void Awake() 
    {
        _button = GetComponent<Button>();   
        _text = GetComponentInChildren<Text>();
    }

    public static void SetButton(bool victory) 
    {
        _button.onClick.RemoveAllListeners();

        if (victory)
        {
            if (LevelHandler.CurrentLevel < LevelHandler.MaxLevel)
            {
                _text.text = "PRÓXIMO NIVEL";
                _button.onClick.AddListener(PlayNextLevel); 
            }
            else
            {
                _text.text = "VOLVER A JUGAR";
                _button.onClick.AddListener(RestartGame); 
            }
        }
        else
        {
            _text.text = "REINICIAR NIVEL";
            _button.onClick.AddListener(RestartLevel); 
        }
    }

    static void PlayNextLevel()
    {
        LevelHandler.CurrentLevel++;
        CallStartingLevel();
    }

    static void RestartGame()
    {
        LevelHandler.CurrentLevel = 0;
        CallStartingLevel();
    }

    static void RestartLevel()
    {
        CallStartingLevel();
    }

    static void CallStartingLevel()
    {
        LevelHandler.OnStartingLevel();
        GameOver.HidePanel();
    }
}
