using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour {

    public AudioClip fx_rocketengine;
    public AudioClip mu_menu;

    // Use this for initialization
    void Start () {

        SoundManager.getInstance().setMusicVolume(0.8f);
        SoundManager.getInstance().setMusicAndPlay(fx_rocketengine);
        SoundManager.getInstance().setsecondaryMusicVolume(0.7f);
        SoundManager.getInstance().setsecondaryMusicAndPlay(mu_menu);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
