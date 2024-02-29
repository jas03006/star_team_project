using UnityEngine;

public class AudioManager : MonoBehaviour
{

    /*
     메서드명
    BGM = BGM_myplanet() or BGM_catchingstar()
    SFX = SFX_Clip이름 (ex:SFX_chapter_open)
    ETC = Switchmode_bgm() or Switchmode_sfx()
     */
    public static AudioManager instance;  
    public bool playing_bgm = true;
    public bool playing_sfx = true;

    [SerializeField] AudioSource BGM_AudioSource;
    [SerializeField] AudioSource SFX_AudioSource;

    [Header("Audioclip")]
    [SerializeField] private AudioClip catchingstar_BGM;
    [SerializeField] private AudioClip myplanet_BGM;
    [SerializeField] private AudioClip chapter_open;
    [SerializeField] private AudioClip clear_star1;
    [SerializeField] private AudioClip clear_star2;
    [SerializeField] private AudioClip clear_star3;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip collect_heart;
    [SerializeField] private AudioClip collect_item;
    [SerializeField] private AudioClip collect_star;
    [SerializeField] private AudioClip game_over;
    [SerializeField] private AudioClip hearvest_start_and_get;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip stage_clear;
    [SerializeField] private AudioClip stage_click;

    private void Awake()
    {
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

    #region BGM
    public void BGM_play()
    {
        if (isPlaying(true))
        {
            BGM_AudioSource.Play();
        }
    }
    public void BGM_myplanet()
    {
        if (isPlaying(true))
        {
            BGM_AudioSource.clip = myplanet_BGM;
            BGM_AudioSource.Play();
        }
    }
    public void BGM_catchingstar()
    {
        if (isPlaying(true))
        {
            BGM_AudioSource.clip = catchingstar_BGM;
            BGM_AudioSource.Play();
        }
    }

    #endregion
    #region SFX
    public void SFX_chapter_open()
    {
        if (isPlaying(false))
        {
            SFX_AudioSource.clip = chapter_open;
            SFX_AudioSource.Play();
        }
    }    
    public void SFX_clear_star1()
    {
        if (isPlaying(false))
        {
            SFX_AudioSource.clip = clear_star1;
            SFX_AudioSource.Play();
        }
    }
    public void SFX_clear_star2()
    {
        if (isPlaying(false))
        {
            SFX_AudioSource.clip = clear_star2;
            SFX_AudioSource.Play();
        }
    }

    public void SFX_clear_star3()
    {
        if (isPlaying(false))
        {
            SFX_AudioSource.clip = clear_star3;
            SFX_AudioSource.Play();
        }
    }
    public void SFX_Click()
    {
        if (isPlaying(false))
        {
            SFX_AudioSource.clip = click;
            SFX_AudioSource.Play();
        }
    }    

    public void SFX_collect_heart()
    {
        if (isPlaying(false))
        {
            SFX_AudioSource.clip = collect_heart;
            SFX_AudioSource.Play();
        }
    }

    public void SFX_collect_item()
    {
        if (isPlaying(false))
        {
            SFX_AudioSource.clip = collect_item;
            SFX_AudioSource.Play();
        }
    }
    public void SFX_collect_star()
    {
        if (isPlaying(false))
        {
            SFX_AudioSource.clip = collect_item;
            SFX_AudioSource.Play();
        }
    }

    public void SFX_game_over()
    {
        if (isPlaying(false))
        {
            SFX_AudioSource.clip = game_over;
            SFX_AudioSource.Play();
        }
    }

    public void SFX_hearvest_start_and_get()
    {
        if (isPlaying(false))
        {
            SFX_AudioSource.clip = hearvest_start_and_get;
            SFX_AudioSource.Play();
        }
    }

    public void SFX_hit()
    {
        if (isPlaying(false))
        {
            SFX_AudioSource.clip = hit;
            SFX_AudioSource.Play();
        }
    }    
    public void SFX_stage_clear()
    {
        if (isPlaying(false))
        {
            SFX_AudioSource.clip = stage_clear;
            SFX_AudioSource.Play();
        }
    }

    public void SFX_stage_click()
    {
        if (isPlaying(false))
        {
            SFX_AudioSource.clip = stage_click;
            SFX_AudioSource.Play();
        }
    }


    #endregion
    #region ETC
    public bool Switchmode_bgm()
    {
        playing_bgm = !playing_bgm;
        return playing_bgm;
    }

    public bool Switchmode_sfx()
    {
        playing_sfx = !playing_sfx;
        return playing_sfx;
    }
    public bool isPlaying(bool isBGM)
    {
        if (isBGM)
        {
            if (playing_bgm == false)
            {
                BGM_AudioSource.Stop();
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (playing_sfx == false)
            {
                SFX_AudioSource.Stop();
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    #endregion
}
