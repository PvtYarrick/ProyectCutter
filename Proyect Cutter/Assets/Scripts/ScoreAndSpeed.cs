using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndSpeed : MonoBehaviour {

    public Player player;

    public Text scoreLabel, distanceLabel, velocityLabel;

    //public Multiplier multiplier;

    public void Update()
    {
        scoreLabel.text = ((int)(player.distanceTraveled * 10f)).ToString();      
    }

    public void SetValues(float distanceTraveled, float velocity)
    {
        distanceLabel.text = ("Sc0re " + (int)(distanceTraveled * 10f)).ToString();
        velocityLabel.text = (((int)(velocity * 10f)).ToString() + " Km/H");
    }
}
