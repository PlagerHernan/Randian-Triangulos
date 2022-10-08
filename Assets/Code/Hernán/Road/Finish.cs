using UnityEngine;

public class Finish : MonoBehaviour
{
    Character _character;
    Background[] _background; 

    void Awake() 
    {
        _character = FindObjectOfType<Character>();
        _background = FindObjectsOfType<Background>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<Character>() != null)
        {
            OnFinishEnter();
        }      
    }

    void OnFinishEnter()
    {
        //detiene movimiento del mundo
        _background[0].Stop();
        _background[1].Stop();

        //deshabilita movimiento personaje
        _character.StopMovement();
        
        StartCoroutine(GameOver.ShowPanel(true));
    }
}
