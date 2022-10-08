using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomUIBar : MonoBehaviour
{
    Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }
    public void UpdateBar(float value, float percentFactor)
    {
        if (_slider != null)
        {
            _slider.value = value / percentFactor;   
        }
    }
}
