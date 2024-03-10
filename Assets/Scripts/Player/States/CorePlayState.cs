using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePlayState : IState
{
    private PlayerSM playerSM;

    public CorePlayState(PlayerSM playerSM)
    {
        this.playerSM = playerSM;
    }

    public void Enter()
    {
        PACPointer movementComp = playerSM.transform.GetComponent<PACPointer>();
        movementComp.enabled = true;
    }

    public void Exit()
    {
        PACPointer movementComp = playerSM.transform.GetComponent<PACPointer>();
        movementComp.enabled = false;
    }
}
