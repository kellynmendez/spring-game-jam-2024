using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StationManager : MonoBehaviour
{
    [SerializeField] protected GameObject gameScreen;
    protected PlayerSM playerSM;
    protected bool inactive = true;

    public abstract void StartGame();

    protected virtual void ExitGame()
    {
        if (!inactive)
        {
            inactive = true;
            gameScreen.SetActive(false);
            // Change back to core game play
            playerSM.ChangeState(PlayerSM.PlayerState.CorePlay, null);
            GameObject[] stations = GameObject.FindGameObjectsWithTag("Un-Clickable");
            foreach (GameObject things in stations)
            {
                gameObject.tag = "Clickable";
            }
        }
        
    }
}
