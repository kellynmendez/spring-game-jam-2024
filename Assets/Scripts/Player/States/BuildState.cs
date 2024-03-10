using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : IState
{
    private PlayerSM playerSM;

    public BuildState(PlayerSM playerSM)
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
