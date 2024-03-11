using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : Station
{
    private void Awake()
    {
        playerSM = FindObjectOfType<PlayerSM>();
    }

    public override void StartGame()
    {

    }
}
