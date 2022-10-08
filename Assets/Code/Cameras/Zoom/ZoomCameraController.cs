using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCameraController : MonoBehaviour
{
    [Header("Zoom basic settings")]
    [Tooltip("Setea el zoom normal (modifica la variable Size del componente Camera)")]
    [SerializeField] float zoomInAmount;
    [Tooltip("Setea la cantidad de zoom out (modifica la variable Size del componente Camera)")]
    [SerializeField] float zoomOutAmount;
    [Tooltip("Modifica la velocidad de transicion entre zoom y zoom")]
    [SerializeField] float zoomLerpSpeed;

    [Header("Zoom time settings")]
    [Tooltip("Máxima cantidad de zoom que se puede usar")]
    [SerializeField] float maxTimeAmount = 100;
    [Tooltip("Velocidad de consumo de zoom (puede variar según el timeScale)")]
    [SerializeField] float zoomConsumption = 40;
    [Tooltip("Velocidad de recarga de zoom (puede variar según el timeScale)")]
    [SerializeField] float zoomRecover = 4;
    [Tooltip("Referencia a la barra de zoom")]
    [SerializeField] ZoomUIBar zoomUIBar;

    [Header("Zoom slow motion settings")]
    [Tooltip("El timeScale cuando el zoom out esta activado")]
    [SerializeField] [Range(0.01f, 1)] float _zoomTimeScale;

    private Camera cam;
    private float targetZoom;

    private bool _zoomedOut;
    private float _timeAmount;

    private void Start()
    {
        cam = GetComponent<Camera>();
        targetZoom = cam.orthographicSize;
        _timeAmount = maxTimeAmount;
    }

    private void Update()
    {
        if (_timeAmount <= 0)
            ZoomOut(false);
        else if (_timeAmount >= maxTimeAmount)
            _timeAmount = maxTimeAmount;

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);

        UpdateTimeValue();
    }

    private void UpdateTimeValue()
    {
        if (_zoomedOut)
            _timeAmount -= (Time.deltaTime * zoomConsumption);
        else
            _timeAmount += (Time.deltaTime * zoomRecover);

        if (zoomUIBar != null)
            zoomUIBar.UpdateBar(_timeAmount, maxTimeAmount);
    }

    public void ZoomOut(bool zoom)
    {
        if (zoom)
        {
            targetZoom = zoomOutAmount;
            _zoomedOut = true;
            Time.timeScale = _zoomTimeScale;
        }
        else
        {
            targetZoom = zoomInAmount;
            _zoomedOut = false;
            Time.timeScale = 1;
        }
    }
}
