using BackEnd;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TestBackend_Login_JGD : MonoBehaviour
{
    private static TestBackend_Login_JGD instance = null;

    public static TestBackend_Login_JGD Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TestBackend_Login_JGD();
            }
            return instance;
        }
    }

    public bool CustomSignUp(string id, string pw, out string reason)
    {
        // ȸ������ ��������
        Debug.Log("ȸ�������� ��û�մϴ�.");

        var bro = Backend.BMember.CustomSignUp(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("ȸ�����Կ� �����߽��ϴ�. : "+bro);
           /* if (BackendGameData_JGD.Instance==null) { 
            
            }*/
            BackendGameData_JGD.Instance.GameDataInsert(nickname: id);
            reason = null;
            return true;
        }
        else
        {
            //Debug.LogError("ȸ�����Կ� �����߽��ϴ�. : " + bro);
            switch (int.Parse(bro.GetStatusCode()))
            {
                case 400:
                    reason = "���̵� Ȯ���� �� �����ϴ�.";
                    break;
                case 401:
                    reason = bro.GetMessage().Contains("customId") ? "�߸��� ���̵��Դϴ�." : "�߸��� ��й�ȣ �Դϴ�.";
                    break;
                case 403:
                    reason = bro.GetMessage().Contains("user") ? "���ܴ��� �����Դϴ�." : "���ܴ��� ����̽��Դϴ�.";
                    break;
                case 409:
                    reason = "�ߺ��� ���̵� �����մϴ�.";
                    break;
                case 410:
                    reason = "Ż�� �������� �����Դϴ�.";
                    break;
                default:
                    reason = bro.GetMessage();
                    break;
            }
            return false;
        }


    }
    public void CustomLogin(string id, string pw)
    {
        //�α��� ��������
        var bro = Backend.BMember.CustomLogin(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("�α��� ���� :"  +bro);
        }
        else
        {
            Debug.LogError("�α��ο� ���� : " + bro);

        }
    }
    public void UpdateNickname(string nickname)
    {
        //�г��� ���� ��������
        Debug.Log("�г��� ������ ��û�մϴ�.");

        var bro = Backend.BMember.UpdateNickname(nickname);

        if (bro.IsSuccess())
        {
            Debug.Log("�г��� ���濡 �����߽��ϴ�.");
        }
        else
        {
            Debug.LogError("�г��� ���濡 �����߽��ϴ�. : " + bro);
        }
    }
}