using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/** 
 #-----------------------------------------------
 # Description #
 #  SoundManager is the audio controller.
 #-------------- 
 # Deployment design options #
 #  -> Only one main music (looped).
 #  -> Two varieties of sounds (music and effects).
 #----------------------------
 # Functions #
 #  -> Play music.
 #  -> Switch music.
 #  -> Stop music.
 #  -> Play sound effects.
 #------------
 */
// public class SoundManager : MonoBehaviour {

    //==========================
    //=	  ATTRIBUTES 
    //==========================
    /** There can be only one instance of SoundManager. 
     */
 //  private static SoundManager instance = null;

    /** AudioSource component. 
     *  This component plays the game music.
     */
 //  private AudioSource musicSource;

    /** AudioSource component.
     *  This component plays the sound effects. 
     */
//   private AudioSource efxSource;

    //==========================
    //=    PRIVATE METHODS     =
    //==========================
    /** Creates the components and configures them. 
     */
//    private void setup() {
   /*    efxSource =   gameObject.AddComponent<AudioSource>();
       musicSource = gameObject.AddComponent<AudioSource>();
       musicSource.loop = true;
    } */

    /** Checks if SoundManager already exists.
     *  If true, destroys it, we only want one SoundManager.
     */
 //   private void Awake() {

      /*  if (instance == null) {
            instance = this;
            setup();
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }*/

    //==========================
    //=     PUBLIC METHODS     =
    //==========================
    /** Used to switch and play looped sound clips. 
     */
    /*public static void setMusicAndPlay(AudioClip song) {

        // If song is null, music doesn't switch.
        if (song == null)
            return;

        // If the music is playing we stop it..
        if (song != null && musicSource.isPlaying)
            musicSource.Stop();

        // ..and switch from sound clip.
        musicSource.clip = song;
        musicSource.Play();
    }*/

    /** Plays the music. 
     */
    /*public static void playMusic() {
        musicSource.Play();
    }

    /** Stops the music. 
     */
    /*public static void stopMusic() {
        musicSource.Stop();
    }

    /** Used to play single sound clips effects.
     */
   /* public static void playSoundEffect(AudioClip clip) {
        efxSource.clip = clip;
        efxSource.Play();
    }

}*/

