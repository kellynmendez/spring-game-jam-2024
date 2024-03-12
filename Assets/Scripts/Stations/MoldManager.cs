using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldManager : Station
{
    private void Awake()
    {
        playerSM = FindObjectOfType<PlayerSM>();
    }

    public override void StartGame()
    {
        ExitGame();
    }
}
