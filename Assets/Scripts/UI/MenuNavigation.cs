using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{

    private string _sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OpenScene(string SceneName)
    {
        _sceneName = SceneName;
        StartCoroutine(Wait());
    }
    public void OpenCredits(GameObject _creditsScreen)
    {
        _creditsScreen.SetActive(true);
    }
    public void OpenMainMenu(GameObject _creditsScreen) 
    {
        _creditsScreen.SetActive(false);
    }
    public void CloseGame()
    {
        StartCoroutine(Wait());
        Application.Quit();
    }
    public void OpenScreen(GameObject screen)
    {
        screen.SetActive(true);
    }
    public void CloseScreen(GameObject screen)
    {
        screen.SetActive(false);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(_sceneName);
    }
}
