using UnityEngine;

public class ConstructionSmoke : MonoBehaviour
{
    Animator _animator;

    Vector3[] _smokePositions = new [] { new Vector3(-2f,3f), new Vector3(2f,2f), new Vector3(-0.5f,1.5f) };
    public Vector3[] Positions { get => _smokePositions; }

    void Awake() 
    {
        _animator = GetComponent<Animator>();   
    }

    void Start() 
    {
        Show();    
    }

    void Show()
    {
        string parameter = Random.Range(0,2).ToString();
        _animator.SetTrigger(parameter);
    }

    //llamado desde animación
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
