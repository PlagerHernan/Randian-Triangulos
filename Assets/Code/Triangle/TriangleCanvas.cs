using UnityEngine;
using System.Collections;

public class TriangleCanvas : MonoBehaviour
{
    static Canvas _canvas;
    static GameObject _blockingCover; 
    static TriangleCanvas _thisScript;

    void Awake() 
    {
        _canvas = GetComponent<Canvas>();
        _blockingCover = transform.GetChild(transform.childCount-1).gameObject; 
        _thisScript = this;
    }

    void Start() 
    {
        Hide();
        DeactivateBlockingCover();
    }

    public static void Show()
    {
        _canvas.enabled = true;

        if (ExerciseHandler.CurrentExercise.id == 0)
        {
            _thisScript.StartCoroutine(CallTutorial());
        }
    }

    public static void Hide()
    {
        _canvas.enabled = false;
    }

    public static void ActivateBlockingCover()
    {
        _blockingCover.SetActive(true);
    }

    public static void DeactivateBlockingCover()
    {
        if (GameManager.TestMode)
        {
            return;
        }
        
        _blockingCover.SetActive(false);
    }

    static IEnumerator CallTutorial()
    {
        yield return new WaitForSeconds(1f);

        HelpIinfo.Show(HelpIinfo.NextAction.chooseCorrectUnclearFormula);
    }
}
