using UnityEngine;
using UnityEngine.UI;

public class TabContainer : MonoBehaviour
{
    static VerticalLayoutGroup _verticalLayoutGroup;

    void Awake() 
    {
        _verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();   
    }

    void Start() 
    {
        ExerciseHandler.EstablishedCurrentExercise += SetInitialValues;
    }

    void SetInitialValues()
    {
        _verticalLayoutGroup.enabled = false;
        _verticalLayoutGroup.enabled = true;
    }
}
