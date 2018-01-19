using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject rocket;
    public MainMenuPanel mainMenuPanel;
    public LevelsPanel levelsPanel;
    public GameObject optionsPanel; 

    public void GoToScoreAttack(string level)
    {
        ScoreAndSpeed.winConditionSetter = "Velocity";
        SceneManager.LoadScene(level);  
    }

    public void GoToHatchRepairs(string level)
    {
        ScoreAndSpeed.winConditionSetter = "Kills";
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

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        mainMenuPanel.showing = false;
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
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
