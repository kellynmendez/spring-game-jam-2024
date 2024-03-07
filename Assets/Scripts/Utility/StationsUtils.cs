using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class Station : MonoBehaviour
{   
    PACPointer PACPointer;
    PointAndClickMovement pointAndClickMovement;

    public enum Station_Type {Station01, Station02, Station03, Station04, Counter}
    public Station_Type station_type;
    private Collider destination_col;

    // Start is called before the first frame update
    void Start()
    {
        pointAndClickMovement = GameObject.FindObjectOfType(typeof(PointAndClickMovement)) as PointAndClickMovement;
        PACPointer = GameObject.FindObjectOfType(typeof(PACPointer)) as PACPointer;
        destination_col = gameObject.GetComponent<Collider>();
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
    }
}
