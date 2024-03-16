using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StationUtils : MonoBehaviour
{
    public enum Station_Type {Build, Assemble, Paint, Mold, Trash, Counter}
    public Station_Type station_type;

    [SerializeField] public GameObject weaponPlace;

    PACPointer PACPointer;
    private Collider destination_col;
    private PlayerSM playerSM;
    private StationManager stationManager;
    private bool stationOccupied = false;

    [HideInInspector] public Weapon weaponAtStation = null;
    [HideInInspector] public bool _counterIsEmpty;

    public Counter counter = null;
    void Start()
    {
        PACPointer = GameObject.FindObjectOfType(typeof(PACPointer)) as PACPointer;
        destination_col = gameObject.GetComponent<Collider>();
        playerSM = FindObjectOfType<PlayerSM>();
        if (station_type != Station_Type.Counter)
            stationManager = GetComponent<StationManager>();
        else
            stationManager = null;

        if (gameObject.GetComponent<Counter>() != null)
        {
            counter = gameObject.GetComponent<Counter>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If a build OR assemble station and not occupied, player is holding weapon, and weapon is built, can place at station
        if ((station_type == Station_Type.Build || station_type == Station_Type.Assemble) && !stationOccupied 
            && playerSM.carryingWeapon && playerSM.GetWeapon().currentState == Weapon.WeaponState.Built)
        {
            SetStationOccupied(true, playerSM.GetWeapon());
        }
        // If assembly OR paint station not occupied, player is holding weapon, and weapon is assembled, can place at station
        else if ((station_type == Station_Type.Assemble || station_type == Station_Type.Paint) && !stationOccupied
            && playerSM.carryingWeapon && playerSM.GetWeapon().currentState == Weapon.WeaponState.Assembled)
        {
            SetStationOccupied(true, playerSM.GetWeapon());
        }
        // If paint station not occupied, player is holding weapon, and weapon is painted, can place at station
        else if (station_type == Station_Type.Paint && !stationOccupied
            && playerSM.carryingWeapon && playerSM.GetWeapon().currentState == Weapon.WeaponState.Painted)
        {
            SetStationOccupied(true, playerSM.GetWeapon());
        }

        else if (station_type == Station_Type.Trash && playerSM.carryingWeapon)
        {
            SetStationOccupied(true, playerSM.GetWeapon());
        }

        else if (station_type == Station_Type.Counter && gameObject.GetComponent<Counter>()._counterIsEmpty == false
                && playerSM.carryingWeapon && playerSM.GetWeapon().currentState == Weapon.WeaponState.Painted)
        {
            //get order, if carrying weapon drop off and check if order is complete?
            print("HELLO");
            print("Weapon " + playerSM.GetWeapon());
            gameObject.GetComponent<Counter>().CheckOrder(playerSM.GetWeapon());
            SetStationOccupied(true, playerSM.GetWeapon());
        }
        
        // If player is not carrying a weapon, check if a mini game can be started
        else if (!playerSM.carryingWeapon)
        {
            StartMinigame(station_type);
        }
        
        destination_col.isTrigger = false;
        PACPointer.inputDisabled = false;
    }

    void StartMinigame(Station_Type station)
    {
        if (!stationOccupied && station == Station_Type.Build)
        {
            playerSM.ChangeState(PlayerSM.PlayerState.Build, stationManager);
        }
        else if (stationOccupied && station == Station_Type.Assemble && weaponAtStation.currentState == Weapon.WeaponState.Built)
        {
            playerSM.ChangeState(PlayerSM.PlayerState.Assemble, stationManager);
        }
        else if (stationOccupied && station == Station_Type.Paint && weaponAtStation.currentState == Weapon.WeaponState.Assembled)
        {
            playerSM.ChangeState(PlayerSM.PlayerState.Paint, stationManager);
        }
        else if (station == Station_Type.Mold && !playerSM.carryingWeapon)
        {
            playerSM.ChangeState(PlayerSM.PlayerState.Mold, stationManager);
        }
        // If station is occupied and player is not carrying anything, player picks up weapon
        else if (station_type != Station_Type.Counter && station_type != Station_Type.Mold
            && stationOccupied && !playerSM.carryingWeapon)
        {
            SetStationOccupied(false, weaponAtStation);
        }
    }

    public void SetStationOccupied(bool newOccupied, Weapon weapon)
    {
        Debug.Log("newOcc = " + newOccupied + " oldOcc = " + stationOccupied);
        // Removing weapon from station and adding to player
        if (stationOccupied && !newOccupied)
        {
            Debug.Log("carrying weapon");
            weapon.CarryWeapon();
            weaponAtStation = null;
            this.stationOccupied = newOccupied;
        }
        // Moving weapon to station
        else if (!stationOccupied && newOccupied)
        {
            if (station_type != Station_Type.Trash)
            {
                weapon.PlaceWeapon(weaponPlace.transform);
                weaponAtStation = weapon;
                this.stationOccupied = newOccupied;
            }
            else
            {
                weapon.PlaceWeapon(weaponPlace.transform);
                StartCoroutine(TrashWeapon(weapon.gameObject));
            }
        }
    }

    IEnumerator TrashWeapon(GameObject weapon)
    {
        yield return new WaitForSeconds(1);
        Destroy(weapon);
    }
}
