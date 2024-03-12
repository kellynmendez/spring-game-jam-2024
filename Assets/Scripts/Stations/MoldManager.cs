using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldManager : Station
{
    // Screen to choose the mold to create
    [SerializeField] GameObject chooseMoldsScreen;
    // Cutter images and clay to cut
    [SerializeField] GameObject swordCutter;
    [SerializeField] GameObject helmetCutter;
    [SerializeField] GameObject arrowCutter;
    [SerializeField] GameObject clayBlob;
    // Finished mold images
    [SerializeField] GameObject swordMold;
    [SerializeField] GameObject helmetMold;
    [SerializeField] GameObject arrowMold;

    private MoldState currentState;
    private GameObject moldCutter = null;
    private GameObject finishedMold = null;

    private enum MoldState
    {
        Inactive,
        ChoosingMold,
        MakingMold,
        Finished
    }

    private void Awake()
    {
        playerSM = FindObjectOfType<PlayerSM>();
        currentState = MoldState.Inactive;
        // Default all screens that shouldn't be active
        gameScreen.SetActive(false);
        swordCutter.SetActive(false);
        helmetCutter.SetActive(false);
        arrowCutter.SetActive(false);
        clayBlob.SetActive(false);
        swordMold.SetActive(false);
        helmetMold.SetActive(false);
        arrowMold.SetActive(false);
    }

    public override void StartGame()
    {
        currentState = MoldState.ChoosingMold;
        gameScreen.SetActive(true);
        chooseMoldsScreen.SetActive(true);
    }

    private void Update()
    {
        if (currentState == MoldState.Inactive)
            return;
        else if (currentState == MoldState.MakingMold)
        {
            HandleMoldDragInput();
        }
        else if (currentState == MoldState.Finished)
        {
            currentState = MoldState.Inactive;
            ExitGame();
        }
    }

    public void SwordChosen()
    {
        moldCutter = swordCutter;
        finishedMold = swordMold;
        SwitchToMakeMold();
    }

    public void HelmetChosen()
    {
        moldCutter = helmetCutter;
        finishedMold = helmetMold;
        SwitchToMakeMold();
    }

    public void ArrowChosen()
    {
        moldCutter = arrowCutter;
        finishedMold = arrowMold;
        SwitchToMakeMold();
    }

    private void SwitchToMakeMold()
    {
        clayBlob.GetComponent<DropPoint>().SetSlotName(moldCutter.transform.name);
        chooseMoldsScreen.SetActive(false);
        moldCutter.SetActive(true);
        clayBlob.SetActive(true);
        currentState = MoldState.MakingMold;
    }

    private void HandleMoldDragInput()
    {
        if (clayBlob.GetComponent<DropPoint>().Filled())
        {
            MakeMold();
        }
    }

    public void MakeMold()
    {
        clayBlob.SetActive(false);
        moldCutter.SetActive(false);
        finishedMold.SetActive(true);
        StartCoroutine(WaitAfterCreating());
    }

    IEnumerator WaitAfterCreating()
    {
        yield return new WaitForSeconds(1f);
        finishedMold.SetActive(false);
        chooseMoldsScreen.SetActive(true);
        clayBlob.GetComponent<DropPoint>().ResetDropPoint();
        currentState = MoldState.Finished;
        yield break;
    }
}
