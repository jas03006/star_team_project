using DG.Tweening.CustomPlugins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;

    [SerializeField] bool playing_bgm = true;
    [SerializeField] bool playing_sfx = true;

    [SerializeField] AudioSource BGM_AudioSource;
    [SerializeField] AudioSource SFX_AudioSource;

    [Header("Audioclip")]
    [SerializeField] private AudioClip click;

    private void Awake()
    {
        //ΩÃ±€≈Ê
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void Switchmode_bgm()
    {
        playing_bgm = !playing_bgm;
    }

    public void Switchmode_sfx()
    {
        playing_sfx = !playing_sfx;
    }

    public void Play_BGM()
    {
        if (playing_bgm == false)
        {
            BGM_AudioSource.Stop();
            return;
        }

        BGM_AudioSource.Play();
    }

    public void Play_Click()
    {
        if (playing_sfx == false)
            return;

        SFX_AudioSource.clip = click;
        SFX_AudioSource.Play();
    }


    


}
