using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class Station : MonoBehaviour
{   
    PACPointer PACPointer;
    PointAndClickMovement pointAndClickMovement;

    public enum Station_Type {Build, Assemble, Paint, Mold, Counter}
    public Station_Type station_type;
    private Collider destination_col;
    private PlayerController playerSM;

    // Start is called before the first frame update
    void Start()
    {
        pointAndClickMovement = GameObject.FindObjectOfType(typeof(PointAndClickMovement)) as PointAndClickMovement;
        PACPointer = GameObject.FindObjectOfType(typeof(PACPointer)) as PACPointer;
        destination_col = gameObject.GetComponent<Collider>();
        playerSM = FindObjectOfType<PlayerController>();
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
        print("started " + station + " minigame");

        // Change state to start build station mini game
        if (station == Station_Type.Build)
        {
            IState st_build = new BuildState(playerSM);
            playerSM.ChangeState(st_build);
        }
        // Change state to start assemble station mini game
        else if (station == Station_Type.Assemble)
        {
            IState st_assemble = new AssembleState(playerSM);
            playerSM.ChangeState(st_assemble);
        }
        // Change state to start paint station mini game
        else if (station == Station_Type.Paint)
        {
            IState st_paint = new PaintState(playerSM);
            playerSM.ChangeState(st_paint);
        }
        // Change state to start mold station mini game
        else if (station == Station_Type.Mold)
        {
            IState st_mold = new MoldState(playerSM);
            playerSM.ChangeState(st_mold);
        }
        // One of three counters
        else if (station == Station_Type.Counter)
        {

        }
    }
}
