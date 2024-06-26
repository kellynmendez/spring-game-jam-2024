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
    private float height = 0.02f;
    [SerializeField] Camera cmra;
    private bool enableHoverAnim = true;

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
        RaycastHit[] hits = Physics.RaycastAll(cmra.ScreenPointToRay(Input.mousePosition), 100f);

        foreach (RaycastHit hit in hits)
        {
            DropPoint3D slot = hit.collider.GetComponent<DropPoint3D>();
            if (slot != null && slot.slotName == transform.name)
            {
                slot.SnapToSlot(this);
                enableHoverAnim = false;
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
        if (!dropped)
        {
            enableHoverAnim = true;
        }
    }

    void OnMouseEnter()
    {
        if (enableHoverAnim) { transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z); }
    }

    private void OnMouseExit()
    {
        if (enableHoverAnim) { transform.position = new Vector3(transform.position.x, transform.position.y - height, transform.position.z); }
    }
}
