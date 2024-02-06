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

        if (IsFieldDateEmpty(imageID, inputFieldID.text, "���̵�")) return;
        if (IsFieldDateEmpty(imagePW, inputFieldPW.text, "��й�ȣ")) return;

        btnLogin.interactable = false;

        StartCoroutine(nameof(LoginProgress));

        ResponseToLogin(inputFieldID.text, inputFieldPW.text);
    }

    private void ResponseToLogin(string ID, string PW)
    {
        //������ �α��� ��û
        Backend.BMember.CustomLogin(ID, PW, callback =>
        {
            StopCoroutine(nameof(LoginProgress));
            if ( callback.IsSuccess())
            {
                //SetMessage($"{inputFieldID.text}�� ȯ���մϴ�");//�α��� ����
                Debug.Log("�α��� ����");
                SceneManager.LoadScene(nextScene.ToString());
            }
            else
            {
                //�α��� ����(���н� �ٽ÷α����ϱ� ���� �α��� ��ư ��ȣ�ۿ� Ȱ��ȭ
                btnLogin.interactable = true;

                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 401:
                        message = callback.GetMessage().Contains("customId") ? "�������� �ʴ� ���̵� �Դϴ�." : "�߸��� ��й�ȣ �Դϴ�.";
                        break;
                    case 403:
                        message = callback.GetMessage().Contains("user") ? "���ܴ��� �����Դϴ�." : "���ܴ��� ����̽��Դϴ�.";
                        break;
                    case 410:
                        message ="Ż�� �������� �����Դϴ�.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("��й�ȣ"))
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

            //SetMessage($"�α��� �� �Դϴ�...{time:F1})");

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