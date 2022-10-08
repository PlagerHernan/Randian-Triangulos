using System.Collections;
using UnityEngine;

public class RotationPoint : MonoBehaviour
{
    [SerializeField] Vector3[] _availableDirs;
    Road _road;

    void Awake()
    {
        _road = FindObjectOfType<Road>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Character>(out var character))
        {
            character.StopMovement();
            
            StartCoroutine(StopRoad());
        }
    }

    IEnumerator StopRoad()
    {
        //espera hasta que el punto de rotación llegue al centro de la pantalla (misma posición de character)
        while (transform.position.y > 0f)
        {
             yield return null;
        }

        _road.Directions = _availableDirs;
        _road.Stop();
        _road.CanRotate = true;
    }
}
