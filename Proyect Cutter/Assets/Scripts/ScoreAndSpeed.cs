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

    public Text scoreLabel, distanceLabel, velocityLabel, enemiesKilled, shieldActive, speedBoosted;

    public void Awake()
    {
        iveWon = false;
    }
    public void Start()
    {
        
        shieldActive.text = "Shield deactivated!";
        speedBoosted.text = "N0rmal speed";
        deadEnemies = 0;
       
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

        if (avatar.isShieldUp == true)
        {
            shieldActive.text = "Shield activated!";
            
        }else
        {
            shieldActive.text = "Shield deactivated!";
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
