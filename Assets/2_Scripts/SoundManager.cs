using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sys_error;
    public AudioClip sys_succ;
    public AudioClip scanned;
    public AudioClip ring;
    public AudioClip message;
    public AudioClip KR_msg1;
    public AudioClip EN_msg1;

    public static SoundManager instance;

    void Awake(){
        if(SoundManager.instance == null){
            SoundManager.instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    /**
     * @Function: Sound Manager Function
     * 
     * @Author: Minjung KIM
     * @Date: 2020.May.6
     */
    public void PlaySound(string audioName, int lang){
        
        // Sys Scanned
        if(audioName.Equals(GlobalEnv.SOUND_SCANNED)){
            audioSource.PlayOneShot(scanned);

        // Sys SUCC
        }else if(audioName.Equals(GlobalEnv.SOUND_SUCC)){
            audioSource.PlayOneShot(sys_succ);

        // Sys Error 
        }else if (audioName.Equals(GlobalEnv.SOUND_ERROR)){
            audioSource.PlayOneShot(sys_error);

        // Calling sound
        }else if (audioName.Equals(GlobalEnv.SOUND_CALL)){
            audioSource.PlayOneShot(ring);

        // Message sound
        }else if (audioName.Equals(GlobalEnv.SOUND_MESSAGE)){
            audioSource.PlayOneShot(message);

        // Vocal
        }else if (audioName.Equals(GlobalEnv.SOUND_VOCAL)){
            if(lang == 2){
                audioSource.PlayOneShot(EN_msg1);
            }else if (lang == 0){
                audioSource.PlayOneShot(KR_msg1);
            }
        }
    }
}