using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Station : MonoBehaviour
{
    [SerializeField] protected GameObject gameScreen;
    protected PlayerSM playerSM;

    public abstract void StartGame();

    protected virtual void ExitGame()
    {
        gameScreen.SetActive(false);
        // Change back to core game play
        playerSM.ChangeState(PlayerSM.PlayerState.CorePlay);
    }
}
