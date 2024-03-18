using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleScreen : MonoBehaviour
{
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject middleScreen;
    [SerializeField] GameObject endScreen;

    private void Awake()
    {
        startScreen.SetActive(true);
        middleScreen.SetActive(false);
        endScreen.SetActive(false);
    }
}
