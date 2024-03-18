using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OutlineText : MonoBehaviour
{
    void Awake()
    {
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        textmeshPro.outlineWidth = 0.2f;
        textmeshPro.outlineColor = Color.black;
    }
}
