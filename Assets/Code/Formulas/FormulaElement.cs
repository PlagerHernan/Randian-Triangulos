using UnityEngine;
using UnityEngine.EventSystems;

public class FormulaElement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] bool _isCorrect;
    [SerializeField] GameObject _formulaStage1;
    [SerializeField] GameObject _formulaStage2;
    [SerializeField] int _id; public int Id { get => _id; }

    Health _health;

    public bool IsCorrect { get => _isCorrect; set => _isCorrect = value; }

    private void Awake()
    {
        _health = FindObjectOfType<Health>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isCorrect)
        {
            _formulaStage2.SetActive(true);
            _formulaStage1.SetActive(false);
        }
        else
            PerderVida();
    }

    public void PerderVida()
    {
        if(_health != null)
        {
            _health.Reduce();
        }
    }
}
