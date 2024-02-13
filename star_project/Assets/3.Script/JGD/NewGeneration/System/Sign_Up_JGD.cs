using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;

public class Sign_Up_JGD : LoginBase_JGD
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
        TestBackend_Login_JGD.Instance.CustomSignUp(inputFieldID.text, inputFieldPW.text);
    }
}