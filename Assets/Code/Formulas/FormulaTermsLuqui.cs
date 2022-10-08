using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormulaTermsLuqui : MonoBehaviour
{
    [SerializeField] Terms[] _targetTermsList;
    [SerializeField] int _currentIndex = 0;

    float _recievedNumber = 0;

    HighLightController _highLightController;
    TermsReplacer _termsReplacer;
    FormulaStage _formulaStage;

    private void Awake()
    {
        _highLightController = GetComponentInChildren<HighLightController>();
        _termsReplacer = GetComponentInChildren<TermsReplacer>();
        _formulaStage = GetComponentInParent<FormulaStage>();
    }

    private void Start()
    {
        UpdateVisuals();
    }

    public bool CompareToCurrentTerm(string term, float number)
    {
        bool result;

        if (term == _targetTermsList[_currentIndex]._termLetter)
        {
            if (number == _targetTermsList[_currentIndex]._termNumber)
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        else
        {
            result = false;
        }

        _currentIndex++;
        _recievedNumber = number;

        UpdateVisuals();

        if(_currentIndex >= _targetTermsList.Length)
        {
            Invoke("DisableGameObject", 2);
        }

        return result;
    }

    void UpdateVisuals()
    {
        _highLightController.SetPosition(_currentIndex);

        if(_currentIndex - 1 >= 0)
            _termsReplacer.ReplaceToNumber(_targetTermsList[_currentIndex - 1]._termLetter, _targetTermsList[_currentIndex - 1]._termNumber);
    }

    void DisableGameObject()
    {
        _formulaStage.SetWin();
    }
}

[Serializable]
struct Terms
{
    public string _termLetter;
    public float _termNumber;
}