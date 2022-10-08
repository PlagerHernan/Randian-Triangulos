using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Road _road;
    Character _character;

    void Awake()
    {
        _road = FindObjectOfType<Road>();
        _character = FindObjectOfType<Character>();
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.GetComponent<Character>() != null)
        {
            StartCoroutine(Brake());
        }
    }

    //Este método sólo se llama en los gameObjects "ObstacleToBreak", los cuales poseen Trigger
    void OnTriggerStay2D(Collider2D other) 
    {
        //Si el personaje está cerca del obstáculo y hay un toque, el obstáculo se destruye
        if (other.GetComponent<Character>() != null)
        {
            if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
            {
                //desactiva colliders
                BoxCollider2D [] boxColliders = GetComponents<BoxCollider2D>();
                foreach (BoxCollider2D col in boxColliders)
                {
                    col.enabled = false;
                }
                //desactiva sprite
                GetComponent<SpriteRenderer>().enabled = false;
                //destruye objeto a los 3 segundos
                Destroy(gameObject, 3f);
            } 
        }
    }

    //Detiene el movimiento del camino (simula interrupción o tropiezo del personaje) y el movimiento lateral del personaje
    IEnumerator Brake()
    {
        _road.Stop();
        _character.StopMovement();
        _character.MovingToLeft = false;
        _character.MovingToRight = false;

        yield return new WaitForSeconds(1f);

        _road.Resume();
        _character.ResumeMovement();
    }
}
