using UnityEngine;
using UnityEngine.UI;

public class UIPointsOfReference : MonoBehaviour
{
    Image[] _points;
    [SerializeField] Sprite _fullPoint, _emptyPoint;

    void Awake() 
    {
        _points = GetComponentsInChildren<Image>();
    }

    void Start() 
    {
        SetCurrentPoint(0);    
    }

    public void SetCurrentPoint(int currentFormula)
    {
        for (int i = 0; i < _points.Length; i++)
        {
            if (i == currentFormula)
            {
               _points[i].sprite = _fullPoint;  
            }
            else
            {
                _points[i].sprite = _emptyPoint;
            }
        } 
    }
}
