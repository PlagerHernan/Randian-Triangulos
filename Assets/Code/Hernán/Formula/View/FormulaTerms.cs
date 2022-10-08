using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FormulaTerms : MonoBehaviour
{
    FormulaHandler _formulaHandler;
    FadeText _fadeText;

    Animator _animator;
    Image _pencil, _highlight;

    [SerializeField] GameObject _prefabTerm, _starParticle;

    string _correctFormula;
    List<Term> _termsToReplace;
    const string _charactersToReplace = "abcdehP";
    string _variablesToReplace;
    int _termsToReplaceCount = -1;
    char _currentUnderlinedTerm; public char CurrentUnderlinedTerm { get => _currentUnderlinedTerm; }
    Vector3 _highlightStartPosition, _pencilStartPosition;
    bool _termsAreClean;
    Text _equationPreviousStepText;

    void Awake() 
    {
        _formulaHandler = GetComponentInParent<FormulaHandler>();
        _fadeText = GetComponentInParent<FadeText>();

        _animator = GetComponent<Animator>();
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            if (image.gameObject.name == "Pencil")
            {
                _pencil = image;
            }
            else if (image.gameObject.name == "Highlight")
            {
                _highlight = image;
            }
        }

        _formulaHandler.ChosenCorrectClearFormula += SetTerms;
        _formulaHandler.ChoosingVariable += UnderlineTerm; 
        _formulaHandler.ShowingNextEquationStep += SetEquationStepText;
    }

    void Start() 
    {
        _highlightStartPosition = _highlight.transform.localPosition;
        _pencilStartPosition = _pencil.transform.localPosition;

        _pencil.enabled = false;
        _highlight.enabled = false;
    }

    void SetTerms()
    {
        _correctFormula = _formulaHandler.CorrectClearFormula;

        _termsToReplace = new List<Term>();

        //recorre cada char de la formula correcta
        for (int i = 0; i < _correctFormula.Length; i++)
        {
            //crea instancia de prefab (objeto simple con componente texto) 
            GameObject termObj = Instantiate(_prefabTerm, transform.position, new Quaternion(), transform);
            termObj.transform.position = Vector3.zero;

            Text textComponent = termObj.GetComponent<Text>();
            //lo hace transparente (para que luego fadeText lo haga aparecer de a poco)
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 0f); 
            //asigna el char al texto
            textComponent.text = _correctFormula[i].ToString();

            //si el char pertenece a los que deben reemplazarse, agrega objeto a la lista
            if (i != 0 && _charactersToReplace.Contains(_correctFormula[i].ToString()))
            {
                Term term;
                //si el caracter anterior es un número, el término está siendo multiplicado
                if (int.TryParse(_correctFormula[i-1].ToString(), out int result))
                {
                    term = new Term(termObj, true); 
                }
                else
                {
                    term = new Term(termObj, false);
                }

                _termsToReplace.Add(term);
                
                _variablesToReplace += _correctFormula[i].ToString();
            }
        } 
    }

    //llamado también desde ReplaceTerm
    public void UnderlineTerm()
    {
        _termsToReplaceCount++;

        //si ya terminó de subrayar todos los términos
        if (_termsToReplaceCount == _termsToReplace.Count)
        {
             _highlight.enabled = false;
             _formulaHandler.OnCompletedReplacements();
             return;
        }

        _pencil.transform.SetParent(_termsToReplace[_termsToReplaceCount].termObject.transform);
        _highlight.transform.SetParent(_termsToReplace[_termsToReplaceCount].termObject.transform);

        _highlight.transform.localPosition = _highlightStartPosition;
        _pencil.transform.localPosition = _pencilStartPosition;

        _pencil.enabled = true;
        _highlight.enabled = true;

        _animator.SetTrigger("underline");
        AudioHandler.PlaySound("Underline");
        Invoke("DeactivePencil", 1f);

        _currentUnderlinedTerm = _variablesToReplace[_termsToReplaceCount];
    }

    //llamado desde UnderlineTerm()
    void DeactivePencil()
    {
        _pencil.enabled = false;

        if (_termsToReplaceCount == 0)
        {
            HelpIinfo.Show(HelpIinfo.NextAction.replaceVariable);   
        }
    }

    public void ReplaceTerm(float value)
    {
        string text;
        if (_termsToReplace[_termsToReplaceCount].multiplied)
        {
            text = "." + value.ToString();
        }
        else
        {
            text = value.ToString();
        }

        GameObject termObj = _termsToReplace[_termsToReplaceCount].termObject;
        termObj.GetComponent<Text>().text = text;

        IncreaseWidth(value);

        ScoreHandler.AddScore();

        StarParticle.Create(termObj.transform.position);
        Invoke("UnderlineTerm", 1.5f);
    }

    void IncreaseWidth(float value)
    {
        //cuantos mas caracteres tenga, mas aumenta el ancho del RT del término
        RectTransform rectTranTerm = _termsToReplace[_termsToReplaceCount].termObject.GetComponent<RectTransform>();
        if (value.ToString().Length == 2)
        {
            rectTranTerm.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 60f);
        }
        else if (value.ToString().Length == 3)
        {
            rectTranTerm.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 70f);
        }
        else if (value.ToString().Length >= 4)
        {
            rectTranTerm.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 90f);
        }
    }

    public void SetEquationStepText()
    {
        if (!_termsAreClean)
        {
            CleanTerms();
        }

        StartCoroutine(_fadeText.FadeOut(_equationPreviousStepText, true));

        //crea nuevo texto con el actual paso de la ecuación
        GameObject newText = Instantiate(_equationPreviousStepText.gameObject, _equationPreviousStepText.transform.position, new Quaternion(), transform);
        RectTransform rectTranNewText = newText.GetComponent<RectTransform>();
        rectTranNewText.anchoredPosition = new Vector2(rectTranNewText.anchoredPosition.x, rectTranNewText.anchoredPosition.y + 60f);
        newText.GetComponent<Text>().text = _formulaHandler.CurrentEquationStep;

        //para la próxima, el nuevo texto será el viejo
        _equationPreviousStepText = newText.GetComponent<Text>();
    }

    public void CleanTerms()
    {
        Text[] terms = GetComponentsInChildren<Text>();

        _equationPreviousStepText = terms[0];
        _equationPreviousStepText.gameObject.name = "StepEquation";
        LetterSpacing letterSpacing = _equationPreviousStepText.gameObject.AddComponent<LetterSpacing>();
        letterSpacing.spacing = 5f;

        //destruye todos los textos excepto el 1ro
        for (int i = 0; i < terms.Length; i++)
        {
            if (i != 0)
            {
                _equationPreviousStepText.text += terms[i].text;
                Destroy(terms[i].gameObject);
            }
        }

        GetComponent<HorizontalLayoutGroup>().enabled = false;
        _equationPreviousStepText.rectTransform.localPosition = Vector3.zero;

        _termsAreClean = true;
    }
}

public struct Term
{
    public GameObject termObject;
    public bool multiplied;

    public Term(GameObject newTermObject, bool isMultiplied)
    {
        termObject = newTermObject;
        multiplied = isMultiplied;
    }
}
