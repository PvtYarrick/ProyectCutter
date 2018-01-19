using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndSpeed : MonoBehaviour {

    public Player player;
    public Avatar avatar;
    private float velocitytoWin = 6f;
    private float enemiestoWin = 1f;
    public static string winConditionSetter;

    public GameSceneManager manager;
    public static int deadEnemies;

    public static bool iveWon = false;

    public Text scoreLabel, distanceLabel, velocityLabel, enemiesKilled, shotsPowered, speedBoosted;

    public void Awake()
    {
        iveWon = false;
        deadEnemies = 0;
    }
    public void Start()
    {
        
        shotsPowered.text = "Shield deactivated!";
        speedBoosted.text = "N0rmal speed";
        
       
    }
    

    public void Update()
    {
        enemiesKilled.text = ("Hatches closed " + deadEnemies.ToString());
        scoreLabel.text = ((int)(player.distanceTraveled * 10f)).ToString();
        if (player.velocity >= velocitytoWin && winConditionSetter == "Velocity")
        {
            iveWon = true;
            manager.WinConMet();

        }else if (deadEnemies >= enemiestoWin && winConditionSetter == "Kills")
        {
            iveWon = true;
            manager.WinConMet();
        }

        if (avatar.poweredUp == true)
        {
            shotsPowered.text = "Shots powered!";
            
        }else
        {
            shotsPowered.text = "Regular shots";
        }

        if (avatar.goingFast == true)
        {
            speedBoosted.text = "Double speed!";
        }
        else
        {
            speedBoosted.text = "Normal speed";
        }
    }

    public void SetValues(float distanceTraveled, float velocity)
    {
        distanceLabel.text = ("Sc0re " + (int)(distanceTraveled * 10f)).ToString();
        velocityLabel.text = (((int)(velocity * 15f)).ToString() + " Km/H");
    }
}
