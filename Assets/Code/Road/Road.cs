using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    //Transform del objeto hijo
    [SerializeField] Transform _subRoadTr;
    [SerializeField] float _speed = 1f;

    //Rotation
    [SerializeField] float _rotationSpeed = 1f;
    [SerializeField] Vector3[] _directions; public Vector3[] Directions { get => _directions; set => _directions = value; }
    [SerializeField] int _dirIndex = 0;
    private Vector3 _currentDir;
    bool _rotating = false;
    bool _canRotate = false; public bool CanRotate { get => _canRotate; set => _canRotate = value; }

    private float _currentSpeed;

    private void Start()
    {
        _currentSpeed = _speed;
        transform.up = _currentDir = _directions[_dirIndex];
    }

    void Update()
    {
        //Controlo el avance del gameObject hijo
        _subRoadTr.position += Vector3.down * _currentSpeed * Time.deltaTime;
    }

    public void Stop()
    {
        _currentSpeed = 0f;
        //_canRotate = true;
    }

    public void Resume()
    {
        //Evito que el mundo avance cuando todavía está rotando
        if (!_rotating)
        {
            _currentSpeed = _speed;
            _canRotate = false;
        }
    }

    public void Rotate(bool isRight)
    {
        if (_canRotate && !_rotating)
        {
            _rotating = true;

            //Cambio el indice para acceder a la proxima direccion
            if (isRight) _dirIndex++;
            else _dirIndex--;

            //Si el indice supera la cantidad de posiciones, vuelve a 0, y viceversa.
            if (_dirIndex < 0) _dirIndex = _directions.Length - 1;
            else if (_dirIndex >= _directions.Length) _dirIndex = 0;

            //Se obtienen los grados entre la direccion actual y la siguiente.
            var degrees = Vector3.SignedAngle(_currentDir, _directions[_dirIndex], Vector3.forward);

            //Se inicia la corutina de rotacion del mundo.
            StartCoroutine(RotateMe(Vector3.forward * (-degrees), 1 / _rotationSpeed));
        }
    }
    public void ForceRotate(bool isRight)
    {
        _canRotate = true;
        Rotate(isRight);
        _canRotate = false;
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        //Proceso del giro
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1.1f; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }

        //Se ejecuta al terminar de hacer el giro
        _rotating = false;
        _currentDir = _directions[_dirIndex];
    }

}
