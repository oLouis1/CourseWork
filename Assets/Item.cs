using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Item : MonoBehaviour, IDragHandler
{
    public string itemName;

    public void OnDrag(PointerEventData eventData)
    {
        //moves item to mouse position
        transform.position = eventData.position;
    }

}
