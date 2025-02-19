using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{


  
    public AudioSource SFXSource;
    
    public AudioClip[] audioClipsPlayer;
    public AudioClip audioClipRival;
    public AudioClip button;


    public AudioSource musicSource;

    public AudioClip theme;

    public AudioClip metal;

 


    public void StartTheme(){

        if (!musicSource.isPlaying){

     musicSource.clip = theme;
       // musicSource.volume = 0.1f;
        musicSource.Play();

        }

  
    }

    public void StopTheme(){
        musicSource.clip = theme;
        musicSource.Pause();
    }

    public void PauseMusic(){

     if (musicSource.isPlaying)
    {
        musicSource.Pause();
    }
    else
    {
        musicSource.Play();
    }
        
    }


    public void PlayPlayerSFX()
    {

        // to ensure no overlapping of the audio
        if (!SFXSource.isPlaying){
       // SFXSource.PlayOneShot(clip);

        SFXSource.clip = audioClipsPlayer[Random.Range(0, audioClipsPlayer.Length)];
        SFXSource.Play();

        }
    }

    // public void StopPlayerSFX(){
    //     SFXSource.clip = audioClipsPlayer[Random.Range(0, audioClipsPlayer.Length)];
    //     SFXSource.Pause();
    // }


    public void PlayRivalSFX(){

      if (!SFXSource.isPlaying){
            SFXSource.clip = audioClipRival;
            SFXSource.Play();
      }
        
    }

    public void StopRivalSFX(){
            SFXSource.clip = audioClipRival;
            SFXSource.Stop();
        
    }



   public void ButtonSFX(){
        SFXSource.clip = button;
        SFXSource.Play();
   }


   public void MetalTune(){
       
        
        musicSource.clip = metal;
        musicSource.Play();  
        // musicSource.PlayOneShot(metal); 
        
   }

   public void StopMetalTune(){

    
    musicSource.clip = metal;
     musicSource.Pause();
     

   }



}


// with playoneshot it continues but then i have to remove start and stop theme from 
// chara controller script.