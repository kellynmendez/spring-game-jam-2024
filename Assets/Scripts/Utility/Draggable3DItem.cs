using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable3DItem : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 mousePosition;
    private bool dropped = false;
    [SerializeField] Camera cmra;

    public void Awake()
    {
        startPosition = transform.position;
    }

    private Vector3 GetMousePos()
    {
        return cmra.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }

    public void OnMouseDrag()
    {
        //Debug.Log("dragging");
        if (dropped) 
            return;

        transform.position = cmra.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

    public void OnMouseUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(cmra.ScreenPointToRay(Input.mousePosition), out hit))
        {
            DropPoint3D slot = hit.collider.GetComponent<DropPoint3D>();
            if (slot != null && slot.slotName == transform.name)
            {
                slot.SnapToSlot(this);
            }
        }
    }

    public Vector3 GetStartPosition()
    {
        return startPosition;
    }

    public void SetDropped(bool dropped)
    {
        this.dropped = dropped;
    }
}
