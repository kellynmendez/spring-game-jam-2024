using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Click3D : MonoBehaviour
{
    public UnityEvent OnClick;

    private void OnMouseUpAsButton()
    {
        OnClick?.Invoke();
    }

    public void PrintTest()
    {
        Debug.Log("clicked obj");
    }
}
