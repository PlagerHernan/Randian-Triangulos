using UnityEngine;
using UnityEngine.Tilemaps;

public class HoleTriggerHammer : MonoBehaviour
{
    Character _character; 
    HoleTriggerExercise _holeTriggerExercise;

    Tilemap _tilemap;
    bool _touchEnabled, _waitingForRebuild;
    float _alpha = 0f;
    [SerializeField] ConstructionSmoke _constructionSmokePrefab;
    int _touchCount;

    private void Awake() 
    {
        _character = FindObjectOfType<Character>();
        _tilemap = GetComponentInParent<Tilemap>();
        _holeTriggerExercise = _tilemap.gameObject.GetComponentInChildren<HoleTriggerExercise>();

        LevelHandler.StartingLevel += SetInitialHoleOpacity;
    }

    void SetInitialHoleOpacity()
    {
        _tilemap.color = new Color(_tilemap.color.r, _tilemap.color.g, _tilemap.color.b, 0f);
    }

    public void EnableTouch()
    {
        _touchEnabled = true;
    }

    void DisableTouch()
    {
        _touchEnabled = false;
    }

    void OnMouseDown() 
    {
        if (_touchEnabled && !_waitingForRebuild)
        {
            _waitingForRebuild = true;
            _character.Hammer();
            Invoke("RebuildTriangle", 0.5f);
        }
    }

    //llamado en OnMouseDown()
    void RebuildTriangle()
    {   
        Instantiate(_constructionSmokePrefab, _constructionSmokePrefab.Positions[_touchCount], new Quaternion());

        //cada click o tap le agrega un sexto de opacidad
        _alpha += 0.17f;
        _tilemap.color = new Color(_tilemap.color.r, _tilemap.color.g, _tilemap.color.b, _alpha);

        _touchCount++;
        if (_touchCount > 2)
        {
            _touchCount = 0;
        }

        //si terminó la recontrucción
        if (_alpha > 1f)
        {
            StarParticle.Create();
            _holeTriggerExercise.OnTriangleExit();
            DisableTouch();
        }

        _waitingForRebuild = false;
    }
}
