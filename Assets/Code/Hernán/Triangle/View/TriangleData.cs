using UnityEngine;
using UnityEngine.UI;

public class TriangleData : MonoBehaviour
{
    GameManager _game;
    FormulaTerms _formulaTerms;
    FormulaHandler _formulaHandler;

    Text _textComponent; 
    Button _button;
    float _value;
    bool _isAlreadySelected;

    public enum Type
    {
        Sides,
        Area, 
        Perimeter, 
        Height
    }

    [SerializeField] Type _dataType; public Type DataType { get => _dataType; }

    void Awake() 
    {
        _game = FindObjectOfType<GameManager>();
        _formulaTerms = FindObjectOfType<FormulaTerms>();
        _formulaHandler = FindObjectOfType<FormulaHandler>();

        _textComponent = GetComponentInChildren<Text>();

        ExerciseHandler.EstablishedCurrentExercise += SetData;

        _formulaHandler.ChoosingVariable += ActivateButton;

        _formulaHandler.CompletedReplacements += DeactivateButton;
    }

    void SetData()
    {
        switch (_dataType)
        {
            case Type.Perimeter: 
                SetValue(Type.Perimeter);
                break;

            case Type.Height: 
                SetValue(Type.Height);
                break;

            case Type.Area:
                SetValue(Type.Area);
                break;
        }
    }

    void SetValue(Type type)
    {
        char unknownVariable = 'x';

        if (type == Type.Area)
        {
            _value = CalculateArea();
            _textComponent.text = _value + " m²";
            unknownVariable = 'A';
        }
        else if (type == Type.Perimeter)
        {
            _value = CalculatePerimeter();   
            _textComponent.text = _value + " m";
            unknownVariable = 'P';
        }
        else if (type == Type.Height)
        {
            _value = ExerciseHandler.CurrentExercise.height.value;
            _textComponent.text = _value + " m";
        }

        //si el objetivo del ejercicio es este tipo de dato, el usuario no puede ver su valor
        if (ExerciseHandler.CurrentExercise.clearFormulas[0].equation[0] == unknownVariable)
        {
            _textComponent.text = "?";
        }

        if (_button == null)
        {
            _button = gameObject.AddComponent<Button>();
            DeactivateButton();
            _button.onClick.AddListener(Verify);   
        }
    }

    float CalculatePerimeter()
    {
        float perimeter = 0f;
        
        for (int i = 0; i < 3; i++)
        {
            perimeter += ExerciseHandler.CurrentExercise.sides[i].value;
        }

        return perimeter;
    }

    float CalculateArea()
    {
        float baseValue = 0f;
        
        if (ExerciseHandler.CurrentExercise.height.baseSide == 'A')
        {
            baseValue = ExerciseHandler.CurrentExercise.sides[0].value;
        }
        else if (ExerciseHandler.CurrentExercise.height.baseSide == 'B')
        {
            baseValue = ExerciseHandler.CurrentExercise.sides[1].value;
        }
        else if (ExerciseHandler.CurrentExercise.height.baseSide == 'C')
        {
            baseValue = ExerciseHandler.CurrentExercise.sides[2].value;
        }

        float area = (baseValue * ExerciseHandler.CurrentExercise.height.value) / 2;

        return area;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    void Verify()
    {
        AudioHandler.PlaySound("Button");

        if (_dataType == Type.Perimeter && _formulaTerms.CurrentUnderlinedTerm == 'P')
        {
            Select();
        }
        else if (_dataType == Type.Height && _formulaTerms.CurrentUnderlinedTerm == 'h')
        {
            Select();
        }
        else if (_dataType == Type.Area && _formulaTerms.CurrentUnderlinedTerm == 'A')
        {
            Select();
        }
        else
        {
            print("Error. Este dato no corresponde al término subrayado");
            _game.OnDecreasingHealth();
        }
    }

    void Select()
    {
        _isAlreadySelected = true;
        _formulaTerms.ReplaceTerm(_value);
    }

    void DeactivateButton()
    {
        if (_button != null)
        {
            _button.enabled = false;
        }
    }

    void ActivateButton()
    {
        if (_button != null)
        {
            _button.enabled = true;
        }
    }
}
