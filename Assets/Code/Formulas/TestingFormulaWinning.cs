using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingFormulaWinning : MonoBehaviour
{
    FormulaStage _formulaStage;

    private void Awake()
    {
        _formulaStage = GetComponentInParent<FormulaStage>();
    }

    void Start()
    {
        Invoke("DisableGameObject", 2);
    }

    void DisableGameObject()
    {
        _formulaStage.SetWin();
    }
}
