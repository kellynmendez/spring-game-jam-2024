using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldInv : MonoBehaviour
{
    [SerializeField] GameObject helmetActive;
    [SerializeField] GameObject swordActive;
    [SerializeField] GameObject arrowActive;

    private void Awake()
    {
        helmetActive.SetActive(false);
        swordActive.SetActive(false);
        arrowActive.SetActive(false);
    }

    public void SetHelmetInactive()
    {
        helmetActive.SetActive(true);
    }

    public void SetSwordInactive()
    {
        swordActive.SetActive(true);
    }

    public void SetArrowInactive()
    {
        arrowActive.SetActive(true);
    }

    public void SetHelmetActive()
    {
        helmetActive.SetActive(false);
    }

    public void SetSwordActive()
    {
        swordActive.SetActive(false);
    }

    public void SetArrowActive()
    {
        arrowActive.SetActive(false);
    }
}
