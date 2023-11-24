using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class Item : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public string itemName;

    private Transform parentTransform;
    private Transform GrandParentTransfrom;

    public void Start(){
        parentTransform = transform.parent;
        GrandParentTransfrom = parentTransform.parent;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //moves item to mouse position
        transform.position = eventData.position;
        transform.parent = GrandParentTransfrom; //unlinks the item from its parent so it doesnt go behind other inventory slots during dragging
    }
    public void OnEndDrag(PointerEventData eventData){
      

        

        float SmallestDistanceFromItem = float.MaxValue;    //this will be used to find which slot is closest to the item when its no longer being dragged
        Transform closestSlot = null;                       //This means we can snap the item to the closest slot.

        for (int i=1; i<GrandParentTransfrom.childCount-1; i++){

            Transform currentInventorySlot = GrandParentTransfrom.GetChild(i);
            float currentDistance = Vector3.Distance(transform.position, currentInventorySlot.transform.position);

            if(currentDistance<SmallestDistanceFromItem){
                SmallestDistanceFromItem=currentDistance;
                closestSlot = currentInventorySlot;
            }
        }
        transform.position = closestSlot.transform.position + new Vector3(0,0,1);
        transform.parent=closestSlot;





    }

}
