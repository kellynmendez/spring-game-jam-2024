using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public enum SwordColor { Default, Red, Blue };
    [HideInInspector] public SwordColor swordColor = SwordColor.Default;

    [SerializeField] GameObject swordParts;
    [SerializeField] GameObject defaultSword;
    [SerializeField] Material whiteMat;
    [SerializeField] Material redMat;
    [SerializeField] Material blueMat;

    private void Awake()
    {
        ShowParts();
        currentState = WeaponState.Built;
        playerSM = FindObjectOfType<PlayerSM>();
    }

    public override void ShowParts()
    {
        swordParts.SetActive(true);
        defaultSword.SetActive(false);
    }

    public override void ShowDefault()
    {
        swordParts.SetActive(false);
        defaultSword.SetActive(true);
        defaultSword.transform.GetChild(0).GetComponent<MeshRenderer>().material = whiteMat;
        swordColor = SwordColor.Default;
    }

    public override void PaintRed()
    {
        swordParts.SetActive(false);
        defaultSword.SetActive(true);
        defaultSword.transform.GetChild(0).GetComponent<MeshRenderer>().material = redMat;
        swordColor = SwordColor.Red;
    }

    public override void PaintBlue()
    {
        swordParts.SetActive(false);
        defaultSword.SetActive(true);
        defaultSword.transform.GetChild(0).GetComponent<MeshRenderer>().material = blueMat;
        swordColor = SwordColor.Blue;
    }
}
