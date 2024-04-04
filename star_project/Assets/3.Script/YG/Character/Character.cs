using BackEnd;
using LitJson;
using UnityEngine;

/// <summary>
/// 캐릭터 정보를 저장하는 클래스.
/// 뒤끝DB에서 차트 데이터를 불러와 저장함.
/// 캐릭터 레벨업 및 플레이어 정보 업데이트도 이 클래스에서 진행함.
/// </summary>
public class Character
{
    public Character_ID character_ID; //고유 id
    public string character_name;//이름

    public int maxlevel;//최대 레벨
    public int curlevel//현재 레벨
    {
        get { return BackendGameData_JGD.userData.character_info.character_dic[character_ID]; }
    }
    public float duration; //지속 시간(그린벨라 제외 사용)
    public double percent; //회복 퍼센트(그린벨라 전용)

    public item_ID special_item;//지급 아이템 ex)게임 시작 시 자석 아이템 한 개를 지급한다.
    public item_ID unique_item;//고유 능력 아이템 ex)자석 아이템 지속 시간 0.5초 증가
    public item_ID unique_item2;//고유 능력 아이템2 (그린벨라 전용)

    public string basic; //기본 스킬
    public string special; //스페셜 스킬
    public string unique; // 유니크 스킬

    public int sprite; //spritemanager에서 sprite가져올때 사용

    public Character(JsonData gameData) //차트데이터에서 정보 받아옴
    {
        character_ID = (Character_ID)int.Parse(gameData["character_ID"].ToString());
        character_name = gameData["character_name"].ToString();

        maxlevel = int.Parse(gameData["maxlevel"].ToString());

        if (character_ID == Character_ID.Green)
        {
            percent = 5 + ((curlevel - 1) * 0.5);
        }
        else
        {
            duration = 0.5f + ((curlevel - 1) * 0.1f) ;
        }
        special_item = (item_ID)int.Parse(gameData["special_item"].ToString());
        unique_item = (item_ID)int.Parse(gameData["unique_item"].ToString());

        basic = gameData["basic"].ToString();
        special = gameData["special"].ToString();
        unique = gameData["unique"].ToString();

        sprite = int.Parse(gameData["sprite"].ToString());
    }

    public void State_update() //레벨업 시 효과 업데이트
    {
        if (character_ID == Character_ID.Green)
        {
            percent += 0.5;
            Debug.Log(percent);
        }

        else
        {
            duration += 0.1f;
            Debug.Log(duration);
        }
    }

    public bool CanLevelup(int gold, int ark, out int goldRequired, out int arkRequired) //레벨업 가능한지 체크
    {
        Character_amount chartdata = BackendChart_JGD.chartData.Characteramount_list[curlevel - 1];
        goldRequired = chartdata.gold;
        arkRequired = chartdata.ark;

        if (gold > goldRequired && ark > arkRequired)
            return true;
        else
            return false;
    }

    public void Levelup(int goldRequired, int arkRequired) //레벨업 진행
    {
        //돈 빼기
        MoneyManager.instance.Spend_Money(Money.gold, goldRequired);
        MoneyManager.instance.Spend_Money(Money.ark, arkRequired);

        //레벨 업데이트
        BackendGameData_JGD.userData.character_info.character_dic[character_ID]++;
        Debug.Log("레벨업");
        QuestManager.instance.Check_mission(Criterion_type.character_levelup);
        Characterinfo_Data_update();
        State_update();
    }

    public void Characterinfo_Data_update() //Characterinfo 데이터 업데이트
    {
        BackendGameData_JGD.userData.character_info.character_dic[character_ID] = curlevel;
        BackendGameData_JGD.userData.character_info.character_list[(int)character_ID].level = curlevel;

        //데이터에 넣기
        Param param = new Param();
        param.Add("character_info", BackendGameData_JGD.userData.character_info);

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

    public void character_Data_update(int num)//Character 데이터 업데이트
    {
        BackendGameData_JGD.userData.character = num;
        //데이터에 넣기
        Param param = new Param();
        param.Add("character", BackendGameData_JGD.userData.character);

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


