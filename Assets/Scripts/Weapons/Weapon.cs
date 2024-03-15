using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum WeaponState { Built, Assembled, Painted };
    public WeaponState currentState;
    public enum WeaponColor { Default, Red, Blue };
    public WeaponColor weaponColor = WeaponColor.Default;

    protected PlayerSM playerSM;

    public abstract void ShowParts();
    public abstract void ShowDefault();
    public abstract void PaintRed();
    public abstract void PaintBlue();

    public void CarryWeapon()
    {
        this.transform.SetParent(playerSM.weaponHolder, false);
        Extensions.SetGlobalScale(transform, Vector3.one);
        playerSM.SetWeapon(this);
        playerSM.carryingWeapon = true;
    }

    public void PlaceWeapon(Transform trnsfrm)
    {
        this.transform.SetParent(trnsfrm, false);
        Extensions.SetGlobalScale(transform, Vector3.one);
        playerSM.SetWeapon(null);
        playerSM.carryingWeapon = false;
    }
}

public static class Extensions
{
    public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    }
}