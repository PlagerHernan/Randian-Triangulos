using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionLight : MonoBehaviour
{
    RoadLights _roadLights;
    SpriteRenderer[] _spritesRenderers;
    
    //Hay 2 triggers en cada sección: uno en cada extremo. Al pasar por ambos, se asegura que el personaje recorrió el tramo,
    //independientemente del extremo en que haya comenzado y terminado (el tramo se puede recorrer en ambos sentidos).  
    int _crossedTriggers;

    void Awake() 
    {
        _roadLights = FindObjectOfType<RoadLights>();
        _spritesRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<Character>() != null)
        {
            _crossedTriggers ++;

            //si el tramo fue recorrido, se ilumina y se agrega a la lista
            if (_crossedTriggers == 2)
            {
                Illuminate();
                _roadLights.AddIlluminatedSection(this);
            }
        }   
    } 

    void Illuminate()
    {
        //se iluminan todos los sprites del tramo (color verde más claro)
        foreach (SpriteRenderer item in _spritesRenderers)
        {
            item.color = new Color(38/255f, 1f, 0f);   
        }
    }

    public void Darken()
    {
        //se oscurecen todos los sprites del tramo (color verde más oscuro)
        foreach (SpriteRenderer item in _spritesRenderers)
        {
            item.color = new Color(130/255f, 130/255f, 130/255f);   
        }
    }
}
