using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects_lost : MonoBehaviour {

    public AudioClip fx_rocketengine;
    public AudioClip fx_beep;

    // Use this for initialization
    void Start () {

            SoundManager.getInstance().setMusicVolume(0.7f);
            SoundManager.getInstance().setMusicAndPlay(fx_rocketengine);
            SoundManager.getInstance().setsecondaryMusicVolume(0.4f);
            SoundManager.getInstance().setsecondaryMusicPitch(0.4f);
            SoundManager.getInstance().setsecondaryMusicAndPlay(fx_beep);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
