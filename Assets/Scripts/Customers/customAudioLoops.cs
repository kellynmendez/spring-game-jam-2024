using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customAudioLoops : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource _hammerAudioToLoop;
    int _secondsToWait;
    void Start()
    {
        _secondsToWait = 10;
        StartCoroutine(WaitToPlay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HammerLoop()
    {
        _hammerAudioToLoop.Play();
        _secondsToWait = Random.Range(25, 40);
        StartCoroutine(WaitToPlay());
    }
    private IEnumerator WaitToPlay()
    {
        yield return new WaitForSeconds(_secondsToWait);
        HammerLoop();
    }
}
