using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class TestLoginout : MonoBehaviour
{
    [SerializeField] public TMP_InputField InputID;
    [SerializeField] public TMP_InputField InputPW;
    [SerializeField] private Button registerButton;




    public void Signin()
    {
        string userID = InputID.text;
        string userPW = InputPW.text;

        TestBackend_Login_JGD.Instance.CustomSignUp(userID, userPW);
    }
    public void Login()
    {
        string userID = InputID.text;
        string userPW = InputPW.text;

        TestBackend_Login_JGD.Instance.CustomLogin(userID, userPW);
    }


}