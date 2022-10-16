using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TriangleSideLuqui : MonoBehaviour
{
    [SerializeField] TermsReplacer _termsReplacer;
    [SerializeField] float _sideValue;
    [SerializeField] string _correspondingChar;

    [SerializeField] FormulaTermsLuqui _formulaTerms;
    TextMeshProUGUI _textComp;

    Health _health;

    Button _button;

    public float SideValue { get => _sideValue;}

    private void OnValidate()
    {
        _sideValue = _sideValue >= 0 ? _sideValue : 0;
    }

    private void OnEnable()
    {
        if(_textComp == null)
            _textComp = GetComponent<TextMeshProUGUI>();

        if (_button == null)
            _button = GetComponent<Button>();

        if (_formulaTerms == null)
            _formulaTerms = FindObjectOfType<FormulaTermsLuqui>();

        if (_health == null)
            _health = FindObjectOfType<Health>();
    }

    private void Start()
    {
        if (_textComp != null)
            _textComp.text = _sideValue.ToString();

        _button.onClick.AddListener(ReplaceToNumber);
    }

    #region Unity Events
    void ReplaceToNumber()
    {
        var result = _formulaTerms.CompareToCurrentTerm(_correspondingChar, _sideValue);


        ColorBlock colors = _button.colors;
        if (!result)
        {
            _health.Reduce();
            colors.disabledColor = Color.red;
        }
        else
        {
            colors.disabledColor = Color.green;
        }
        _button.colors = colors;
        _button.interactable = false;
    }
    #endregion
}
