using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FormulaButton : MonoBehaviour
{
    FormulaHandler _formulaHandler;

    Button _button;
    Animator _animator;
    Text _textComponent;

    bool _animFinished = true; public bool AnimFinished { get => _animFinished; }

    void Awake() 
    {
        _formulaHandler = GetComponentInParent<FormulaHandler>();

        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();    
        _textComponent = GetComponentInChildren<Text>();
        
        _formulaHandler.ChosenCorrectUnclearFormula += CorrectChoice;
        _formulaHandler.ChosenCorrectClearFormula += CorrectChoice;
    }
    
    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void UpdateText(string formulaText)
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine("UpdateTextC", formulaText);   
        }
    }

    IEnumerator UpdateTextC(string formulaText)
    {
        while (!_animFinished)
        {
            yield return null;
        }

        _textComponent.text = formulaText;
    }

    void CorrectChoice()
    {
        AudioHandler.PlaySound("CorrectFormula");

        _animFinished = false;
        _button.interactable = false;

        _animator.SetTrigger("correct");
        Invoke("SetAnimFinished", 1f);
    }

    //llamado por Invoke en CorrectChoice()
    void SetAnimFinished()
    {
        _button.interactable = true;
        _animFinished = true;
    }
}
