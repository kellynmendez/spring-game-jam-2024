using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Click3D : MonoBehaviour
{
    public UnityEvent OnClick;
    private float height = 0.015f;

    private void OnMouseUpAsButton()
    {
        OnClick?.Invoke();
    }

    public void PrintTest()
    {
        Debug.Log("clicked obj");
    }

    void OnMouseEnter()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
    }

    private void OnMouseExit()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - height, transform.position.z);
    }
}
