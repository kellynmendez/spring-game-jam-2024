using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Station : MonoBehaviour
{
    protected PlayerSM playerSM;

    public abstract void StartGame();

    public virtual void ExitGame()
    {
        // Change back to core game play
        playerSM.ChangeState(PlayerSM.PlayerState.CorePlay);
    }
}
