using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{

  public AudioSource playerSource;

  
    public AudioClip[] audioClipsPlayer;
  
    
  public AudioSource rivalSource;
    public AudioClip audioClipRival;

  public AudioSource SFXSource;
    public AudioClip button;
    public AudioClip win;
    public AudioClip timesUp;
    public AudioClip caught;

  public AudioSource musicSource;
    private AudioClip currentClip;
    public AudioClip theme;
    public AudioClip metal;
    public AudioClip home;
      

  private float metalTime = 0f; //store position of metal tune
  private float themeTime = 0f; // store position of theme tune
 
  private bool isMusicPaused = false; // Track pause state


    

 private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene loaded event
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to prevent memory leaks
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
      if(scene.name == "HomePage"){
          StartHomeTune();
      }
      
      if(scene.name == "Win01"){
          WinSFX();
      }
      if(scene.name == "Lose_timeout"){
          TimesUpSFX();
      }
      if(scene.name == "Lose_caught"){
        CaughtSFX();
      }
           

 }





public void PauseMusic()
{
    if (isMusicPaused)
    {
        musicSource.mute = false; // Unmute audio
        musicSource.Play();       // Resume playback
        isMusicPaused = false;
        Debug.Log("Music Resumed");
    }
    else
    {
        musicSource.Pause();      // Pause playback
        musicSource.mute = true;  // Mute audio to avoid weird silent bugs
        isMusicPaused = true;
        Debug.Log("Music Paused");
    }
}


    public void PlayPlayerSFX()
    {

        // to ensure no overlapping of the audio
        if (!playerSource.isPlaying){
       // SFXSource.PlayOneShot(clip);

        playerSource.clip = audioClipsPlayer[Random.Range(0, audioClipsPlayer.Length)];
        playerSource.Play();

        }
    }

    // public void StopPlayerSFX(){
    //     SFXSource.clip = audioClipsPlayer[Random.Range(0, audioClipsPlayer.Length)];
    //     SFXSource.Pause();
    // }


    public void PlayRivalSFX(){

      if (!rivalSource.isPlaying){
            rivalSource.clip = audioClipRival;
            rivalSource.Play();
      }
        
    }

    public void StopRivalSFX(){
            rivalSource.clip = audioClipRival;
            rivalSource.Stop();
        
    }



  public void ButtonSFX(){
    SFXSource.clip = button;
    SFXSource.Play();
  }


  public void WinSFX(){
    SFXSource.clip = win;
    SFXSource.Play();
  }

  public void TimesUpSFX(){
    SFXSource.clip = timesUp;
    SFXSource.Play();
  }


  public void CaughtSFX(){
    SFXSource.clip = caught;
    SFXSource.Play();
  }



    public void MetalTune()
  {

    themeTime = musicSource.time; // Save theme position
    currentClip = metal;
    musicSource.clip = metal;
    musicSource.time = metalTime; // Resume from saved position
    musicSource.Play();

  }

  public void PauseMetalTune()
  {
    if (musicSource.isPlaying && currentClip == metal)
    {
      metalTime = musicSource.time; // Save metal position
      musicSource.Pause();
    }
  }



  public void StartTheme()
  {

    metalTime = musicSource.time; // Save metal position
    currentClip = theme;
    musicSource.clip = theme;
    musicSource.time = themeTime; // Resume from saved position
    musicSource.Play();
  }

  public void PauseTheme()
  {
    if (musicSource.isPlaying && currentClip == theme)
    {
      themeTime = musicSource.time; // Save theme position
      musicSource.Pause();
    }
  }

  public void StopAudio()
  {
    musicSource.mute = true;
    rivalSource.mute = true;
    playerSource.mute = true;
    SFXSource.mute = true;
  }


  public void StartHomeTune(){
    currentClip = home;
    musicSource.clip = home;
    musicSource.Play();
  }


}



// public void StartTheme(){

    //   //  if (!musicSource.isPlaying){

    //     musicSource.clip = theme;
    //    // musicSource.volume = 0.1f;
    //     musicSource.Play();

    //   //'  }

  
    // }

    // public void PauseTheme(){
    //     if(musicSource.clip = theme){
    //    // musicSource.clip = theme;
    //     musicSource.Pause();
    //     }
    // }

