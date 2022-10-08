using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelpIinfo : MonoBehaviour
{
    static Text _headerText, _bodyText;
    static GameObject _thisGameObject;
    static HelpIinfo _thisScript;

    static bool _finishedTutorial;

    public enum NextAction
    {
        startGame,
        chooseCorrectUnclearFormula,
        chooseCorrectClearFormula, 
        replaceVariable,
        unfoldRoll,
        hammer
    }

    void Awake() 
    {
        Text[] texts = GetComponentsInChildren<Text>();  
        foreach (Text text in texts)
        {
            if (text.gameObject.name == "Header Text")
            {
                _headerText = text;
            }
            if (text.gameObject.name == "Body Text")
            {
                _bodyText = text;
            }
        }

        _thisGameObject = gameObject;
        _thisScript = this;
    }

    void Start() 
    {
        if (Application.isEditor)
        {
            _finishedTutorial = true;
        }

         Hide();
         Invoke("ShowStartTutorial", 2f);
    }

    //llamado desde Start()
    void ShowStartTutorial()
    {
        Show(NextAction.startGame);
    }

    public static void Show(NextAction action)
    {
        if (_finishedTutorial)
        {
            return;
        }

        _headerText.text = "Tutorial";
        string inputText;

        switch (action)
        {
            case NextAction.startGame: 
                    _bodyText.text = "¡Te damos la bienvenida! Para llegar a la meta tendremos que reconstruir el camino, y lo vamos a hacer con ayuda de la trigonometría. \n\n" 
                                        + "Este asistente te guiará en los primeros pasos. ¡Buena suerte y adelante!";
                    ShowPanel();
                    break;

            case NextAction.chooseCorrectUnclearFormula: 
                    _bodyText.text = "Parece que tenemos un pozo en el camino, y su forma se asemeja a la de un triángulo. \n\n" 
                                         + "Como primer paso, seleccioná la fórmula correcta. Tené en cuenta el tipo de triángulo que se muestra en pantalla.";
                    ShowPanel();
                    break;

            case NextAction.chooseCorrectClearFormula: 
                    _bodyText.text = "¡Bien elegida la fórmula! \n\n Ahora seleccioná cuál es la forma correcta de despejarla. (En otros casos, la ecuación puede venir ya despejada)"; 
                    _thisScript.StartCoroutine(WaitToShow());
                    break;
            
            case NextAction.replaceVariable: 
                    _bodyText.text = "¡Esa es la ecuación que necesitamos! \n\n Ahora reemplazá la variable subrayada por su valor correspondiente. " 
                                         + "Usá las pestañas para encontrar los distintos datos del triángulo " 
                                         + "(no siempre todas las pestañas ni todos los datos estarán disponibles).";
                    ShowPanel();
                    break;

            case NextAction.unfoldRoll: 
                    if (Application.isMobilePlatform) inputText = "(deslizá el dedo hacia abajo comenzando en la posición del rollo)";
                    else inputText = "(clic en la posición del rollo + flecha abajo)";

                    _bodyText.text = "¡Todos los valores en su lugar! \n\n Ahora desplegá aún más la hoja, las veces que sean necesarias, hasta que sepamos el valor de la incógnita "
                                        + inputText;
                    ShowPanel();
                    break;

            case NextAction.hammer: 
                    if (Application.isMobilePlatform) inputText = "Tocá la pantalla";
                    else inputText = "Hacé clic";
                    _bodyText.text = "Ahora que tenemos los cálculos hechos, pasamos a la acción. \n\n"
                                        + inputText + " en la posición del pozo, las veces que sean necesarias, hasta que el camino quede reconstruido.";
                    ShowPanel();
                    break;
        }
    }

    static void ShowPanel()
    {
        _thisGameObject.transform.GetChild(0).gameObject.SetActive(true);
        Game.OnPausingGame();
    }

    void Hide()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    //llamado desde botón
    public void Exit()
    {
        Game.OnPlayingGame();
        Hide();
    }

    static IEnumerator WaitToShow()
    {
        FormulaButton _formulaButton = FindObjectOfType<FormulaButton>();

        while (!_formulaButton.AnimFinished)
        {
            yield return null;
        }

        ShowPanel();
    }

    public static void FinishTutorial()
    {
        if (!_finishedTutorial)
        {
            _finishedTutorial = true;   
        }
    }
}
