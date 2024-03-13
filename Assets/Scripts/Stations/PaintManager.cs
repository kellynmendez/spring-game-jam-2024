using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintManager : StationManager
{
    [SerializeField] GameObject swordDefault;
    [SerializeField] GameObject swordRed;
    [SerializeField] GameObject swordBlue;
    //[SerializeField] GameObject helmetDefault;
    //[SerializeField] GameObject helmetRed;
    //[SerializeField] GameObject helmetBlue;
    //[SerializeField] GameObject arrowDefault;
    //[SerializeField] GameObject arrowRed;
    //[SerializeField] GameObject arrowBlue;

    private PaintState currentState;

    private enum PaintState
    {
        Inactive,
        Painting,
        Finished
    }

    private void Awake()
    {
        playerSM = FindObjectOfType<PlayerSM>();
        gameScreen.SetActive(false);
        currentState = PaintState.Inactive;
    }

    public override void StartGame()
    {
        currentState = PaintState.Painting;
        gameScreen.SetActive(true);
    }

    private void Update()
    {
        if (currentState == PaintState.Inactive)
            return;
        else if (currentState == PaintState.Finished)
        {
            currentState = PaintState.Inactive;
            ExitGame();
        }
    }

    public void RedChosen()
    {
        swordDefault.SetActive(false);
        swordBlue.SetActive(false);
        swordRed.SetActive(true);
    }

    public void BlueChosen()
    {
        swordDefault.SetActive(false);
        swordRed.SetActive(false);
        swordBlue.SetActive(true);
    }

    public void FinishPainting()
    {
        StartCoroutine(WaitAfterPaint());
    }

    IEnumerator WaitAfterPaint()
    {
        yield return new WaitForSeconds(0.5f);
        swordDefault.SetActive(true);
        swordRed.SetActive(false);
        swordBlue.SetActive(false);
        currentState = PaintState.Finished;
        yield break;
    }
}
