using System.Collections.Generic;
using UnityEngine;

public class TriangleListSetter : MonoBehaviour
{
    [SerializeField] string _jsonFileName;
    TriangleList _triangleList; 

    TriangleSetter[] _triangleSetters;


    void Awake() 
    {
        _triangleSetters = GetComponentsInChildren<TriangleSetter>();

        _triangleList.triangleTypes = new List<TriangleType>();
    }

    void Start() 
    {
        SetList();
    }

    void SetList()
    {
        for (int i = 0; i < _triangleSetters.Length; i++)
        {
            _triangleList.triangleTypes.Add(_triangleSetters[i].Triangle);
        }

        JsonWriter.SetListElementsToJson(_triangleList, _jsonFileName);
    }
}
