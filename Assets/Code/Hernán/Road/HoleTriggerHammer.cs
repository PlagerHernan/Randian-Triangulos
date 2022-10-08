using UnityEngine;
using UnityEngine.Tilemaps;

public class HoleTriggerHammer : MonoBehaviour
{
    Character _character; 

    HoleTriggerExercise _holeTrigger;

    Tilemap _tilemap;
    bool _touchEnabled; public bool TouchEnabled { get => _touchEnabled; set => _touchEnabled = value; }
    float _alpha = 0f;
    [SerializeField] ConstructionSmoke _constructionSmokePrefab;
    int _touchCount;

    private void Awake() 
    {
        _character = FindObjectOfType<Character>();
        
        _holeTrigger = GetComponentInParent<Tilemap>().gameObject.GetComponentInChildren<HoleTriggerExercise>();

        /* _enableTouch = false;

        if(_trigger == null)
            _trigger = GetComponentInChildren<HoleTrigger>();

        _color = GetComponent<SpriteRenderer>().color;
        //empieza con un sexto de opacidad
        _color.a = 0.17f; */
    }

    private void Start() {
        _tilemap = GetComponentInParent<Tilemap>();
    }

    public void EnableTouch()
    {
        _touchEnabled = true;
    }

    void OnMouseDown() 
    {
        if (_touchEnabled)
        {
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
        //GetComponent<SpriteRenderer>().color = _tilemapColor;
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
            _holeTrigger.OnTriangleExit();
            Destroy(gameObject);
        }
    }
}
