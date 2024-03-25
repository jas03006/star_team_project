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
        TestBackend_Login_JGD.Instance.CustomSignUp(userID, userPW, out reason);   //ȸ������

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
            //BackendGameData_JGD.Instance.LevelUp();   //�������ε� �� �����ִ��� �𸣰��� ����

            BackendGameData_JGD.Instance.GameDataUpdate();

            Debug.Log("�׽�Ʈ�� �����մϴ�.");
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
        string userNickname = InputID.text;           //�г���
        BackendFriend_JDG.Instance.SendFriendRequest(userNickname);
    
        Debug.Log($"{userNickname}");
    }
    public void ChangeNickname()
    {
        string userNickname = InputID.text;           //�г���
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