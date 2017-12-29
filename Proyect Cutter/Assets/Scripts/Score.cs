using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public static Text score_text;
    public static float score;
    public static uint score_add = 1;
    private float scoreMeasurer = 0.1f;
    //public Text end_text;
    //public Text winText;
    //public GameObject BluePowerUp;
    //private AudioSource startSound;

    // Use this for initialization
    void Start () {

        score_text = GetComponentInChildren<Text>();
        //end_text.gameObject.SetActive(false);
        score = 0;
        //startSound = GetComponent<AudioSource>();
        //startSound.Play();
    }
	
	// Update is called once per frame
	void Update () {

        score_text.text = "" + (int)score;
        score = score + scoreMeasurer;
    }
}
