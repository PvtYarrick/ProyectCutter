using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

    public void DeadPlayer()
    {
        SceneManager.LoadScene("YouLose");
    }

    public void WinConMet()
    {
        SceneManager.LoadScene("YouWin");
    }
}
