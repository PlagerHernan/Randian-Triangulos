using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Triangle : MonoBehaviour
{
    ExerciseHandler _exerciseHandler;
    FormulaHandler _formulaHandler;

    TriangleSide[] _triangleSides;
    Ruler _ruler;

    Image _triangleImage;
    
    [SerializeField] Sprite[] _triangleSprites;

    List<TriangleType> _triangleTypeList;
    bool _isIsosceles; public bool IsIsosceles { get => _isIsosceles; }

    void Awake() 
    {
        _exerciseHandler = FindObjectOfType<ExerciseHandler>();
        _formulaHandler = GetComponentInParent<TriangleCanvas>().GetComponentInChildren<FormulaHandler>();

        _triangleSides = GetComponentsInChildren<TriangleSide>();
        _ruler = FindObjectOfType<Ruler>();

        _triangleImage = GetComponent<Image>();

        GetTriangleList();

        _exerciseHandler.EstablishedCurrentExercise += SetSides;
        _exerciseHandler.EstablishedCurrentExercise += SetTriangleSprite;
        _exerciseHandler.EstablishedCurrentExercise += SetRuler;

        _formulaHandler.ChoosingVariable += CallActiveSideButtons;
    }

    void GetTriangleList()
    {
        string text = Resources.Load<TextAsset>("Json/Triangles").text;
        _triangleTypeList = JsonUtility.FromJson<TriangleList>(text).triangleTypes;
    }

    void SetSides()
    {
        foreach (TriangleType triangleType in _triangleTypeList)
        {
            string charIndex = _exerciseHandler.CurrentExercise.triangleID[0].ToString();
            int.TryParse(charIndex, out int currentIndex);

            if (triangleType.index == currentIndex)
            {
                if (currentIndex == 2 || currentIndex == 3 || currentIndex == 4)
                {
                    _isIsosceles = true;
                }

                int i = 0;
                int ii;

                //el triángulo de tipo 3 es el único cuyo lado distinto es el 1ro y no el último, por ende lo recorre al revés
                if (currentIndex != 3)
                {
                    ii = 0;
                    _triangleSides[2].IsDifferentSide = true;
                }
                else
                {
                    ii = 2;
                    _triangleSides[0].IsDifferentSide = true;
                }

                foreach (TriangleSide triangleSide in _triangleSides)
                {
                    triangleSide.SetPosition(triangleType.sideImages[i].xPosition, triangleType.sideImages[i].yPosition);
                    triangleSide.SetValue(_exerciseHandler.CurrentExercise.sides[ii].variable, _exerciseHandler.CurrentExercise.sides[ii].value);

                    if(triangleSide.gameObject.name.Split('_')[1] == _exerciseHandler.CurrentExercise.height.baseSide.ToString())
                    {
                        triangleSide.IsBaseSide = true;
                    }

                    i++;
                    if (currentIndex != 3)
                    {
                        ii++;
                    }
                    else
                    {
                        ii--;
                    }
                }
            }
        }
    }

    void SetTriangleSprite()
    {
        foreach (Sprite sprite in _triangleSprites)
        {
            if (sprite.name == _exerciseHandler.CurrentExercise.triangleID)
            {
                _triangleImage.sprite = sprite;       
            }
        }
    }

    void SetRuler()
    {
        foreach (TriangleType triangleType in _triangleTypeList)
        {
            string charIndex = _exerciseHandler.CurrentExercise.triangleID[0].ToString();
            int.TryParse(charIndex, out int currentIndex);

            if (triangleType.index == currentIndex)
            {
                _ruler.SetPosition(triangleType.ruler.xPosition, triangleType.ruler.yPosition);
                _ruler.SetRotation(triangleType.ruler.rotation);
                _ruler.SetSize(triangleType.ruler.shortRuler);
            }
        }
    }

    void CallActiveSideButtons()
    {
        Invoke("ActiveSideButtons", 1f); //espera a anim subrayado
    } 

    //llamado desde CallActiveSideButtons()
    void ActiveSideButtons()
    {
        foreach (TriangleSide triangleSide in _triangleSides)
        {
            triangleSide.ActivateButton();
        }
    } 
}
