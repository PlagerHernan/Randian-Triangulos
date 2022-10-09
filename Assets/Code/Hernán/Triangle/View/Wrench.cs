using UnityEngine;
using UnityEngine.UI;

public class Wrench : MonoBehaviour
{
    Text _textComponent; 

    void Awake() 
    {
        _textComponent = GetComponentInChildren<Text>();

        ExerciseHandler.EstablishedCurrentExercise += SetText;
    }

    void SetText()
    {
        //1er caracter de la formula despejada correcta
        switch (ExerciseHandler.CurrentExercise.clearFormulas[0].equation[0])
        {
            case 'P': _textComponent.text = "Perímetro"; break;
            case 'x': _textComponent.text = "Lado faltante"; break;
            case 'A': _textComponent.text = "Área"; break;
        }
    }
}
