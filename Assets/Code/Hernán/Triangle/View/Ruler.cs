using UnityEngine;
using UnityEngine.UI;

public class Ruler : MonoBehaviour
{
    Image _imageComponent;

    void Awake() 
    {
        _imageComponent = GetComponent<Image>();
    }

    public void SetPosition(float x, float y)
    {
        transform.localPosition = new Vector3(x, y);
    }

    public void SetRotation(float angle)
    {
        Quaternion quaternion = new Quaternion();
        quaternion.eulerAngles = new Vector3(0f,0f, angle);
        _imageComponent.transform.localRotation = quaternion;
    }

    public void SetSize(bool small)
    {
        if (small)
        {
            _imageComponent.fillAmount = 0.5f;   
        }
        else
        {
            _imageComponent.fillAmount = 1f; 
        }
    }
}
