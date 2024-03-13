using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public enum Station_Type { Sword, Helmet, Arrow }

    private PlayerSM playerSM;

    private void Awake()
    {
        playerSM = FindObjectOfType<PlayerSM>();
    }

    public void CarryWeapon()
    {
        this.transform.parent = playerSM.weaponHolder.transform;
    }

    public void PlaceWeapon(StationUtils station)
    {
        this.transform.parent = station.weaponPlace.transform;
    }
}
