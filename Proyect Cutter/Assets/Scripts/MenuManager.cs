using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject rocket;
    public MainMenuPanel mainMenuPanel;
    public LevelsPanel levelsPanel;

    public void GoToScene(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void OpenLevelPanel()
    {
        mainMenuPanel.showing = false;
        levelsPanel.gameObject.SetActive(true);
        levelsPanel.showing = true; 
    }

    public void CloseLevelPanel()
    {
        levelsPanel.showing = false;
        mainMenuPanel.gameObject.SetActive(true);
        mainMenuPanel.showing = true;
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LaunchRocket(string level)
    {
        Animator anim = rocket.GetComponent<Animator>();
        anim.enabled = true;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(level);
    }




   





}
