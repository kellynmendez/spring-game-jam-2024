using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembleManager : MonoBehaviour
{
    private PlayerSM playerSM;

    private void Awake()
    {
        playerSM = FindObjectOfType<PlayerSM>();
    }

    public void StartGame()
    {


        // Change back to core game play
        playerSM.ChangeState(PlayerSM.PlayerState.CorePlay);
    }

    public void Exit()
    {
        
    }
}
