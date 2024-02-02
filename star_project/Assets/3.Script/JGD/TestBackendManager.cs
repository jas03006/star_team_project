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
            TestBackend_Login_JGD.Instance.CustomLogin("user0", "1234"); // [�߰�] �ڳ� �α���
            //TestBackend_Login_JGD.Instance.UpdateNickname("�̸�~~~"); //[�߰�] �г��� ����

            //���� ���� ��� ���� ����
            BackendGameData_JGD.Instance.GameDataInsert();

            Debug.Log("�׽�Ʈ�� �����մϴ�.");
        });
    }





}