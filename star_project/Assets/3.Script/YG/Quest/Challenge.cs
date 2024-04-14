using BackEnd;
using LitJson;
using UnityEngine;


/// <summary>
/// 업적 정보를 저장하는 클래스.
/// 뒤끝DB에서 차트 데이터를 불러와 저장함.
/// 업적 보상 수령, 클리어 여부 체크 및 플레이어 정보 업데이트도 이 클래스에서 진행함.
/// </summary>
public class Challenge : Quest
{
    public Challenge_userdata userdata;

    //차트
    public challenge_cate challenge_cate; //업적 카테고리
    public Clear_type clear_type;//업적 종류
    public int id; //업적별 고유 id
    public int CP; //업적 완료 시 지급되는 CP의 액수

    public Challenge(JsonData jsonData, int index)
    {
        challenge_cate = (challenge_cate)int.Parse(jsonData["challenge_cate"].ToString());
        clear_type = (Clear_type)int.Parse(jsonData["clear_type"].ToString());
        id = int.Parse(jsonData["challenge_id"].ToString());
        goal = int.Parse(jsonData["clear_val"].ToString());
        CP = int.Parse(jsonData["CP"].ToString());

        userdata = BackendGameData_JGD.userData.challenge_Userdatas[index];
        userdata.clear_Type = clear_type;

        title = jsonData["title"].ToString();
        contents = jsonData["info"].ToString();
        sub_text = jsonData["sub_text"].ToString();
    }

    public void Get_reward() //업적 보상 수령
    {
        //완료O, 보상수령X상태가 아니면 return
        if (userdata.state != challenge_state.can_reward)
            return;

        //보상 지급
        BackendGameData_JGD.userData.CP += CP;
        userdata.state = challenge_state.complete;

        //DB저장
        Data_update();

    }

    public void Data_update()//Challenge 데이터 업데이트
    {
        Param param = new Param();
        param.Add("CP", BackendGameData_JGD.userData.CP);
        param.Add("challenge_Userdatas", BackendGameData_JGD.userData.challenge_Userdatas);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(BackendGameData_JGD.Instance.gameDataRowInDate))
        {
            Debug.Log("내 제일 최신 게임정보 데이터 수정을 요청");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }

        else
        {
            Debug.Log($"{BackendGameData_JGD.Instance.gameDataRowInDate}의 게임정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.UpdateV2("USER_DATA", BackendGameData_JGD.Instance.gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 수정에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임정보 데이터 수정에 실패했습니다. : " + bro);
        }
    }

    public bool Check_clear()//클리어 여부 확인
    {
        if (userdata.criterion >= goal && userdata.state == challenge_state.incomplete)
        {
            userdata.is_clear = true;
            userdata.state = challenge_state.can_reward;
            Data_update();
            return true;
        }
        return false;
    }
}

public class Challenge_userdata
{
    public Clear_type clear_Type;//업적 종류
    public bool is_clear;//클리어 여부
    public int CP; //챌린지 포인트
    public bool get_rewarded;//보상 수령 여부
    public int criterion //현재 수치
    {
        get
        {
            return BackendGameData_JGD.userData.quest_Info.challenge_dic[clear_Type];
        }

        set
        {
            criterion_ = value;

            if (is_clear)
            {
                return;
            }
        }
    }
    private int criterion_;
    public challenge_state state;

    public Challenge_userdata(JsonData jsonData)//기존 회원 - 데이터 불러오기
    {
        is_clear = bool.Parse(jsonData["is_clear"].ToString());
        CP = int.Parse(jsonData["CP"].ToString());
        get_rewarded = bool.Parse(jsonData["get_rewarded"].ToString());
        criterion = int.Parse(jsonData["criterion"].ToString());
        state = (challenge_state)int.Parse(jsonData["state"].ToString());
    }

    public Challenge_userdata()//신규 회원 - 데이터 생성
    {
        is_clear = false;
        CP = 0;
        get_rewarded = false;
        criterion = 0;
        state = challenge_state.incomplete;
    }

    public void Data_update()//challenge 데이터 업데이트
    {
        //데이터에 넣기
        Param param = new Param();
        param.Add("challenge_Userdatas", BackendGameData_JGD.userData.challenge_Userdatas);
        param.Add("Achievements_List", BackendGameData_JGD.userData.Achievements_List);


        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(BackendGameData_JGD.Instance.gameDataRowInDate))
        {
            Debug.Log("내 제일 최신 게임정보 데이터 수정을 요청");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }

        else
        {
            Debug.Log($"{BackendGameData_JGD.Instance.gameDataRowInDate}의 게임정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.UpdateV2("USER_DATA", BackendGameData_JGD.Instance.gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 수정에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임정보 데이터 수정에 실패했습니다. : " + bro);
        }
    }
}
public enum Clear_type //업적 종류
{
    none = -1,
    first_connection = 0,
    clear_tutorial = 1,
    buy = 2,
    attendance = 3,
    get_star = 4,
    catchingstar_clear = 5,
    make_word = 6,
    add_friend = 7,
    visit_friendplanet = 8,
    proxy_harvesting = 9,
    upgrade_starnest = 10
}
public enum challenge_cate //업적 카테고리
{
    none = -1,
    common,
    play,
    community
}
public enum challenge_state //업적 상태
{
    none = -1,
    incomplete,//완료X, 보상수령X
    can_reward,//완료O, 보상수령X
    complete//완료O, 보상수령O
}
