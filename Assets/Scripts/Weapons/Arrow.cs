using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{
    [SerializeField] GameObject arrowParts;
    [SerializeField] GameObject defaultArrow;
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
        arrowParts.SetActive(true);
        defaultArrow.SetActive(false);
    }

    public override void ShowDefault()
    {
        arrowParts.SetActive(false);
        defaultArrow.SetActive(true);
        defaultArrow.transform.GetChild(0).GetComponent<MeshRenderer>().material = whiteMat;
        weaponColor = WeaponColor.Default;
    }

    public override void PaintRed()
    {
        arrowParts.SetActive(false);
        defaultArrow.SetActive(true);
        defaultArrow.transform.GetChild(0).GetComponent<MeshRenderer>().material = redMat;
        weaponColor = WeaponColor.Red;
    }

    public override void PaintBlue()
    {
        arrowParts.SetActive(false);
        defaultArrow.SetActive(true);
        defaultArrow.transform.GetChild(0).GetComponent<MeshRenderer>().material = blueMat;
        weaponColor = WeaponColor.Blue;
    }
}
