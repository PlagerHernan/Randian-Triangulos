using UnityEngine;

public class FormulaPCController : FormulaController
{
    void Update()
    {
        if (!_disabled)
        {
            KeysDetect();   
        }
    }

    void KeysDetect()
    {
        if (!_blockedSides)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            {
                _formulaHandler.ShowPreviousFormula();
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                _formulaHandler.ShowNextFormula();
            } 
        }

        if (!_blockedDown)
        {
            if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
            {
                _roll.SlideDown();
            }
        }
    }
}
