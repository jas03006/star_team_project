using BackEnd;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sign_Up_JGD : LoginBase_JGD
{
    [SerializeField] GameObject Login;

    [SerializeField] private TMP_InputField inputFieldID;
    [SerializeField] private TMP_InputField inputFieldPW;
    [SerializeField] private TMP_InputField inputFieldPW_check;

    [SerializeField] Image PWbtn_img;
    [SerializeField] Image PWcheckbtn_img;
    [SerializeField] Sprite standard;
    [SerializeField] Sprite password;

    [SerializeField] private TMP_Text Title_text;
    [SerializeField] private TMP_Text reason_text;

    [SerializeField] private Button btnRegisterAccount;

    [SerializeField] private Login_JGD login;

    [SerializeField] private GameObject Done;
    [SerializeField] private GameObject DoneX;
    //private Coroutine now_result_co = null;

    public void OnclickSignUp()
    {
        Backend.BMember.CustomSignUp(inputFieldID.text, inputFieldPW.text, callback =>
        {
            btnRegisterAccount.interactable = true;

            if (callback.IsSuccess())
            {
                Debug.Log("���̵� ���� ����");

                Login.SetActive(true);
                this.gameObject.SetActive(false);

            }
            else
            {
                Debug.Log("���̵� ���� ����");
            }
        });
    }



    public void testSignUp()
    {
        if (inputFieldPW.text != inputFieldPW_check.text)
        {
            Debug.Log("PW != PWcheck");
            reason_text.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            show_result(false) ;
            return;
        }
        //if (now_result_co != null)
        //{
        //    StopCoroutine(now_result_co);
        //}
        if (TestBackend_Login_JGD.Instance.CustomSignUp(inputFieldID.text, inputFieldPW.text))
        {
            login.Login_Scene();
            show_result(true);
        }
        else
        {
            show_result(false);
        }
    }
    public void show_result(bool success)
    {
        GameObject obj = success ? Done : DoneX;

        Done.SetActive(success);
        DoneX.SetActive(!success);

        if (success)
        {
            obj.transform.GetChild(0).GetComponent<TMP_Text>().text = "ȸ������ �Ϸ�";
            obj.transform.GetChild(1).gameObject.SetActive(false);
        }

        else
        {
            obj.transform.GetChild(0).GetComponent<TMP_Text>().text = "ȸ������ ����";
            obj.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    //public void show_result(bool success)
    //{

    //    Done.SetActive(true);

    //    if (success)
    //    {
    //        Title_text.text = "ȸ������ �Ϸ�";
    //        reason_text.enabled = false;
    //    }
    //    else
    //    {
    //        Title_text.text = "ȸ������ ����";
    //        reason_text.enabled = true;
    //    }
    //}

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
            PWbtn_img.sprite = password;
        }
        inputFieldPW.textComponent.SetAllDirty();
    }

    public void Change_PWchecktype()
    {
        if (inputFieldPW_check.contentType == TMP_InputField.ContentType.Password)
        {
            inputFieldPW_check.contentType = TMP_InputField.ContentType.Standard;
            PWcheckbtn_img.sprite = standard;
        }
        else
        {
            inputFieldPW_check.contentType = TMP_InputField.ContentType.Password;
            PWcheckbtn_img.sprite = password;
        }
        inputFieldPW_check.textComponent.SetAllDirty();
    }
}