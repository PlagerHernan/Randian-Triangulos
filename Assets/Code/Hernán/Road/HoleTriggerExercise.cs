using UnityEngine;
using System.Collections;

public class HoleTriggerExercise : MonoBehaviour
{
    [SerializeField] int _exerciseId;

    ExerciseHandler _exerciseHandler;
    FormulaHandler _formulaHandler;
    Character _character;
    Background[] _background; 

    HoleTriggerHammer _triangleTriggerHammer;
    TriangleCanvas _triangleCanvas;

    private void Awake() 
    {
        _exerciseHandler = FindObjectOfType<ExerciseHandler>(); 
        _character = FindObjectOfType<Character>();
        _background = FindObjectsOfType<Background>();
        _triangleTriggerHammer = transform.parent.GetComponentInChildren<HoleTriggerHammer>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<Character>() != null)
        {
            StartCoroutine("OnTriangleEnter");
        }    
    }

    IEnumerator OnTriangleEnter()
    {
        //detiene movimiento del mundo
        _background[0].Stop();
        _background[1].Stop();

        //deshabilita movimiento personaje
        _character.StopMovement();
        
        yield return new WaitForSeconds(0.5f);

        /* if (_triangleCanvas.gameObject != null)
        {
            _triangleCanvas.enabled = true;
        } */

        _triangleCanvas = TriangleCanvas.Create(ScoreHandler.Score);
        _triangleCanvas.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
        _triangleCanvas.GetComponent<Canvas>().enabled = true;

        _exerciseHandler.SetCurrentExercise(_exerciseId);

        _formulaHandler = FindObjectOfType<FormulaHandler>();
        _formulaHandler.CompletedEquationSteps += OnTriangleCompleted;
    }

    void OnTriangleCompleted()
    {
        StartCoroutine(OnTriangleCompletedC());
    }
    IEnumerator OnTriangleCompletedC()
    {
        if (_exerciseHandler.CurrentExercise.id != _exerciseId)
        {
            yield break;
        }

        while (StarPoints.IncreasingShine)
        {
            yield return null;
        }
        
        _triangleCanvas.GetComponent<Canvas>().enabled = false;
        _triangleCanvas = null; 

        HelpIinfo.Show(HelpIinfo.NextAction.hammer);
        HelpIinfo.FinishTutorial();

        while (Game.IsGamePausing)
        {
            yield return null;
        }

        _triangleTriggerHammer.EnableTouch();
    }

    public void OnTriangleExit()
    {
        Invoke("OnTriangleExitI", 2f); //espera anim estrellas camino
    }
    void OnTriangleExitI()
    {
        //reanuda movimiento del mundo
        _background[0].Move();
        _background[1].Move();

        //habilita movimiento personaje
        _character.ResumeMovement();

        Destroy(gameObject);
    }
}
