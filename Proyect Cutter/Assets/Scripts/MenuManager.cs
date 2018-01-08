using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject rocket;

    public void GoToScene(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void GoToLevel(string level)
    {
        StartCoroutine(LaunchRocket(level));
    }

    IEnumerator LaunchRocket(string level)
    {
        Animator anim = rocket.GetComponent<Animator>();
        anim.enabled = true;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(level);
    }

   





}
