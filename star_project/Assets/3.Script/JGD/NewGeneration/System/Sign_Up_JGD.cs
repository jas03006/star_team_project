using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;
using System.Collections;

public class Sign_Up_JGD : LoginBase_JGD
{
    [SerializeField] GameObject Login;

    [SerializeField] private TMP_InputField inputFieldID;
    [SerializeField] private TMP_InputField inputFieldPW;

    [SerializeField] private Button btnRegisterAccount;

    [SerializeField] private Login_JGD login;
    [SerializeField] private GameObject sing_up_success_UI;
    [SerializeField] private GameObject sing_up_fail_UI;
    private Coroutine now_result_co = null;
    public void OnclickSignUp()
    {
        Backend.BMember.CustomSignUp(inputFieldID.text, inputFieldPW.text, callback =>
        {
            btnRegisterAccount.interactable = true;

            if (callback.IsSuccess())
            {
                Debug.Log("아이디 생성 성공");

                Login.SetActive(true);
                this.gameObject.SetActive(false);

            }
            else
            {
                Debug.Log("아이디 생성 실패");
            }
        });
    }



    public void testSignUp()
    {
        if (now_result_co != null) {
            StopCoroutine(now_result_co);
        }
        if (TestBackend_Login_JGD.Instance.CustomSignUp(inputFieldID.text, inputFieldPW.text))
        {
            login.Login_Scene();
            now_result_co = StartCoroutine(show_result(true));
        }
        else { 
            now_result_co = StartCoroutine(show_result(false));
        }
    }

    public IEnumerator show_result(bool success) {
        GameObject ui_ob = null;
        if (success)
        {
            ui_ob = sing_up_success_UI;
        }
        else {
            ui_ob = sing_up_fail_UI;
        }
        ui_ob.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        ui_ob.SetActive(false);
    }
}