using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropPoint : MonoBehaviour, IDropHandler
{
    [SerializeField] string slotName;

    private bool dropped = false;
    private DraggableItem draggable;
    private Vector3 origStartPos;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.transform.name == slotName)
        {
            draggable = eventData.pointerDrag.GetComponent<DraggableItem>();
            if (draggable != null)
            {
                origStartPos = draggable.GetStartPosition();
                draggable.SetStartPosition(this.transform.position);
                //draggable.image.raycastTarget = false;
                dropped = true;
            }
        }
    }

    public bool Filled()
    {
        return dropped;
    }

    public void ResetDropPoint()
    {
        draggable.SetStartPosition(origStartPos);
        draggable.transform.position = origStartPos;
        //draggable.image.raycastTarget = true;
        dropped = false;
    }
}
