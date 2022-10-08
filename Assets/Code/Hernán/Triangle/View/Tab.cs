using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tab : MonoBehaviour, IPointerClickHandler
{
    ExerciseHandler _exerciseHandler;

    Image _image;
    RectTransform _rectTransform;

    bool _locked ,_on;
    Color _pasiveColor, _activeColor;

    [SerializeField] TriangleData _triangleData;
    [SerializeField] GameObject _graphicObject;
    [SerializeField] TriangleData.Type _dataType;

    void Awake() 
    {
        _exerciseHandler = FindObjectOfType<ExerciseHandler>();

        _image = GetComponent<Image>();  
        _rectTransform = GetComponent<RectTransform>();

        _pasiveColor = new Color(_image.color.r, _image.color.g, _image.color.b, 0.6f);
        _activeColor = Color.white;

        _exerciseHandler.EstablishedCurrentExercise += SetLocked;
    }

    void Start()
    {
        Deactivate();
    }

    void SetLocked()
    {
        if (_dataType == TriangleData.Type.Height || _dataType == TriangleData.Type.Area)
        {
            if (_exerciseHandler.CurrentExercise.height.value == 0f)
            {
                _locked = true;   
            }
        }
    }

    public void OnPointerClick(PointerEventData e)
    {
        if (!_locked)
        {
            AudioHandler.PlaySound("Button");
            Switch();   
        }
    }

    public void Switch()
    {
        _on = !_on;

        if (_on)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    void Activate()
    {
        _rectTransform.localPosition = new Vector3(_rectTransform.localPosition.x - 10f, _rectTransform.localPosition.y);
        _image.color = _activeColor;

        _triangleData?.Show();
        _graphicObject?.SetActive(true);
    }

    void Deactivate()
    {
        _rectTransform.localPosition = new Vector3(_rectTransform.localPosition.x + 10f, _rectTransform.localPosition.y);
        _image.color = _pasiveColor;

        _triangleData?.Hide();
        _graphicObject?.SetActive(false);
    }
}
