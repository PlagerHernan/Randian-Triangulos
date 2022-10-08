using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour
{
    static GameObject _thisGameObject;
    static FinishStar[] _finishStars;
    static Image _faceImage, _medalImage, _backgroundImage;
    static Text _titleText0, _titleText1;

    [SerializeField] Sprite _winFaceAux, _loseFaceAux, _winMedalAux, _loseMedalAux;
    static Sprite _winFace, _loseFace, _winMedal, _loseMedal;
    static Color _winColor, _loseColor;

    void Awake() 
    {
        _thisGameObject = gameObject;
        _finishStars = GetComponentsInChildren<FinishStar>();  

        _backgroundImage = GetComponent<Image>();

        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            if (image.gameObject.name == "Face")
            {
                _faceImage = image;
            }
            if (image.gameObject.name == "Medal")
            {
                _medalImage = image;
            }
        }  
        
        Text[] texts = GetComponentsInChildren<Text>();
        foreach (Text text in texts)
        {
            if (text.gameObject.name == "TitleText (0)")
            {
                _titleText0 = text;
            }
            else if (text.gameObject.name == "TitleText (1)")
            {
                _titleText1 = text;
            }
        }

        ColorUtility.TryParseHtmlString("#78E4FF", out _winColor); 
        ColorUtility.TryParseHtmlString("#FFA3A3", out _loseColor); 

        _winFace = _winFaceAux;
        _loseFace = _loseFaceAux;
        _winMedal = _winMedalAux;
        _loseMedal = _loseMedalAux;
    }

    void Start() 
    {
        HidePanel();  
    }

    public static void HidePanel()
    {
        _thisGameObject.SetActive(false);
    }

    public static IEnumerator ShowPanel(bool victory)
    {
        AudioHandler.StopMusic(); 

        yield return new WaitForSeconds(1.4f);

        _thisGameObject.SetActive(true);
        
        if (victory)
        {
            if (LevelHandler.CurrentLevel == LevelHandler.MaxLevel)
            {
                _titleText0.text = "JUEGO";
            }
            else
            {
                _titleText0.text = "NIVEL " + (LevelHandler.CurrentLevel + 1).ToString();
            }
            _titleText1.text = "COMPLETADO";

            AudioHandler.PlayMusic("VictoryTrack");
            PlayButton.SetButton(true);
            _faceImage.sprite = _winFace;
            _medalImage.sprite = _winMedal;
            _backgroundImage.color = _winColor;
            ShowStars();
        }
        else
        {
            _titleText0.text = "JUEGO";
            _titleText1.text = "TERMINADO";

            AudioHandler.PlaySound("Defeat");
            PlayButton.SetButton(false);
            _faceImage.sprite = _loseFace;
            _medalImage.sprite = _loseMedal;
            _backgroundImage.color = _loseColor;
            HideStars();
        }
    }

    static void ShowStars()
    {
        int starsToShow = CalculateStars();

        for (int i = 0; i < starsToShow; i++)
        {
            _finishStars[i].Show();
        }

        for (int i = starsToShow; i < _finishStars.Length; i++)
        {
            _finishStars[i].Hide();
        }
    }

    static void HideStars()
    {
        for (int i = 0; i < _finishStars.Length; i++)
        {
            _finishStars[i].Hide();
        }
    }

    static int CalculateStars()
    {
        if (ScoreHandler.Score < 0.4f)
        {
            return 1;
        }
        else if (ScoreHandler.Score > 0.4f && ScoreHandler.Score < 0.5f)
        {
            return 2;
        }
        else if (ScoreHandler.Score > 0.5f && ScoreHandler.Score < 0.6f)
        {
            return 3;
        }
        else if (ScoreHandler.Score > 0.6f && ScoreHandler.Score < 0.7f)
        {
            return 4;
        }
        else if (ScoreHandler.Score > 0.7f && ScoreHandler.Score < 0.8f)
        {
            return 5;
        }
        else
        {
            return 6;
        }
    }

    //llamado desde botón
    public void Exit()
    {
        Application.Quit();
    }
}
