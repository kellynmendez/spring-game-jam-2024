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
    private float planeY = 0;
    [SerializeField] Camera cmra;

    private void Awake()
    {
        planeY = transform.position.y;
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

        /*Transform draggingObject = transform;

        Plane plane = new Plane(Vector3.up, Vector3.up * planeY); // ground plane

        Ray ray = cmra.ScreenPointToRay(Input.mousePosition);

        float distance; // the distance from the ray origin to the ray intersection of the plane
        if (plane.Raycast(ray, out distance))
        {
            draggingObject.position = ray.GetPoint(distance); // distance along the ray
        }*/

        transform.position = cmra.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

    public Vector3 GetStartPosition()
    {
        return startPosition;
    }

    public void SetStartPosition(Vector3 startPosition)
    { 
        this.startPosition = startPosition;
    }

    public void SetDropped(bool dropped)
    {
        this.dropped = dropped;
    }
}
