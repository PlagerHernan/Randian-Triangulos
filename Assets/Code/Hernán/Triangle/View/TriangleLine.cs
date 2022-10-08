using UnityEngine;
using UnityEngine.UI;

public class TriangleLine : MonoBehaviour
{
    ExerciseHandler _exerciseHandler;

    Image _imageComponent;

    [SerializeField] TriangleData.Type _dataType;
    [SerializeField] Sprite[] _lineSprites;

    void Awake() 
    {
        _exerciseHandler = FindObjectOfType<ExerciseHandler>();

        _imageComponent = GetComponent<Image>();

        _exerciseHandler.EstablishedCurrentExercise += SetLine;
    }

    void SetLine()
    {
        foreach (Sprite sprite in _lineSprites)
        {
            if (sprite.name[1] == _exerciseHandler.CurrentExercise.triangleID[0])
            {
                _imageComponent.sprite = sprite;       
            }
        }
    }
}
