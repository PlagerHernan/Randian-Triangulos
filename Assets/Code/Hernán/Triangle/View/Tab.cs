using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tab : MonoBehaviour, IPointerClickHandler
{
    Image _image;
    RectTransform _rectTransform;

    bool _locked ,_on;
    Color _pasiveColor, _activeColor;

    [SerializeField] TriangleData _triangleData;
    [SerializeField] GameObject _graphicObject;
    [SerializeField] TriangleData.Type _dataType;

    void Awake() 
    {
        _image = GetComponent<Image>();  
        _rectTransform = GetComponent<RectTransform>();

        _pasiveColor = new Color(_image.color.r, _image.color.g, _image.color.b, 0.6f);
        _activeColor = Color.white;

        ExerciseHandler.EstablishedCurrentExercise += SetInitialValues;
    }

    void SetInitialValues()
    {
        SetBlocking();
        Deactivate();
        _on = false;
    }

    void SetBlocking()
    {
        if ((_dataType == TriangleData.Type.Height || _dataType == TriangleData.Type.Area) && ExerciseHandler.CurrentExercise.height.value == 0f)
        {
            _locked = true;
        }
        else
        {
            _locked = false;
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
