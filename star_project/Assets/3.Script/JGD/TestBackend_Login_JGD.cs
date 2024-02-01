using BackEnd;
using System.Collections;
using System.Collections.Generic;
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

    public void CustomSignUp(string id, string pw)
    {
        // ȸ������ ��������
        Debug.Log("ȸ�������� ��û�մϴ�.");

        var bro = Backend.BMember.CustomSignUp(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("ȸ�����Կ� �����߽��ϴ�. : "+bro);
        }
        else
        {
            Debug.LogError("ȸ�����Կ� �����߽��ϴ�. : " + bro);
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
            Debug.LogError("�г��� ���ܿ� �����߽��ϴ�. : " + bro);
        }
    }

}