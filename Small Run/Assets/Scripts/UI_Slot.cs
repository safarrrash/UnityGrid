using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject ContainedObject;
    [SerializeField] private Image IconSlot;
    
    private MapManager mapManager;

    private void Awake()
    {
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        IconSlot.enabled = true;
        IconSlot.sprite = ContainedObject.GetComponent<CharacterInfo>().Icon;

        IconSlot.raycastTarget = false;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mapManager.selectedGameobject = ContainedObject;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        
            
            mapManager.disableGhost();
        

        if (mapManager.selectedGameobject != null)
        {
            mapManager.SpawnCharacter();
            IconSlot.rectTransform.localPosition = Vector3.zero;
            var tmp = IconSlot.color;
            tmp.a = 1;
            IconSlot.color = tmp;
            mapManager.selectedGameobject = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
            
        IconSlot.rectTransform.position = new Vector3(IconSlot.rectTransform.position.x , Input.mousePosition.y, IconSlot.rectTransform.position.z);
            
        if(IconSlot.rectTransform.localPosition.y >= 50) IconSlot.rectTransform.localPosition = new Vector3(IconSlot.rectTransform.localPosition.x, 50, IconSlot.rectTransform.localPosition.z);
        if (IconSlot.rectTransform.localPosition.y <=0) IconSlot.rectTransform.localPosition = new Vector3(IconSlot.rectTransform.localPosition.x, 0, IconSlot.rectTransform.localPosition.z);
        var tmp = IconSlot.color;
        tmp.a = 1-(IconSlot.rectTransform.localPosition.y/50);
        
        IconSlot.color = tmp;
        
    }

    public GameObject getSelectedObject()
    {
        return ContainedObject;
    }
}
