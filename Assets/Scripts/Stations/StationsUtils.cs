using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StationUtils : MonoBehaviour
{
    public enum Station_Type {Build, Assemble, Paint, Mold, Counter}
    public Station_Type station_type;

    [SerializeField] public GameObject weaponPlace;

    PACPointer PACPointer;
    private Collider destination_col;
    private PlayerSM playerSM;
    private StationManager stationManager;
    private bool stationOccupied = false;
    private GameObject weaponAtStation = null;

    public bool _counterIsEmpty;

    void Start()
    {
        PACPointer = GameObject.FindObjectOfType(typeof(PACPointer)) as PACPointer;
        destination_col = gameObject.GetComponent<Collider>();
        playerSM = FindObjectOfType<PlayerSM>();
        if (station_type != Station_Type.Counter)
            stationManager = GetComponent<StationManager>();
        else
            stationManager = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If station is occupied and player is not carrying anything, player picks up weapon
        if (station_type != Station_Type.Counter && stationOccupied && !playerSM.carryingWeapon)
        {
            SetStationOccupied(false, weaponAtStation);
        }
        // If station is not occupied and player is carrying weapon, weapon is placed at station
        else if (station_type != Station_Type.Counter && !stationOccupied && playerSM.carryingWeapon)
        {
            SetStationOccupied(true, playerSM.GetWeapon());
        }
        else if (station_type == Station_Type.Counter)
        {
            //get order, if carrying weapon drop off and check if order is complete?
        }
        else
        {
            StartMinigame(station_type);
        }
        
        
        destination_col.isTrigger = false;
        PACPointer.inputDisabled = false;
    }

    void StartMinigame(Station_Type station)
    {
        if (station == Station_Type.Build)
        {
            playerSM.ChangeState(PlayerSM.PlayerState.Build, stationManager);
        }
        else if (station == Station_Type.Assemble)
        {
            playerSM.ChangeState(PlayerSM.PlayerState.Assemble, stationManager);
        }
        else if (station == Station_Type.Paint)
        {
            playerSM.ChangeState(PlayerSM.PlayerState.Paint, stationManager);
        }
        else if (station == Station_Type.Mold)
        {
            playerSM.ChangeState(PlayerSM.PlayerState.Mold, stationManager);
        }
    }

    public void SetStationOccupied(bool newOccupied, GameObject weapon)
    {
        // Removing weapon from station and adding to player
        if (stationOccupied && !newOccupied)
        {
            weapon.transform.parent = playerSM.weaponHolder.transform;
            playerSM.carryingWeapon = true;
            weaponAtStation = null;
        }
        // Moving weapon to station
        else if (!stationOccupied && newOccupied)
        {
            weapon.transform.parent = weaponPlace.transform;
            playerSM.carryingWeapon = false;
            weaponAtStation = weapon;
        }

        this.stationOccupied = newOccupied;
    }
}
