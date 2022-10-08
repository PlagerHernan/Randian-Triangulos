using UnityEngine;
using UnityEngine.UI;

public class Wrench : MonoBehaviour
{
    ExerciseHandler _exerciseHandler;

    Text _textComponent; 

    void Awake() 
    {
        _exerciseHandler = FindObjectOfType<ExerciseHandler>();

        _textComponent = GetComponentInChildren<Text>();

        _exerciseHandler.EstablishedCurrentExercise += SetText;
    }

    void SetText()
    {
        //1er caracter de la formula despejada correcta
        switch (_exerciseHandler.CurrentExercise.clearFormulas[0].equation[0])
        {
            case 'P': _textComponent.text = "Perímetro"; break;
            case 'x': _textComponent.text = "Lado faltante"; break;
            case 'A': _textComponent.text = "Área"; break;
        }
    }
}
