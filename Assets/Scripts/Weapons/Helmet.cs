using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : Weapon
{
    [SerializeField] GameObject helmetParts;
    [SerializeField] GameObject defaultHelmet;
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
        helmetParts.SetActive(true);
        defaultHelmet.SetActive(false);
    }

    public override void ShowDefault()
    {
        helmetParts.SetActive(false);
        defaultHelmet.SetActive(true);
        defaultHelmet.transform.GetChild(0).GetComponent<MeshRenderer>().material = whiteMat;
        weaponColor = WeaponColor.Default;
    }

    public override void PaintRed()
    {
        helmetParts.SetActive(false);
        defaultHelmet.SetActive(true);
        defaultHelmet.transform.GetChild(0).GetComponent<MeshRenderer>().material = redMat;
        weaponColor = WeaponColor.Red;
    }

    public override void PaintBlue()
    {
        helmetParts.SetActive(false);
        defaultHelmet.SetActive(true);
        defaultHelmet.transform.GetChild(0).GetComponent<MeshRenderer>().material = blueMat;
        weaponColor = WeaponColor.Blue;
    }
}
