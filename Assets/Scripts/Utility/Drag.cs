using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image img;
    private Vector3 startPosition;

    private void Start()
    {
        img = GetComponent<Image>();
        startPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        img.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition;
        img.raycastTarget = true;
    }

    public void SetStartPosition(Vector3 startPosition)
    { 
        this.startPosition = startPosition; 
    }
}
