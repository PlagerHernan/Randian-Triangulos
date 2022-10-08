using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chronometer : MonoBehaviour
{
    float _time;
    Text _text;
    bool _stop; public bool Stop { get => _stop; set => _stop = value; }

    private void Awake() 
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        //si no está detenido por Trigger
        if (!_stop)
        {
            _time += Time.deltaTime;
            _text.text = "Tiempo: " + string.Format("{0:0.0}", _time);
        } 
    }

    public void ResetTimer()
    {
        _time = 0f;
    }
}
