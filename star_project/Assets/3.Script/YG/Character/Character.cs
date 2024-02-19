using BackEnd;
using LitJson;
using UnityEngine;

public class Character
{
    public Character_ID character_ID;
    public string character_name;

    public int maxlevel;
    public int curlevel;//차트아니고 게임데이터에서 불러옴

    public double duration; //지속 시간
    public double percent;

    public item_ID special_item;//지급 아이템 ex)게임 시작 시 자석 아이템 한 개를 지급한다.
    public item_ID unique_item;//고유 능력 아이템 ex)자석 아이템 지속 시간 0.5초 증가
    public item_ID unique_item2;//고유 능력 아이템2 (그린벨라 전용)

    public string basic;
    public string special;
    public string unique;

    public int sprite;

    public Character(JsonData gameData)
    {
        character_ID = (Character_ID)int.Parse(gameData["character_ID"].ToString());
        character_name = gameData["character_name"].ToString();

        maxlevel = int.Parse(gameData["maxlevel"].ToString());
        curlevel = BackendGameData_JGD.userData.character_info.character_dic[character_ID];

        if (character_ID == Character_ID.Green)
            percent = 5 + (curlevel * 0.5);
        else
            duration = 0.5 + (curlevel * 0.1);

        special_item = (item_ID)int.Parse(gameData["special_item"].ToString());
        unique_item = (item_ID)int.Parse(gameData["unique_item"].ToString());

        basic = gameData["basic"].ToString();
        special = gameData["special"].ToString();
        unique = gameData["unique"].ToString();

        sprite = int.Parse(gameData["sprite"].ToString());
    }
    public Character() //test
    {
        character_ID = Character_ID.Green;
        character_name = "옐로우 벨라" + $"{Random.Range(1, 100)}";

        maxlevel = 30;
        curlevel = 1;

        if (character_ID == Character_ID.Green)
            percent = 5;
        else
            duration = 0.5;

        special_item = item_ID.Shield;

        basic = "basic";
        special = "special";
        unique = "unique";

        sprite = 100;
    }

    public void State_update()
    {
        if (character_ID == Character_ID.Green)
        {
            percent += 0.5;
            Debug.Log(percent);
        }

        else
        {
            duration += 0.1;
            Debug.Log(duration);
        }
    }

    public bool CanLevelup(int gold, int ark) //레벨업 가능한지 체크
    {
        //11:08
        return true;
    }

    public void Levelup() //레벨업 진행
    {
        curlevel++;
        Data_update();
        State_update();
        
    }

    public void Data_update()
    {
        BackendGameData_JGD.userData.character_info.character_dic[character_ID] = curlevel;
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
}


