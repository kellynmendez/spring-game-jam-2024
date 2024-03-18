using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAnim : MonoBehaviour
{
    [SerializeField] float height = 0.015f;

    void OnMouseEnter()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
    }

    private void OnMouseExit()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - height, transform.position.z);
    }
}
