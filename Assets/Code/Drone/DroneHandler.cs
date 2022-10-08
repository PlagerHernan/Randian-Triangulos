using UnityEngine;

public class DroneHandler : MonoBehaviour
{
    DroneView _droneView;

    void Awake() 
    {
        _droneView = GetComponent<DroneView>();    
    }

    void Start() 
    {
        _droneView.PlayAnimation("smokeWorking");
    }
}
