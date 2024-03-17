using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintManager : StationManager
{
    [Header("Sword")]
    [SerializeField] GameObject swordGroup;
    [SerializeField] GameObject swordDefault;
    [SerializeField] GameObject swordRed;
    [SerializeField] GameObject swordBlue;
    [Header("Helmet")]
    [SerializeField] GameObject helmetGroup;
    [SerializeField] GameObject helmetDefault;
    [SerializeField] GameObject helmetRed;
    [SerializeField] GameObject helmetBlue;
    [Header("Arrow")]
    [SerializeField] GameObject arrowGroup;
    [SerializeField] GameObject arrowDefault;
    [SerializeField] GameObject arrowRed;
    [SerializeField] GameObject arrowBlue;

    [Header("Sound")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClipRed;
    [SerializeField] AudioClip _audioClipBlue;

    private PaintState currentState;
    private Weapon stationWeapon;

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
        stationWeapon = GetComponent<StationUtils>().weaponAtStation;

        // Starting UI setup
        if (stationWeapon is Sword)
        {
            swordGroup.SetActive(true);
            swordDefault.SetActive(true);
            swordBlue.SetActive(false);
            swordRed.SetActive(false);
            helmetGroup.SetActive(false);
            arrowGroup.SetActive(false);
        }
        else if (stationWeapon is Helmet)
        {
            helmetGroup.SetActive(true);
            helmetDefault.SetActive(true);
            helmetBlue.SetActive(false);
            helmetRed.SetActive(false);
            swordGroup.SetActive(false);
            arrowGroup.SetActive(false);
        }
        else if (stationWeapon is Arrow)
        {
            arrowGroup.SetActive(true);
            arrowDefault.SetActive(true);
            arrowBlue.SetActive(false);
            arrowRed.SetActive(false);
            swordGroup.SetActive(false);
            helmetGroup.SetActive(false);
        }
    }

    public void RedChosen()
    {
        stationWeapon.PaintRed();

        if (stationWeapon is Sword)
        {
            swordDefault.SetActive(false);
            swordBlue.SetActive(false);
            swordRed.SetActive(true);
        }
        else if (stationWeapon is Helmet)
        {
            helmetDefault.SetActive(false);
            helmetBlue.SetActive(false);
            helmetRed.SetActive(true);
        }
        else if (stationWeapon is Arrow)
        {
            arrowDefault.SetActive(false);
            arrowBlue.SetActive(false);
            arrowRed.SetActive(true);
        }

        _audioSource.clip = _audioClipRed;
        _audioSource.Play();
    }

    public void BlueChosen()
    {
        stationWeapon.PaintBlue();

        if (stationWeapon is Sword)
        {
            swordDefault.SetActive(false);
            swordRed.SetActive(false);
            swordBlue.SetActive(true);
        }
        else if (stationWeapon is Helmet)
        {
            helmetDefault.SetActive(false);
            helmetRed.SetActive(false);
            helmetBlue.SetActive(true);
        }
        else if (stationWeapon is Arrow)
        {
            arrowDefault.SetActive(false);
            arrowRed.SetActive(false);
            arrowBlue.SetActive(true);
        }

        _audioSource.clip = _audioClipBlue;
        _audioSource.Play();
    }

    public void FinishPainting()
    {
        StartCoroutine(WaitAfterPaint());
    }

    IEnumerator WaitAfterPaint()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("finished painting");
        // Setting paint state
        currentState = PaintState.Inactive;
        // Setting weapon state
        stationWeapon.currentState = Weapon.WeaponState.Painted;
        // Restting weapon
        stationWeapon = null;

        ExitGame();
        yield break;
    }
}
