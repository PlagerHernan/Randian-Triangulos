using UnityEngine;
using UnityEngine.UI;

public class TriangleSetter : MonoBehaviour
{
    TriangleType _triangle; public TriangleType Triangle { get => _triangle; }

    Image[] _images;

    [SerializeField] bool _shortRuler;

    void Awake() 
    {
        _images = GetComponentsInChildren<Image>();

        _triangle.sideImages = new SideImage[3];

        SetId();
        SetSideImagesPositions();
        SetRuler();
    }

    void SetId()
    {
        string triangleName = gameObject.name.Split('_')[1];
        int.TryParse(triangleName, out _triangle.index);
    }

    void SetSideImagesPositions()
    {
        int i = 0;
        foreach (Image image in _images)
        {
            //descarta imagen del propio triángulo y de la regla
            if (image.gameObject.GetInstanceID() != gameObject.GetInstanceID() && image.gameObject.name != "Ruler")
            {
                _triangle.sideImages[i].xPosition = image.transform.localPosition.x;
                _triangle.sideImages[i].yPosition = image.transform.localPosition.y;

                i++;
            }
        }
    }

    void SetRuler()
    {
        foreach (Image image in _images)
        {
            if (image.gameObject.name == "Ruler")
            {
                _triangle.ruler.xPosition = image.transform.localPosition.x;
                _triangle.ruler.yPosition = image.transform.localPosition.y;
                _triangle.ruler.rotation = image.transform.localRotation.eulerAngles.z;
            }
        }

        _triangle.ruler.shortRuler = _shortRuler;
    }
}
