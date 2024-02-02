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

        TestBackend_Login_JGD.Instance.CustomSignUp(userID, userPW);   //회원가입

        BackendGameData_JGD.Instance.GameDataInsert();   //유저 정보 삽입
    }
    async void Login()
    {
        await Task.Run(() =>
        {
            string userID = InputID.text;
            string userPW = InputPW.text;

            TestBackend_Login_JGD.Instance.CustomLogin(userID, userPW);   //로그인

            BackendGameData_JGD.Instance.GameDataGet();   //유저 정보 불러오기
            if (BackendGameData_JGD.userData == null)
            {
                BackendGameData_JGD.Instance.GameDataInsert();
            }
            BackendGameData_JGD.Instance.LevelUp();   //레벨업인데 왜 여기있는지 모르겠음 ㄹㅇ

            BackendGameData_JGD.Instance.GameDataUpdate();

            Debug.Log("테스트를 종료합니다.");
        });
    }


}