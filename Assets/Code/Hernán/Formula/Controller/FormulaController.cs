using UnityEngine;

public class FormulaController : MonoBehaviour
{
    protected FormulaHandler _formulaHandler;
    protected Roll _roll;

    protected bool _disabled, _blockedSides, _blockedDown;

    protected virtual void Start() 
    {
        _formulaHandler = GetComponent<FormulaHandler>();
        _roll = GetComponentInChildren<Roll>();

        Game.PausingGame += Disable;
        Game.PlayingGame += Enable;
        
        _formulaHandler.ChosenCorrectUnclearFormula += LockSides;  
        _formulaHandler.ChosenCorrectUnclearFormula += CallUnlockSides;

        _formulaHandler.ChosenCorrectClearFormula += LockSides;    

        LockDown();
        _formulaHandler.CompletedReplacements += UnLockDown;

        _formulaHandler.ShowingNextEquationStep += LockDown;
        _formulaHandler.ShowingNextEquationStep += CallUnlockDown;

        _formulaHandler.CompletedEquationSteps += LockDown;
    }

    void Disable()
    {
        _disabled = true; 
    }

    void Enable()
    {
        _disabled = false;
    }

    void LockSides()
    {
        _blockedSides = true;
    }

    void CallUnlockSides()
    {
        Invoke("UnlockSides", 1f);
    }

    void UnlockSides()
    {
        _blockedSides = false;
    }

    void LockDown()
    {
        _blockedDown = true;
    }

    void CallUnlockDown()
    {
        Invoke("UnLockDown", 0.5f);
    }

    void UnLockDown()
    {
        _blockedDown = false;
    }
}
