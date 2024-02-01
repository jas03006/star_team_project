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
        // 회원가입 구현로직
        Debug.Log("회원가입을 요청합니다.");

        var bro = Backend.BMember.CustomSignUp(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("회원가입에 성공했습니다. : "+bro);
        }
        else
        {
            Debug.LogError("회원가입에 실패했습니다. : " + bro);
        }


    }
    public void CustomLogin(string id, string pw)
    {
        //로그인 구현로직
        var bro = Backend.BMember.CustomLogin(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("로그인 성공 :"  +bro);
        }
        else
        {
            Debug.LogError("로그인에 실패 : " + bro);
        }
    }
    public void UpdateNickname(string nickname)
    {
        //닉네임 변경 구현로직
        Debug.Log("닉네임 변경을 요청합니다.");

        var bro = Backend.BMember.UpdateNickname(nickname);

        if (bro.IsSuccess())
        {
            Debug.Log("닉네임 변경에 성공했습니다.");
        }
        else
        {
            Debug.LogError("닉네임 변겨엥 실패했습니다. : " + bro);
        }
    }

}