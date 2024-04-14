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
    public void OnclickLoin() //�α��� ��ư
    {
        string message = string.Empty;

        ResetUI(imageID, imagePW);

        if (IsFieldDateEmpty(imageID, inputFieldID.text, "���̵�") || IsFieldDateEmpty(imagePW, inputFieldPW.text, "��й�ȣ"))
        {
            message = "���̵�/��й�ȣ�� �Է����ּ���.";
            show_result(false, message);
            return;
        }

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
                //�α��� ����(���н� �ٽ÷α����ϱ� ���� �α��� ��ư ��ȣ�ۿ� Ȱ��ȭ
                btnLogin.interactable = true;
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 401:
                        message = callback.GetMessage().Contains("customId") ? "�������� �ʴ� ���̵��Դϴ�." : "�߸��� ��й�ȣ �Դϴ�.";
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

                show_result(false, message);

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
    public void Login_Scene() //�α��� ȭ��
    {
        Login.SetActive(true);
        Signup.SetActive(false);
    }
    public void Signup_Scene() //ȸ������ ȭ��
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

    public void show_result(bool success, string message = null) //�α��� ���
    {
        GameObject obj = success ? Done : DoneX;

        Done.SetActive(success);
        DoneX.SetActive(!success);

        if (success)
        {
            obj.transform.GetChild(0).GetComponent<TMP_Text>().text = "�α��� �Ϸ�";
            obj.transform.GetChild(1).gameObject.SetActive(false);
            obj.transform.GetChild(2).gameObject.SetActive(false);
        }

        else
        {
            obj.transform.GetChild(0).GetComponent<TMP_Text>().text = "�α��� ����";
            obj.transform.GetChild(1).gameObject.SetActive(true);
            obj.transform.GetChild(1).GetComponent<TMP_Text>().text = message;
        }
    }
}