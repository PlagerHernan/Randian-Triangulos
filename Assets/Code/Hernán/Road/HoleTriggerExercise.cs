using UnityEngine;
using System.Collections;

public class HoleTriggerExercise : MonoBehaviour
{
    FormulaHandler _formulaHandler;
    Character _character;
    Background[] _background; 

    HoleTriggerHammer _holeTriggerHammer;
    TriangleCanvas _triangleCanvas;
    bool _triggerEnabled;

    private void Awake() 
    {
        _character = FindObjectOfType<Character>();
        _background = FindObjectsOfType<Background>();
        _holeTriggerHammer = transform.parent.GetComponentInChildren<HoleTriggerHammer>();

        LevelHandler.StartingLevel += EnableTrigger;
    }

    void EnableTrigger()
    {
        _triggerEnabled = true;
    }

    void DisableTrigger()
    {
        _triggerEnabled = false;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (_triggerEnabled && other.GetComponent<Character>() != null)
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

        if (GameManager.TestMode)
        {
            ExerciseHandler.SetCurrentExercise();
            _holeTriggerHammer.EnableTouch();
            yield break;
        }
        
        yield return new WaitForSeconds(0.5f);

        /* if (_triangleCanvas.gameObject != null)
        {
            _triangleCanvas.enabled = true;
        } */

        _triangleCanvas = TriangleCanvas.Create(ScoreHandler.Score);
        _triangleCanvas.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
        _triangleCanvas.GetComponent<Canvas>().enabled = true;

        ExerciseHandler.SetCurrentExercise();

        _formulaHandler = FindObjectOfType<FormulaHandler>();
        _formulaHandler.CompletedEquationSteps += OnTriangleCompleted;
    }

    void OnTriangleCompleted()
    {
        StartCoroutine(OnTriangleCompletedC());
    }
    IEnumerator OnTriangleCompletedC()
    {
        while (StarPoints.IncreasingShine)
        {
            yield return null;
        }
        
        _triangleCanvas.GetComponent<Canvas>().enabled = false;
        _triangleCanvas = null; 

        HelpIinfo.Show(HelpIinfo.NextAction.hammer);
        HelpIinfo.FinishTutorial();

        while (GameManager.IsGamePausing)
        {
            yield return null;
        }

        _holeTriggerHammer.EnableTouch();
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

        DisableTrigger();
    }
}
