using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float _speed;
    float _currentSpeed;
    Vector3 _initialPosition;

    void Awake() 
    {
        if (Application.isEditor)
        {
            _speed = 3f;
        }

        _initialPosition = transform.position;
        LevelHandler.StartingLevel += StartUp;
    }

    void Update()
    {
        transform.position += Vector3.down * _currentSpeed * Time.deltaTime;
    }

    void StartUp()
    {
        transform.position = _initialPosition;
        Move();
    }

    public void Stop()
    {
        _currentSpeed = 0f;
    }

    public void Move()
    {
        _currentSpeed = _speed;
    }
}
