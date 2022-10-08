using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadLights : MonoBehaviour
{
    List<SectionLight> _illuminatedSectionsList;

    void Awake() 
    {
        _illuminatedSectionsList = new List<SectionLight>();    
    }

    public void AddIlluminatedSection(SectionLight section)
    {
        _illuminatedSectionsList.Add(section);

        //Sólo se necesita mantener iluminados los últimos 3 tramos (para formar un triángulo),
        //si hay más de 3, se oscurece el primer tramo y se remueve de la lista.
        if (_illuminatedSectionsList.Count > 3)
        {
            _illuminatedSectionsList[0].Darken();
            _illuminatedSectionsList.RemoveAt(0);
        }
    }
}
