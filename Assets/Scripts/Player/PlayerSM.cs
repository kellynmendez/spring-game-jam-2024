using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSM : MonoBehaviour
{
    private IState currentState;
    private bool transitioning = false;

    private void Start()
    {
        currentState = new CorePlayState(this);
        currentState.Enter();
    }

    public void ChangeState(IState nextState)
    {
        transitioning = true;
        currentState.Exit();
        currentState = nextState;
        currentState.Enter();
        transitioning = false;
    }


}
