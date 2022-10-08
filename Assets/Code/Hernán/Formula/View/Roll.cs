using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Roll : MonoBehaviour, IPointerDownHandler
{   
    FormulaHandler _formulaHandler;

    bool _touched;

    void Awake() 
    {
        _formulaHandler = GetComponentInParent<FormulaHandler>();    
    }

    /* void OnMouseDown()
    {
        print("OnMouseDown");
        StartCoroutine("Touch");
    } */

    public void OnPointerDown (PointerEventData eventData) 
    {
        //GetComponent<Image>().color = Color.black;

        StartCoroutine("Touch");
    }

    IEnumerator Touch()
    {
        _touched = true;

        yield return new WaitForSeconds(1f);

        _touched = false;
    }

    public void SlideDown()
    {
        if (_touched)
        {
            _formulaHandler.OnShowingNextEquationStep();
        }
    }
}
