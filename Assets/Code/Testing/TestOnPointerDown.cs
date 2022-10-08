using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestOnPointerDown : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown (PointerEventData eventData) 
     {
         Debug.Log (this.gameObject.name + " Was Clicked.");

         GetComponent<Image>().color = Color.black;
     }
}
