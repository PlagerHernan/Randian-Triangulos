using UnityEngine;
using UnityEngine.UI;

public class TriangleSide : MonoBehaviour
{
    Game _game;
    FormulaHandler _formulaHandler;
    FormulaTerms _formulaTerms;
    Triangle _triangle;

    Text _textComponent;
    Image _imageComponent;
    Button _button;

    char _variable;
    float _value;
    bool _isDifferentSide; public bool IsDifferentSide { set => _isDifferentSide = value; }
    bool _isBaseSide; public bool IsBaseSide { set => _isBaseSide = value; }
    bool _isAlreadySelected;
    [SerializeField] Sprite _xSprite, _defaultSprite;

    void Awake() 
    {
        _game = FindObjectOfType<Game>();
        _formulaHandler = FindObjectOfType<FormulaHandler>();
        _formulaTerms = FindObjectOfType<FormulaTerms>();
        _triangle = GetComponentInParent<Triangle>();

        _textComponent = GetComponentInChildren<Text>();    
        _imageComponent = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    void Start() 
    {
        _formulaHandler.CompletedReplacements += DeactivateButton;
    }

    public void SetPosition(float x, float y)
    {
        transform.localPosition = new Vector3(x, y);
    }

    public void SetValue(char variableLetter, float valueNumber)
    {
        if (_button != null)
        {
            DeactivateButton();
        }

        _variable = variableLetter;
        _value = valueNumber;

        if (_variable == 'x')
        {
            _textComponent.text = "x"; 
            _imageComponent.sprite = _xSprite;

            _button = null;
        }
        else if (_variable == '?')
        {
            _imageComponent.enabled = false;

            _button = null;
        }
        else
        {
            _textComponent.text = valueNumber.ToString() + " m";
            _imageComponent.sprite = _defaultSprite;

            _button = GetComponent<Button>();
            _button.onClick.AddListener(Verify);
        }
    }

    void DeactivateButton()
    {
        if (_button != null)
        {
            _button.enabled = false;
        }
    }

    public void ActivateButton()
    {
        if (_button != null)
        {
            _button.enabled = true;
        }
    }

    void Verify()
    {
        AudioHandler.PlaySound("Button");

        if (!_isAlreadySelected && (_formulaTerms.CurrentUnderlinedTerm != 'P' && _formulaTerms.CurrentUnderlinedTerm != 'h'))
        {
            //Si el objetivo es el área y se subraya la base
            if (_formulaHandler.CorrectClearFormula[0] == 'A' && _formulaTerms.CurrentUnderlinedTerm == 'b')
            {
                if (_isBaseSide)
                {
                    SelectSide();
                }
                else
                {
                    print("Error. Este no es el lado base");
                    _game.OnDecreasingHealth();
                }
                return;
            }
            if (!_triangle.IsIsosceles || _formulaHandler.CorrectClearFormula == "P=a+b+c")
            {
                SelectSide();
            }
            else
            {
                if (_formulaTerms.CurrentUnderlinedTerm == _formulaHandler.DifferentSideVariable)
                {
                    if (_isDifferentSide)
                    {
                        SelectSide();
                    }
                    else
                    {
                        print("Error. Este no es el lado diferente");
                        _game.OnDecreasingHealth();
                    }
                }
                else
                {
                    if (_isDifferentSide)
                    {
                        print("Error. Este es el lado diferente");
                        _game.OnDecreasingHealth();
                    }
                    else
                    {
                        SelectSide();
                    }
                }
            }
        }
        else
        {
            print("Error. Lado ya seleccionado o término subrayado corresponde a perímetro o altura");
            _game.OnDecreasingHealth();
        }
    }

    void SelectSide()
    {
        _isAlreadySelected = true;
        _formulaTerms.ReplaceTerm(_value);
    }
}
