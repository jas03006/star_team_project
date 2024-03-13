using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

using BackEnd;

public class TestBackendManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        BackendSetUp();
    }
    private void Start()
    {
        //Test();
    }
    private void Update()
    {
        if (Backend.IsInitialized)
        {
            Backend.AsyncPoll();
        }
    }

    async void Test()
    {
        await Task.Run(() =>
        {
            //���� �׽�Ʈ �߰�
            TestBackend_Login_JGD.Instance.CustomLogin("tlqkf", "1234"); // [�߰�] �ڳ� �α���
            //TestBackend_Login_JGD.Instance.UpdateNickname("�̸�~~~"); //[�߰�] �г��� ����

            //���� ���� ��� ���� ����
            BackendGameData_JGD.Instance.GameDataInsert();
            //ģ�� ��� ����
            //BackendChart_JGD.Instance.ChartGet("107516");


            Debug.Log("�׽�Ʈ�� �����մϴ�.");
        });
    }
    private void BackendSetUp()
    {
        var bro = Backend.Initialize(true);    //�ڳ� �ʱ�ȭ

        if (bro.IsSuccess())
        {
            Debug.Log("�ʱ�ȭ ���� : " + bro);  //������ ���
            if (BackendGameData_JGD.Instance==null) { 
            
            }
        }
        else
        {
            Debug.LogError("�ʱ�ȭ ���� �Ф�" + bro);    //�����ϰ��
        }
    }
}