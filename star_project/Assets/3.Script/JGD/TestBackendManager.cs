using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

using BackEnd;

public class TestBackendManager : MonoBehaviour
{
    private void Start()
    {
        var bro = Backend.Initialize(true);    //�ڳ� �ʱ�ȭ

        if (bro.IsSuccess())
        {
            Debug.Log("�ʱ�ȭ ���� : " + bro);  //������ ���
        }
        else
        {
            Debug.LogError("�ʱ�ȭ ���� �Ф�" + bro);    //�����ϰ��
        }
        //Test();
    }

    async void Test()
    {
        await Task.Run(() =>
        {
            //���� �׽�Ʈ �߰�
            TestBackend_Login_JGD.Instance.CustomLogin("tlqkf", "1234"); // [�߰�] �ڳ� �α���
            //TestBackend_Login_JGD.Instance.UpdateNickname("�̸�~~~"); //[�߰�] �г��� ����

            //���� ���� ��� ���� ����
            //BackendGameData_JGD.Instance.GameDataInsert();
            //ģ�� ��� ����
            BackendFriend_JDG.Instance.GetReceivedRequestFriend();
            BackendFriend_JDG.Instance.ApplyFriend(0);



            Debug.Log("�׽�Ʈ�� �����մϴ�.");
        });
    }
    
}