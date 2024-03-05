using System.Collections;
using System;
using System.Net;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using BackEnd;
using Facebook.Unity;

using UnityEngine.SceneManagement;
using LitJson;

public class BackendFederationAuth_TG : MonoBehaviour
{
    [SerializeField] private SceneNames nextScene;
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else {
            FB.ActivateApp();
        }
    }

    private void InitCallback() {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else {
            Debug.Log("Facebook SDK �ʱ�ȭ ����");
        }
    }

    private void OnHideUnity(bool isGameShown) {
        if (!isGameShown) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }
    }
    public void OnCFBLogin() {
        var perms = new List<string>() { "public_profile", "email", "name" }; // �̸��� ������ �䱸�ϱ� ���� ���Դϴ�.
        FB.LogInWithReadPermissions(perms, AuthCallback);
    
    }

    private void AuthCallback(ILoginResult result) {
        if (FB.IsLoggedIn) {
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            string facebookToken = aToken.TokenString;

            BackendReturnObject bro = Backend.BMember.AuthorizeFederation(facebookToken, FederationType.Facebook, "���̽��� �α���");

            if (bro.IsSuccess())
            {
                if (bro.GetStatusCode() == "201") { //ȸ�������� ���
                    Debug.Log("���̽��� ȸ������ ����");
                    var web = new WebClient();
                    //web.Encoding = Encoding.UTF8;
                    string name_ = web.DownloadString("https://graph.facebook.com/me?access_token="+facebookToken);
                    name_ = name_.Split(",")[0].Split(":")[1].Replace("\"", "");
                    name_ = convert_from_unicode(name_).Replace(" ", "_");
                    //name_ = Encoding.GetEncoding("EUC-KR").GetString(Encoding.GetEncoding("ISO-8859-1").GetBytes(name_)).Replace(" ", "_");
                    BackendGameData_JGD.Instance.GameDataInsert(name_) ;
                }

                Debug.Log("���̽��� �α��� ����");

                

                BackendGameData_JGD.Instance.GameDataGet();
                BackendChart_JGD.Instance.ChartDataGet();

                if (BackendGameData_JGD.userData == null)
                {
                    BackendGameData_JGD.Instance.GameDataInsert(bro.FlattenRows()["email"].ToString());
                }
                
                Debug.Log($"exp : {BackendGameData_JGD.userData.housing_Info.exp}");
                Debug.Log($"item_ID : {BackendGameData_JGD.userData.housing_Info.objectInfos[0].item_ID}");
                SceneManager.LoadScene(nextScene.ToString());
            }
            else {
                Debug.LogError("���̽��� �α��� ����");
            }
        }
    }
    private string convert_from_unicode(string str)

    {

        string rtstr = "";
        for (int i = 2; i < str.Length; i += 6)
        {
            string str1 = str.Substring(i, 4);
            rtstr += (char)Int16.Parse(str1, System.Globalization.NumberStyles.HexNumber);
        }
        return rtstr;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
