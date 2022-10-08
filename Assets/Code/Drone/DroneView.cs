using UnityEngine;

public class DroneView : MonoBehaviour
{
    [SerializeField] float _smokeWorkingFrequency;
    Animator _animator; 

    void Awake() 
    {
        _animator = GetComponent<Animator>();
    } 

    public void PlayAnimation(string parameterName)
    {
        if (parameterName == "smokeWorking")
        {
            InvokeRepeating("ThrowSmoke", 0f, _smokeWorkingFrequency);   
        }
    }

    void ThrowSmoke()
    {
        _animator.SetTrigger("smokeWorking");
    }

    //llamado desde animación
    void PlayVaporSound()
    {
        //AudioHandler.PlaySound("Vapor");
    }
}
