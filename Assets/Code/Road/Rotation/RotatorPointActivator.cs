using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorPointActivator : MonoBehaviour
{
    //[Tooltip("Activa o desactiva los puntos de rotacion (true activa, false desactiva)")]
    //[SerializeField] bool _activatePoints;
    [SerializeField] Objects[] _objectsToControl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var obj in _objectsToControl)
        {
            obj.obj.SetActive(obj.setActive);
            //obj.SetActive(_activatePoints);
        }
    }
}

[System.Serializable]
public struct Objects
{
    public GameObject obj;
    public bool setActive;
}
