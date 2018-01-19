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
 public class SoundManager : MonoBehaviour {

    //==========================
    //=	  ATTRIBUTES 
    //==========================
    /** There can be only one instance of SoundManager. 
     */
    private static SoundManager instance = null;
    public static SoundManager getInstance() {

        return instance;
    } 

    /** AudioSource component. 
     *  This component plays the game music.
     */
   private AudioSource musicSource;

    /** AudioSource component.
     *  This component plays the sound effects. 
     */
   private AudioSource efxSource;

   private AudioSource secondarymusicSource; 
     

    //==========================
    //=    PRIVATE METHODS     =
    //==========================
    /** Creates the components and configures them. 
     */

   private void setup() {
       efxSource =   gameObject.AddComponent<AudioSource>();
       musicSource = gameObject.AddComponent<AudioSource>();
       musicSource.loop = true;
       secondarymusicSource = gameObject.AddComponent<AudioSource>();
       secondarymusicSource.loop = true;
    } 

    /** Checks if SoundManager already exists.
     *  If true, destroys it, we only want one SoundManager.
     */
  private void Awake() {

        if (instance == null) {
            instance = this;
            setup();
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    //==========================
    //=     PUBLIC METHODS     =
    //==========================
    /** Used to switch and play looped sound clips. 
     */
    public void setMusicAndPlay(AudioClip song) {

        // If song is null, music doesn't switch.
        if (song == null)
            return;

        // If the music is playing we stop it..
        if (musicSource.isPlaying)
            musicSource.Stop();

        // ..and switch from sound clip.
        musicSource.clip = song;
        musicSource.Play();
    }

    public void setsecondaryMusicAndPlay(AudioClip song)
    {

        // If song is null, music doesn't switch.
        if (song == null)
            return;

        // If the music is playing we stop it..
        if (secondarymusicSource.isPlaying)
            secondarymusicSource.Stop();

        // ..and switch from sound clip.
        secondarymusicSource.clip = song;
        secondarymusicSource.Play();
    }

    /** Plays the music. 
     */
    public void playMusic() {
        musicSource.Play();
        secondarymusicSource.Play();
    }

    /** Stops the music. 
     */
    public void stopMusic() {
        musicSource.Stop();
        secondarymusicSource.Stop();
    }

    public void setMusicVolume(float m_volume)
    {
        musicSource.volume = m_volume;
    }

    public void setMusicPitch(float m_pitch)
    {
        musicSource.pitch = m_pitch;
    }
    
    public void setsecondaryMusicVolume(float m_volume)
    {
        secondarymusicSource.volume = m_volume;
    }
    public void setsecondaryMusicPitch(float m_pitch)
    {
        secondarymusicSource.pitch = m_pitch;
    }

    public void setSFXVolume(float SFXvolume)
    {
        efxSource.volume = SFXvolume;
    }

    /** Used to play single sound clips effects.
     */
    public void playSoundEffect(AudioClip clip, float volume, float pitch) {
        efxSource.volume = volume; // * sfxVolume (range 0-1)
        efxSource.pitch = pitch;
        efxSource.clip = clip;
        efxSource.Play();
    }

}

