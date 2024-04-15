using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���� �� �ʿ��� ����� �ҽ��� ������ �� �ִ� Ŭ����
/// ����� �ҽ����� �޼��带 ���� �ٷ� �����ü� �ְ� �س���.
/// </summary>
public class AudioManager : MonoBehaviour
{
    /*
     �޼����
    BGM = BGM_myplanet() or BGM_catchingstar()
    SFX = SFX_Clip�̸� (ex:SFX_chapter_open)
    ETC = �ּ�����
     */

    public static AudioManager instance;  
    public bool playing_bgm = true;
    public bool playing_sfx = true;
    public bool playing_vibration = false;

    [SerializeField] public AudioSource BGM_AudioSource;
    [SerializeField] public AudioSource SFX_AudioSource;
    [SerializeField] public AudioSource SFX_Item_AudioSource;

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
    [Header("StageSound_JGD")]
    [SerializeField] private AudioClip Using_Shield;
    [SerializeField] private AudioClip Using_Magnet;
    [SerializeField] private AudioClip Using_SizeUp;
    [SerializeField] private AudioClip Using_SizeDown;
    [SerializeField] private AudioClip Using_SpeedUp;
    [Header("StageSound_JGD")]
    [SerializeField] public List<AudioClip> Theme01 = new List<AudioClip>();
    [SerializeField] public List<AudioClip> Theme02 = new List<AudioClip>();
    [SerializeField] public List<AudioClip> Theme03 = new List<AudioClip>();
    [SerializeField] public List<AudioClip> Theme04 = new List<AudioClip>();
    [SerializeField] public List<AudioClip> Theme05 = new List<AudioClip>();

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
    public void SFX_Using_Shield()
    {
        if (isPlaying(false))
        {
            SFX_Item_AudioSource.clip = Using_Shield;
            SFX_Item_AudioSource.Play();
        }
    }
    public void SFX_Using_Magnet()
    {
        if (isPlaying(false))
        {
            SFX_Item_AudioSource.clip = Using_Magnet;
            SFX_Item_AudioSource.Play();
        }
    }
    public void SFX_Using_SizeUp()
    {
        if (isPlaying(false))
        {
            SFX_Item_AudioSource.clip = Using_SizeUp;
            SFX_Item_AudioSource.Play();
        }
    }
    public void SFX_Using_SizeDown()
    {
        if (isPlaying(false))
        {
            SFX_Item_AudioSource.clip = Using_SizeDown;
            SFX_Item_AudioSource.Play();
        }
    }
    public void SFX_Using_SpeedUp()
    {
        if (isPlaying(false))
        {
            SFX_Item_AudioSource.clip = Using_SpeedUp;
            SFX_Item_AudioSource.Play();
        }
    }

    #endregion
    #region ETC
    public bool Switchmode_bgm()//ȯ�漳������ bgm on/off�� ����
    {
        playing_bgm = !playing_bgm;
        return playing_bgm;
    }

    public bool Switchmode_sfx()//ȯ�漳������ sfx on/off�� ����
    {
        playing_sfx = !playing_sfx;
        return playing_sfx;
    }
    public bool Switchmode_vibration()//ȯ�漳������ ������� on/off�� ����
    {
        playing_vibration = !playing_vibration;
        return playing_vibration;
    }

    public bool isPlaying(bool isBGM) //onoff����Ȯ�� - true�ϰ�� BGM, false�ϰ�� sfx 
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
