using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarPoints : MonoBehaviour
{
    FormulaHandler _formulaHandler; 

    Animator _animator;
    RectTransform _rectTransform;
    Image _shineImage;

    float _brightness; //public float Brightness { set => _brightness = value; }
    static bool _increasingShine; public static bool IncreasingShine { get => _increasingShine; } 

    void Awake() 
    {
        _formulaHandler = FindObjectOfType<FormulaHandler>();

        _animator = GetComponent<Animator>(); 
        _rectTransform = GetComponent<RectTransform>();  

        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            if (image.gameObject.name == "Shine")
            {
                _shineImage = image;
            }
        } 

        ExerciseHandler.EstablishedCurrentExercise += SetInitialValues;
        _formulaHandler.CompletedEquationSteps += IncreaseShine;
    }

    void SetInitialValues()
    {
        _increasingShine = true;
    }

    void IncreaseShine()
    {
        StartCoroutine(IncreaseShineC());
    }

    IEnumerator IncreaseShineC()
    {
        //Aumenta escala
        float targetScale = 1.5f, currentScale = 1f;
        while (currentScale < targetScale)
        {
            currentScale = currentScale + 0.01f;
            _rectTransform.localScale = new Vector3(currentScale, currentScale);  
            yield return new WaitForSeconds(0.001f); 
        } 
        yield return new WaitForSeconds(0.1f);

        //Activa animación latido 
        _animator.SetBool("shine", true);
        AudioHandler.PlaySound("BigStar");

        //Aumenta opacidad
        float targetAlpha = ScoreHandler.Score; 

        float difference = targetAlpha - _brightness;
        float gradualIncrease = difference * 0.01f;

        while (_brightness < targetAlpha)
        {
            _brightness = _brightness + gradualIncrease;
            _shineImage.color = new Color(1f, 1f, 1f, _brightness); 
            yield return new WaitForSeconds(0.02f); 
        } 

        if (difference <= 0)
        {
            yield return new WaitForSeconds(1f);
        }

        //Desactiva animación latido 
        _animator.SetBool("shine", false);
        yield return new WaitForSeconds(0.1f);

        //Disminuye escala
        float newTargetScale = 1f, newCurrentScale = 1.5f;
        while (newCurrentScale > newTargetScale)
        {
            newCurrentScale = newCurrentScale - 0.01f;
            _rectTransform.localScale = new Vector3(newCurrentScale, newCurrentScale);  
            yield return new WaitForSeconds(0.001f); 
        }
        yield return new WaitForSeconds(0.5f);

        _increasingShine = false;
    }
}
