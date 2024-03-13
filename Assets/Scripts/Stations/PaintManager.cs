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
        Painting
    }

    private void Awake()
    {
        playerSM = FindObjectOfType<PlayerSM>();
        gameScreen.SetActive(false);
        currentState = PaintState.Inactive;
    }

    public override void StartGame()
    {
        inactive = false;
        currentState = PaintState.Painting;
        gameScreen.SetActive(true);
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
        currentState = PaintState.Inactive;
        ExitGame();
        yield break;
    }
}
