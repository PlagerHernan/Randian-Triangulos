using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FormulaSheet : MonoBehaviour
{
    FormulaHandler _formulaHandler;
    FormulaButton _formulaButton;

    Animator _animator;
    RectTransform _sheet;

    bool _isUnrollingMore;

    void Awake() 
    {
        _formulaHandler = GetComponentInParent<FormulaHandler>();
        _formulaButton = FindObjectOfType<FormulaButton>();

        _animator = GetComponent<Animator>();    
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            if (image.gameObject.name == "Sheet")
            {
                _sheet = image.gameObject.GetComponent<RectTransform>();
            }
        }
    }

    void Start()
    {
        _formulaHandler.ChosenCorrectClearFormula += Unroll;
        _formulaHandler.ShowingNextEquationStep += UnrollMore;
    }

    void Unroll()
    {
        StartCoroutine(UnrollC());
    }

    IEnumerator UnrollC()
    {
        while (!_formulaButton.AnimFinished)
        {
            yield return null;
        }

        Show();
        AudioHandler.PlaySound("UnrollSheet");
        _animator.SetTrigger("unroll");
        Invoke("CallOnChoosingVariable", 3f);
    }

    void UnrollMore()
    {
        //_animator.Play("RollAndSheet_UnfoldMore", -1, 0f);

        _animator.enabled = false;

        if (!_isUnrollingMore)
        {
            StartCoroutine("UnrollMoreC");   
        }
    }

    IEnumerator UnrollMoreC()
    {
        _isUnrollingMore = true;

        AudioHandler.PlaySound("UnrollSheet");

        float currentPosition = _sheet.anchoredPosition.y;
        float targetPosition = _sheet.anchoredPosition.y - 50f;

        while (currentPosition > targetPosition) 
        {
            currentPosition -= 2f;
            _sheet.anchoredPosition = new Vector3(_sheet.anchoredPosition.x, currentPosition);

            if (Application.isEditor)
            {
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                yield return new WaitForSeconds(0.0001f);
            }
        }

        _isUnrollingMore = false;
    }

    void Show()
    {
        //se muestra por delante
        transform.SetSiblingIndex(2);

        //se activa
        //transform.GetChild(0).gameObject.SetActive(true);
    }

    //llamado desde UnrollC
    void CallOnChoosingVariable()
    {
        _formulaHandler.OnChoosingVariable();
    }

    /* void Hide()
    {
        //se muestra por detrás
        transform.SetSiblingIndex(1);
    } */
}
