using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sign_Up_JGD : MonoBehaviour
{
    [SerializeField] GameObject Login;

    [SerializeField] private TMP_InputField inputFieldID;
    [SerializeField] private TMP_InputField inputFieldPW;

    [SerializeField] private Button btnRegisterAccount;

    private Login_JGD login;
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

}