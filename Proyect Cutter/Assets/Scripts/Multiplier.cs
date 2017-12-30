using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Multiplier : MonoBehaviour {

    public Text ScoreMultiplier_text;
    public Animator ScoreMultiplier_anim;
    public static uint _Multiplier;
    public static uint MPCounter;
    public static int killing_countdown;
    public const int count = 500;

    // Use this for initialization
    void Start () {
        ScoreMultiplier_anim = GetComponent<Animator>();
        ScoreMultiplier_text = GetComponent<Text>();
        MPCounter = 0;
        _Multiplier = 1;
        killing_countdown = count;
    }
	
	// Update is called once per frame
	void Update () {

        if (MPCounter >= 3)
        {
            ScoreMultiplier_anim.SetTrigger("MultiplierBump");
            _Multiplier = _Multiplier + 1;
            ScoreMultiplier_text.text = "x" + _Multiplier;
            MPCounter = 0;
            killing_countdown = count;

        }
        else if (_Multiplier > 1)
        {
            killing_countdown--;
        }
        if (killing_countdown <= 0 && _Multiplier > 1)
        {
            killing_countdown = count;
            _Multiplier = _Multiplier - 1;
            ScoreMultiplier_text.text = "x" + _Multiplier;
            ScoreMultiplier_anim.SetTrigger("MultiplierBump");
        }
    }
}
