using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BackEnd;
public class UIManager_YG : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] TMP_Text nickname_text;
    [SerializeField] TMP_Text title_text;
    [SerializeField] Image profile_image;

    public static UIManager_YG Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(canvas);
        SceneManager.sceneLoaded += LoadedsceneEvent;

        nickname_text.text = Backend.UserNickName;
        title_text.text = BackendGameData_JGD.userData.title_adjective.ToString() +" "+ BackendGameData_JGD.userData.title_noun.ToString();
        profile_image.sprite = SpriteManager.instance.Num2Sprite(BackendGameData_JGD.userData.profile_picture);
    }

    private void LoadedsceneEvent(Scene arg0, LoadSceneMode arg1)
    {
        if (SceneManager.GetActiveScene().name == "Stage" || SceneManager.GetActiveScene().name == "My_Planet_TG")
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }
    }
}
