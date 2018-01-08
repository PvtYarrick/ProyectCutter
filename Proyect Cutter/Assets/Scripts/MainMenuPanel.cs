using System.Collections;
using UnityEngine;

public class MainMenuPanel : MonoBehaviour {

    Animator anim;

    public bool showing = true;

    private void Start()
    {
        anim = GetComponent<Animator>();

        anim.SetTrigger("Show");
    }

   

    private void Update()
    {
        if (showing == false)
        {
            anim.SetTrigger("Hide");
            StartCoroutine(WaitABit());
        }
    }


    IEnumerator WaitABit()
    {
        
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
