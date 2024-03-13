using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BuildManager : StationManager
{
    [SerializeField] GameObject chooseMoldsScreen;
    [SerializeField] GameObject swordUnfilledMold;
    [SerializeField] GameObject helmetUnfilledMold;
    [SerializeField] GameObject arrowUnfilledMold;
    [SerializeField] GameObject swordFilledMold;
    [SerializeField] GameObject helmetFilledMold;
    [SerializeField] GameObject arrowFilledMold;

    private BuildState currentState;
    private GameObject unfilledMold = null;
    private GameObject filledMold = null;

    private enum BuildState
    {
        Inactive,
        ChoosingMold,
        FillingMold
    }

    private void Awake()
    {
        playerSM = FindObjectOfType<PlayerSM>();
        gameScreen.SetActive(false);
        currentState = BuildState.Inactive;
    }

    public override void StartGame()
    {
        inactive = false;
        currentState = BuildState.ChoosingMold;
        gameScreen.SetActive(true);
        chooseMoldsScreen.SetActive(true);
    }

    public void SwordChosen()
    {
        unfilledMold = swordUnfilledMold;
        filledMold = swordFilledMold;
        SwitchToFillMold();
    }

    public void HelmetChosen()
    {
        unfilledMold = helmetUnfilledMold;
        filledMold = helmetFilledMold;
        SwitchToFillMold();
    }

    public void ArrowChosen()
    {
        unfilledMold = arrowUnfilledMold;
        filledMold = arrowFilledMold;
        SwitchToFillMold();
    }

    private void SwitchToFillMold()
    {
        chooseMoldsScreen.SetActive(false);
        unfilledMold.SetActive(true);
        currentState = BuildState.FillingMold;
    }

    public void FillMold()
    {
        unfilledMold.SetActive(false);
        filledMold.SetActive(true);
        StartCoroutine(WaitAfterFill());
    }

    IEnumerator WaitAfterFill()
    {
        yield return new WaitForSeconds(1f);
        filledMold.SetActive(false);
        currentState = BuildState.Inactive;
        ExitGame();
        yield break;
    }
}
