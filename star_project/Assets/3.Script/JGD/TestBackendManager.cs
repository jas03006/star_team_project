using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

using BackEnd;

public class TestBackendManager : MonoBehaviour
{
    private void Start()
    {
        var bro = Backend.Initialize(true);    //뒤끝 초기화

        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공 : " + bro);  //성공일 경우
        }
        else
        {
            Debug.LogError("초기화 실패 ㅠㅜ" + bro);    //실패일경우
        }
        //Test();
    }

    async void Test()
    {
        await Task.Run(() =>
        {
            //추후 테스트 추가
            TestBackend_Login_JGD.Instance.CustomLogin("tlqkf", "1234"); // [추가] 뒤끝 로그인
            //TestBackend_Login_JGD.Instance.UpdateNickname("이름~~~"); //[추가] 닉네임 변경

            //게임 정보 기능 구현 로직
            //BackendGameData_JGD.Instance.GameDataInsert();
            //친구 기능 로직
            BackendFriend_JDG.Instance.GetReceivedRequestFriend();
            BackendFriend_JDG.Instance.ApplyFriend(0);



            Debug.Log("테스트를 종료합니다.");
        });
    }
    
}