using UnityEngine;

public class TriangleCanvas : MonoBehaviour
{
    //static Object prefab;
    static Canvas _canvas;
    static GameObject _blockingCover; 

    /* public static TriangleCanvas Create(float StarPointsShine)
    {
        //DestroyOldCanvas();

        prefab = Resources.Load("TriangleCanvas");
        GameObject newObject = Instantiate(prefab) as GameObject;

        newObject.GetComponentInChildren<StarPoints>().Brightness = StarPointsShine;

        TriangleCanvas triCanvasObject = newObject.GetComponent<TriangleCanvas>();
        return triCanvasObject;
    } */

    void Awake() 
    {
        _canvas = GetComponent<Canvas>();
        _blockingCover = transform.GetChild(transform.childCount-1).gameObject; 
    }

    void Start() 
    {
        Hide();
        DeactivateBlockingCover();

        //Invoke("CallTutorial", 1f);
    }

    public static void Show()
    {
        _canvas.enabled = true;
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

    //llamado en Start()
    /* void CallTutorial()
    {
        HelpIinfo.Show(HelpIinfo.NextAction.chooseCorrectUnclearFormula);
    } */

    /* static void DestroyOldCanvas()
    {
        TriangleCanvas oldCanvas = FindObjectOfType<TriangleCanvas>();
        if (oldCanvas != null)
        {
            Destroy(oldCanvas.gameObject);
        }
    } */
}
