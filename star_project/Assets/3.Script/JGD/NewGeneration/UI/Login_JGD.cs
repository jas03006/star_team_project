using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;
using UnityEngine.SceneManagement;

public class Login_JGD : LoginBase_JGD
{
    [SerializeField] private Image imageID;
    [SerializeField] private TMP_InputField inputFieldID;
    [SerializeField] private Image imagePW;
    [SerializeField] private TMP_InputField inputFieldPW;

    [SerializeField] private Button btnLogin;

    [SerializeField] GameObject Login;
    [SerializeField] GameObject Signup;

    [SerializeField] private SceneNames nextScene;
    public void OnclickLoin()
    {
        ResetUI(imageID, imagePW);

        if (IsFieldDateEmpty(imageID, inputFieldID.text, "아이디")) return;
        if (IsFieldDateEmpty(imagePW, inputFieldPW.text, "비밀번호")) return;

        btnLogin.interactable = false;

        StartCoroutine(nameof(LoginProgress));

        ResponseToLogin(inputFieldID.text, inputFieldPW.text);
    }

    private void ResponseToLogin(string ID, string PW)
    {
        //서버에 로그인 요청
        Backend.BMember.CustomLogin(ID, PW, callback =>
        {
            StopCoroutine(nameof(LoginProgress));
            if ( callback.IsSuccess())
            {
                //SetMessage($"{inputFieldID.text}님 환영합니다");//로그인 성공
                Debug.Log("로그인 성공");
                SceneManager.LoadScene(nextScene.ToString());
            }
            else
            {
                //로그인 실패(실패시 다시로그인하기 위해 로그인 버튼 상호작용 활성화
                btnLogin.interactable = true;

                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 401:
                        message = callback.GetMessage().Contains("customId") ? "존재하지 않는 아이디 입니다." : "잘못된 비밀번호 입니다.";
                        break;
                    case 403:
                        message = callback.GetMessage().Contains("user") ? "차단당한 유저입니다." : "차단당한 디바이스입니다.";
                        break;
                    case 410:
                        message ="탈퇴가 진행중인 유저입니다.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("비밀번호"))
                {
                    GuideForIncorrenctltEnteredData(imagePW, message);
                }
                else
                {
                    GuideForIncorrenctltEnteredData(imageID, message);
                }

            }

        });
    }
    private IEnumerator LoginProgress()
    {
        float time = 0;

        while (true)
        {
            time += Time.deltaTime;

            //SetMessage($"로그인 중 입니다...{time:F1})");

            yield return null;
        }
    }
    public void Login_Scene()
    {
        Login.SetActive(true);
        Signup.SetActive(false);
    }
    public void Signup_Scene()
    {
        Login.SetActive(false);
        Signup.SetActive(true);
    }
}