using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using System.Threading.Tasks;
using BackEnd;


public class TestLoginout : MonoBehaviour
{
    [SerializeField] public TMP_InputField InputID;
    [SerializeField] public TMP_InputField InputPW;
    [SerializeField] private Button registerButton;




    public void Signin()
    {
        string userID = InputID.text;
        string userPW = InputPW.text;

        TestBackend_Login_JGD.Instance.CustomSignUp(userID, userPW);   //ȸ������

        BackendGameData_JGD.Instance.GameDataInsert();   //���� ���� ����
    }
    async void Login()
    {
        await Task.Run(() =>
        {
            string userID = InputID.text;
            string userPW = InputPW.text;

            TestBackend_Login_JGD.Instance.CustomLogin(userID, userPW);   //�α���

            BackendGameData_JGD.Instance.GameDataGet();   //���� ���� �ҷ�����
            if (BackendGameData_JGD.userData == null)
            {
                BackendGameData_JGD.Instance.GameDataInsert();
            }
            BackendGameData_JGD.Instance.LevelUp();   //�������ε� �� �����ִ��� �𸣰��� ����

            BackendGameData_JGD.Instance.GameDataUpdate();

            Debug.Log("�׽�Ʈ�� �����մϴ�.");
        });
    }


}