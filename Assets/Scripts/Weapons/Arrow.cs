using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{
    public enum ArrowColor { Default, Red, Blue };
    public ArrowColor arrowColor = ArrowColor.Default;

    [SerializeField] GameObject arrowParts;
    [SerializeField] GameObject defaultArrow;
    [SerializeField] GameObject redArrow;
    [SerializeField] GameObject blueArrow;

    private void Awake()
    {
        ShowParts();
    }

    public override void ShowParts()
    {
        arrowParts.SetActive(true);
        defaultArrow.SetActive(false);
        redArrow.SetActive(false);
        blueArrow.SetActive(false);
    }

    public override void ShowDefault()
    {
        arrowParts.SetActive(false);
        defaultArrow.SetActive(true);
        redArrow.SetActive(false);
        blueArrow.SetActive(false);
    }

    public override void PaintRed()
    {
        defaultArrow.SetActive(false);
        redArrow.SetActive(true);
        blueArrow.SetActive(false);
    }

    public override void PaintBlue()
    {
        defaultArrow.SetActive(false);
        redArrow.SetActive(false);
        blueArrow.SetActive(true);
    }
}
