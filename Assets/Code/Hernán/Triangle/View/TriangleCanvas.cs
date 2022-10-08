using UnityEngine;

public class TriangleCanvas : MonoBehaviour
{
    static Object prefab;
    static GameObject _blockingCover;

    public static TriangleCanvas Create(float StarPointsShine)
    {
        //DestroyOldCanvas();

        prefab = Resources.Load("TriangleCanvas");
        GameObject newObject = Instantiate(prefab) as GameObject;

        newObject.GetComponentInChildren<StarPoints>().Brightness = StarPointsShine;

        TriangleCanvas triCanvasObject = newObject.GetComponent<TriangleCanvas>();
        return triCanvasObject;
    }

    void Awake() 
    {
        _blockingCover = transform.GetChild(transform.childCount-1).gameObject; 
    }

    void Start() 
    {
        DeactivateBlockingCover();

        Invoke("CallTutorial", 1f);
    }

    static void DestroyOldCanvas()
    {
        /* TriangleCanvas[] canvas = FindObjectsOfType<TriangleCanvas>();    
        foreach (TriangleCanvas canva in canvas)
        {
            if (canva != this)
            {
                Destroy(canva.gameObject);
            }
        } */

        TriangleCanvas oldCanvas = FindObjectOfType<TriangleCanvas>();
        if (oldCanvas != null)
        {
            Destroy(oldCanvas.gameObject);
        }
    }

    public static void ActivateBlockingCover()
    {
        _blockingCover.SetActive(true);
    }

    public static void DeactivateBlockingCover()
    {
        _blockingCover.SetActive(false);
    }

    //llamado en Start()
    void CallTutorial()
    {
        HelpIinfo.Show(HelpIinfo.NextAction.chooseCorrectUnclearFormula);
    }
}
