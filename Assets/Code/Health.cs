using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    Game _game;

    Image[] _children;
    Animator _animator;

    [SerializeField] int _healthPoints;
    int _initialHealthPoints;

    void Awake() 
    {
        _game = FindObjectOfType<Game>();  

        _children = GetComponentsInChildren<Image>();  
        _animator = GetComponent<Animator>();

        _game.DecreasingHealth += PlayReduceAnimation;
    }

    void Start() 
    {
        _initialHealthPoints = _healthPoints;
    }

    void PlayReduceAnimation()
    {
        _animator.Play("LifeBar_Reduce");
        TriangleCanvas.ActivateBlockingCover();
        AudioHandler.PlaySound("Mistake");
    }

    //llamado desde animación
    public void Reduce()
    {
        _healthPoints --;

        if (_healthPoints >= 0)
        {
            GameObject child = transform.GetChild(_healthPoints).gameObject;
            child.SetActive(false);

            if (_healthPoints == 0)
            {
                StartCoroutine(GameOver.ShowPanel(false));
            }
        }
    }

    //llamado desde animación
    public void DeactivateBlockingCover()
    {
        TriangleCanvas.DeactivateBlockingCover();
    }

    /* void HealthReset()
    {
        foreach (Image child in _children)
        {
            child.gameObject.SetActive(true);
        }

        _healthPoints = _initialHealthPoints;
    } */
}
