using UnityEngine;

public class FormulaSelector : MonoBehaviour
{
    FormulaHandler _formulaHandler;

    void Awake() 
    {
        _formulaHandler = GetComponentInParent<FormulaHandler>();   
        ExerciseHandler.EstablishedCurrentExercise += Show; 
    }

    void Start()
    {
        _formulaHandler.ChosenCorrectClearFormula += Hide;
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
