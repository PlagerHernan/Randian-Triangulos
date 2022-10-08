using UnityEngine;

public class Character : MonoBehaviour
{
    Animator _animator; 

    bool _movement = true;

    //Rotacion
    Vector3 _rotationTo = Vector3.zero;
    float _t = 0f;

    //Movimiento en carril
    [SerializeField] [Range(3f,6f)] float _speedSide;
    [SerializeField] float _leftLimit, _rightLimit;

    bool _movingToLeft; 
    public bool MovingToLeft 
    { 
        set
        {
            //si no está ya moviéndose, se mueve (para evitar doble movimiento)  
            if (!_movingToRight && !_movingToLeft)
            {
                _movingToLeft = value;   
            }
        }  
    }
    
    bool _movingToRight; 
    public bool MovingToRight 
    {
        set
        {
            //si no está ya moviéndose, se mueve (para evitar doble movimiento)  
            if (!_movingToRight && !_movingToLeft)
            {
                _movingToRight = value;   
            }
        }  
    }
        
    bool _back = false;

    void Awake() 
    {
        _animator = GetComponent<Animator>();

        LevelHandler.StartingLevel += ResumeMovement;

        /* if (Application.isEditor)
        {
            gameObject.AddComponent<PlayerPCController>();   
        }
        else
        {
            gameObject.AddComponent<PlayerAndroidController>();
            gameObject.AddComponent<SwipeDetector>();
        } */
    }

    void Update()
    {
        if (_movement)
        {
            if (_movingToLeft)
            {
                LeftMovement();
            } 
            else if (_movingToRight)
            {
                RightMovement();
            } 
        }
        

        //RotationControl3();
    }

    void LeftMovement()
    {
        //ida
        if (!_back && transform.position.x >= _leftLimit)
        {
            transform.position -= new Vector3(_speedSide * Time.deltaTime, 0f);
        } 
        //llegada
        else if (!_back && transform.position.x <= _leftLimit)
        {
            _back = true;
        }
        //vuelta
        else if (_back && transform.position.x < 0)
        {
            transform.position += new Vector3(_speedSide/2 * Time.deltaTime, 0f);
        }
        //finalización
        else if (_back && transform.position.x >= 0)
        {
            transform.position = Vector3.zero;
            _back = false;
            _movingToLeft = false;
        }
    }
    void RightMovement()
    {
        //ida
        if (!_back && transform.position.x <= _rightLimit)
        {
            transform.position += new Vector3(_speedSide * Time.deltaTime, 0f);
        } 
        //llegada
        else if (!_back && transform.position.x >= _rightLimit)
        {
            _back = true;
        }
        //vuelta
        else if (_back && transform.position.x > 0)
        {
            transform.position -= new Vector3(_speedSide/2 * Time.deltaTime, 0f);
        }
        //finalización
        else if (_back && transform.position.x <= 0)
        {
            transform.position = Vector3.zero;
            _back = false;
            _movingToRight = false;
        }
    }

    public void StopMovement()
    {
        _movement = false;
        _animator.SetBool("stop", true);
    }

    public void ResumeMovement()
    {
        _movement = true;
        _animator.SetBool("stop", false);
    }

    public void Hammer()
    {
        _animator.SetTrigger("hammer");
    }

    //llamado desde animación
    void Impact()
    {
        AudioHandler.PlaySound("HammerBuild");
    }
}
