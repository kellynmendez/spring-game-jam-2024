using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PaintState : IState
{
    private PlayerSM playerSM;

    public PaintState(PlayerSM playerSM) 
    {
        this.playerSM = playerSM;
    }

    public void Enter()
    {


        // Change back to core game play
        CorePlayState st_corePlay = new CorePlayState(playerSM);
        playerSM.ChangeState(st_corePlay);
    }

    public void Exit()
    {

    }
}
