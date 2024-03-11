using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembleManager : Station
{
    private void Awake()
    {
        playerSM = FindObjectOfType<PlayerSM>();
    }

    public override void StartGame()
    {

    }
}
