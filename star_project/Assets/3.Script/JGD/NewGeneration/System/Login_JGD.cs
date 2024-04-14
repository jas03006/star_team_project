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

    [SerializeField] Image PWbtn_img;
    [SerializeField] Sprite standard;
    [SerializeField] Sprite passward;

    [SerializeField] private Button btnLogin;

    [SerializeField] GameObject Login;
    [SerializeField] GameObject Signup;

    [SerializeField] GameObject Done;
    [SerializeField] GameObject DoneX;
    [SerializeField] TMP_Text DoneX_text;

    [SerializeField] private SceneNames nextScene;
    public void OnclickLoin() //로그인 버튼
    {
        string message = string.Empty;

        ResetUI(imageID, imagePW);

        if (IsFieldDateEmpty(imageID, inputFieldID.text, "아이디") || IsFieldDateEmpty(imagePW, inputFieldPW.text, "비밀번호"))
        {
            message = "아이디/비밀번호를 입력해주세요.";
            show_result(false, message);
            return;
        }

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
                show_result(true);
                BackendGameData_JGD.Instance.GameDataGet();
                BackendChart_JGD.Instance.ChartDataGet();

                if (BackendGameData_JGD.userData == null)
                {
                    BackendGameData_JGD.Instance.GameDataInsert();

                }
                BackendGameData_JGD.Instance.GameDataUpdate();
                Debug.Log($"exp : {BackendGameData_JGD.userData.housing_Info.exp}");
                Debug.Log($"item_ID : {BackendGameData_JGD.userData.housing_Info.objectInfos[0].item_ID}");
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
                        message = callback.GetMessage().Contains("customId") ? "존재하지 않는 아이디입니다." : "잘못된 비밀번호 입니다.";
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

                show_result(false, message);

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
    public void Login_Scene() //로그인 화면
    {
        Login.SetActive(true);
        Signup.SetActive(false);
    }
    public void Signup_Scene() //회원가입 화면
    {
        Login.SetActive(false);
        Signup.SetActive(true);
    }

    public void Change_PWtype()
    {
        if (inputFieldPW.contentType == TMP_InputField.ContentType.Password)
        {
            inputFieldPW.contentType = TMP_InputField.ContentType.Standard;
            PWbtn_img.sprite = standard;
        }

        else
        {
            inputFieldPW.contentType = TMP_InputField.ContentType.Password;
            PWbtn_img.sprite = passward;
        }

        inputFieldPW.textComponent.SetAllDirty();
    }

    public void show_result(bool success, string message = null) //로그인 결과
    {
        GameObject obj = success ? Done : DoneX;

        Done.SetActive(success);
        DoneX.SetActive(!success);

        if (success)
        {
            obj.transform.GetChild(0).GetComponent<TMP_Text>().text = "로그인 완료";
            obj.transform.GetChild(1).gameObject.SetActive(false);
            obj.transform.GetChild(2).gameObject.SetActive(false);
        }

        else
        {
            obj.transform.GetChild(0).GetComponent<TMP_Text>().text = "로그인 실패";
            obj.transform.GetChild(1).gameObject.SetActive(true);
            obj.transform.GetChild(1).GetComponent<TMP_Text>().text = message;
        }
    }
}