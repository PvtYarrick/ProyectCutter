using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour {

    public void Retry()
    {
        ScoreAndSpeed.iveWon = false;
        SceneManager.LoadScene("Game");
    }

    public void BackToMM()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
