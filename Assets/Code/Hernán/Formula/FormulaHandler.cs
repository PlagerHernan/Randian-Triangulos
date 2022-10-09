using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

public class FormulaHandler : MonoBehaviour
{
    GameManager _game;
    FormulaView _formulaView;
    UIPointsOfReference _UIPointsOfReference;
    FormulaButton _formulaButton;

    public delegate void VoidDelegate(); 
    public event VoidDelegate ChosenCorrectUnclearFormula, ChosenCorrectClearFormula, ChoosingVariable, 
                                CompletedReplacements ,ShowingNextEquationStep, CompletedEquationSteps;
    int _centeredCurrentFormulaIndex;  
    List<Formula> _currentFormulas;
    string _correctClearFormula; public string CorrectClearFormula { get => _correctClearFormula; }

    bool _waitingWhileShowing;
    bool _currentFormulasAreClear;
    char _differentSideVariable; public char DifferentSideVariable { get => _differentSideVariable; }
    int _equationStepsCount;
    string _currentEquationStep; public string CurrentEquationStep { get => _currentEquationStep; }

    void Awake() 
    {
        _game = FindObjectOfType<GameManager>();
        _formulaView = GetComponentInChildren<FormulaView>();    
        _UIPointsOfReference = GetComponentInChildren<UIPointsOfReference>();
        _formulaButton = GetComponentInChildren<FormulaButton>();

        if (Application.isEditor)
        {
            gameObject.AddComponent<FormulaPCController>();   
        }
        else
        {
            gameObject.AddComponent<FormulaAndroidController>();
            gameObject.AddComponent<SwipeDetector>();
        }

        ExerciseHandler.EstablishedCurrentExercise += SetFormulas;
    }

    void Start()
    {
        this.ChosenCorrectUnclearFormula += SetFormulas;
        this.ChosenCorrectUnclearFormula += ShowNextFormula;
    }

    void SetFormulas()
    {
        //si aún no se despejaron (1ra vez que entra)
        if (!_currentFormulasAreClear)
        {
            _correctClearFormula = ExerciseHandler.CurrentExercise.clearFormulas[0].equation;

            //si hay no despejadas en excel
            if (ExerciseHandler.CurrentExercise.unclearFormulas != null)
            {
                _currentFormulas = ExerciseHandler.CurrentExercise.unclearFormulas;
            }
            //si solo hay despejadas
            else
            {
                _currentFormulas = ExerciseHandler.CurrentExercise.clearFormulas;
                _currentFormulasAreClear = true;
            }   

            if (_currentFormulas[0].equation.Contains("2"))
            {
                _differentSideVariable = _currentFormulas[0].equation[_currentFormulas[0].equation.Length - 1];
            }
        }

        //desordena la posición de las fórmulas
        _currentFormulas = _currentFormulas.OrderBy( x => Random.value ).ToList( );

        //SeparateInTerms();

        _formulaView.SetTextButton(_currentFormulas[0].equation);
    }

    /* void SeparateInTerms()
    {
        string correctFormula = _exerciseHandler.CurrentExercise.formulas[2].equation;
        _terms = Regex.Split(correctFormula, @"([*()\^\/]|(?<!E)[\+\-=])");
    } */

    //llamado también desde RightButton
    public void ShowNextFormula()
    {
        if (!_waitingWhileShowing)
        {
            StartCoroutine(ShowNextFormulaC());   
        }
    }

    //llamado también desde LeftButton
    public void ShowPreviousFormula()
    {
        if (!_waitingWhileShowing)
        {
            StartCoroutine(ShowPreviousFormulaC());   
        }
    }

    IEnumerator ShowNextFormulaC()
    {
        while (!_formulaButton.AnimFinished)
        {
            yield return null;
        }

        _waitingWhileShowing = true;

        if (_centeredCurrentFormulaIndex < _currentFormulas.Count - 1)
        {
             _centeredCurrentFormulaIndex++;
        }
        else
        {
            _centeredCurrentFormulaIndex = 0; 
        }
        
        _formulaView.SetTextButton(_currentFormulas[_centeredCurrentFormulaIndex].equation);
        _formulaView.MoveToSide("right");
        _UIPointsOfReference.SetCurrentPoint(_centeredCurrentFormulaIndex);

        yield return new WaitForSeconds(0.3f);

        _waitingWhileShowing = false;
    }

    IEnumerator ShowPreviousFormulaC()
    {
        _waitingWhileShowing = true;

        if (_centeredCurrentFormulaIndex > 0)
        {
             _centeredCurrentFormulaIndex--;
        }
        else
        {
            _centeredCurrentFormulaIndex = _currentFormulas.Count - 1; 
        }
        
        _formulaView.SetTextButton(_currentFormulas[_centeredCurrentFormulaIndex].equation);
        _formulaView.MoveToSide("left");
        _UIPointsOfReference.SetCurrentPoint(_centeredCurrentFormulaIndex);

        yield return new WaitForSeconds(0.3f);

        _waitingWhileShowing = false;
    }

    //llamado desde Formula Button
    public void Verify()
    {
        //si la elección del usuario es correcta
        if (_currentFormulas[_centeredCurrentFormulaIndex].correct)
        {
            ScoreHandler.AddScore();

            //si las fórmulas ya estaban despejadas, pasa a siguiente etapa
            if (_currentFormulasAreClear)
            {
                ChosenCorrectClearFormula?.Invoke();   
            }
            //si las fórmulas no estaban despejadas, las despeja
            else
            {
                _currentFormulas = ExerciseHandler.CurrentExercise.clearFormulas;
                _currentFormulasAreClear = true;
                //SetFormulas();
                ChosenCorrectUnclearFormula?.Invoke();
                HelpIinfo.Show(HelpIinfo.NextAction.chooseCorrectClearFormula);
            }
        }
        //si la elección del usuario es incorrecta
        else
        {
            _game.OnDecreasingHealth();
        }
    } 

    public void OnChoosingVariable()
    {
        ChoosingVariable?.Invoke();
    }

    public void OnCompletedReplacements()
    {
        CompletedReplacements?.Invoke();
        HelpIinfo.Show(HelpIinfo.NextAction.unfoldRoll);
    }

    public void OnShowingNextEquationStep()
    {
        if (SetNextEquationStep())
        {
            ShowingNextEquationStep?.Invoke();   
        }
        else
        {
            CompletedEquationSteps?.Invoke();
        }
    }

    bool SetNextEquationStep()
    {
        if (_equationStepsCount < ExerciseHandler.CurrentExercise.equationSteps.Count)
        {
            _currentEquationStep = ExerciseHandler.CurrentExercise.equationSteps[_equationStepsCount];
            _equationStepsCount++;
            return true;
        }
        else
        {
            return false;
        }
    }
}
