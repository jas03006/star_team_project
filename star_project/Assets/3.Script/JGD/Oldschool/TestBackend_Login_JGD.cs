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
        // 회원가입 구현로직
        Debug.Log("회원가입을 요청합니다.");

        var bro = Backend.BMember.CustomSignUp(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("회원가입에 성공했습니다. : "+bro);
           /* if (BackendGameData_JGD.Instance==null) { 
            
            }*/
            BackendGameData_JGD.Instance.GameDataInsert(nickname: id);
            reason = null;
            return true;
        }
        else
        {
            //Debug.LogError("회원가입에 실패했습니다. : " + bro);
            switch (int.Parse(bro.GetStatusCode()))
            {
                case 400:
                    reason = "아이디를 확인할 수 없습니다.";
                    break;
                case 401:
                    reason = bro.GetMessage().Contains("customId") ? "잘못된 아이디입니다." : "잘못된 비밀번호 입니다.";
                    break;
                case 403:
                    reason = bro.GetMessage().Contains("user") ? "차단당한 유저입니다." : "차단당한 디바이스입니다.";
                    break;
                case 409:
                    reason = "중복된 아이디가 존재합니다.";
                    break;
                case 410:
                    reason = "탈퇴가 진행중인 유저입니다.";
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
            Debug.LogError("닉네임 변경에 실패했습니다. : " + bro);
        }
    }
}