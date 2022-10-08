using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightController : MonoBehaviour
{
    HighLight[] _highlights;

    [SerializeField] int _highlightsCount;
    [SerializeField] int _currentHighlightPosition;

    public int CurrentHighlightPosition { get => _currentHighlightPosition;}

    private void Awake()
    {
        _highlights = GetComponentsInChildren<HighLight>();
    }

    private void Start()
    {
        _highlightsCount = _highlights.Length;
        _currentHighlightPosition = 0;
        UpdateHighlights();
    }
    public void SetPosition(int pos)
    {
        if (pos > _highlightsCount || pos < 0) return;

        _currentHighlightPosition = pos;
        UpdateHighlights();
    }

    private void UpdateHighlights()
    {
        for (int i = 0; i < _highlights.Length; i++)
        {
            if (i == _currentHighlightPosition)
                _highlights[i].gameObject.SetActive(true);
            else
                _highlights[i].gameObject.SetActive(false);
        }
    }

}
