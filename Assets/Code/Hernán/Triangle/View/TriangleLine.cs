using UnityEngine;
using UnityEngine.UI;

public class TriangleLine : MonoBehaviour
{
    Image _imageComponent;

    [SerializeField] TriangleData.Type _dataType;
    [SerializeField] Sprite[] _lineSprites;

    void Awake() 
    {
        _imageComponent = GetComponent<Image>();

        ExerciseHandler.EstablishedCurrentExercise += SetLine;
    }

    void SetLine()
    {
        foreach (Sprite sprite in _lineSprites)
        {
            if (sprite.name[1] == ExerciseHandler.CurrentExercise.triangleID[0])
            {
                _imageComponent.sprite = sprite;       
            }
        }
    }
}
