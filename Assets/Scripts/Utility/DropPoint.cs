using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropPoint : MonoBehaviour
{
    [SerializeField] string slotName;
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.transform.name == slotName)
        {
            Drag draggable = eventData.pointerDrag.GetComponent<Drag>();
            if (draggable != null)
            {
                draggable.SetStartPosition(this.transform.position);
            }
        }
    }
}
