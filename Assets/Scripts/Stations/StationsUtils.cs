using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class StationUtils : MonoBehaviour
{   
    PACPointer PACPointer;
    PointAndClickMovement pointAndClickMovement;

    public enum Station_Type {Build, Assemble, Paint, Mold, Counter}
    public Station_Type station_type;
    private Collider destination_col;
    private PlayerSM playerSM;

    // Start is called before the first frame update
    void Start()
    {
        pointAndClickMovement = GameObject.FindObjectOfType(typeof(PointAndClickMovement)) as PointAndClickMovement;
        PACPointer = GameObject.FindObjectOfType(typeof(PACPointer)) as PACPointer;
        destination_col = gameObject.GetComponent<Collider>();
        playerSM = FindObjectOfType<PlayerSM>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void OnTriggerEnter(Collider other)
    {
        StartMinigame(station_type);

        destination_col.isTrigger = false;
        PACPointer.inputDisabled = false;
    }

    void StartMinigame(Station_Type station)
    {
        // Change state to start build station mini game
        if (station == Station_Type.Build)
        {
            playerSM.ChangeState(PlayerSM.PlayerState.Build);
        }
        // Change state to start assemble station mini game
        else if (station == Station_Type.Assemble)
        {
            playerSM.ChangeState(PlayerSM.PlayerState.Assemble);
        }
        // Change state to start paint station mini game
        else if (station == Station_Type.Paint)
        {
            playerSM.ChangeState(PlayerSM.PlayerState.Paint);
        }
        // Change state to start mold station mini game
        else if (station == Station_Type.Mold)
        {
            playerSM.ChangeState(PlayerSM.PlayerState.Mold);
        }
        // One of three counters
        else if (station == Station_Type.Counter)
        {

        }
    }
}
