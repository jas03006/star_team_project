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

        Debug.Log("signin");
        string reason;
        TestBackend_Login_JGD.Instance.CustomSignUp(userID, userPW, out reason);   //회원가입

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
            //BackendGameData_JGD.Instance.LevelUp();   //레벨업인데 왜 여기있는지 모르겠음 ㄹㅇ

            BackendGameData_JGD.Instance.GameDataUpdate();

            Debug.Log("테스트를 종료합니다.");
        });
    }


    public void test_update() {
        BackendGameData_JGD.userData.house_inventory.Add(new House_Item_Info_JGD());
        BackendGameData_JGD.userData.house_inventory.Add(new House_Item_Info_JGD());
        BackendGameData_JGD.userData.house_inventory.Add(new House_Item_Info_JGD());
        user_DB_update();
        Debug.Log(BackendGameData_JGD.userData.house_inventory);
    }
    async void user_DB_update() {
        await Task.Run(() =>
        {
            BackendGameData_JGD.Instance.GameDataUpdate();
            BackendGameData_JGD.Instance.GameDataGet();
            
        });            
    }
    public void SendMakeFriend()
    {
        string userNickname = InputID.text;           //닉네임
        BackendFriend_JDG.Instance.SendFriendRequest(userNickname);
    
        Debug.Log($"{userNickname}");
    }
    public void ChangeNickname()
    {
        string userNickname = InputID.text;           //닉네임
        TestBackend_Login_JGD.Instance.UpdateNickname(userNickname);

        Debug.Log($"{userNickname}");
    }
    public void ShowMyindePower()
    {
        BackendFriend_JDG.Instance.GetReceivedRequestFriend();
    }
    public void MakeFriend()
    {
        BackendFriend_JDG.Instance.ApplyFriend(0);
    }

}