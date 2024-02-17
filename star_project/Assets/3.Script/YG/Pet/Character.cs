using BackEnd;
using LitJson;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Character_ID character_ID;
    public string character_name;

    public int maxlevel;
    public int curlevel;//차트아니고 게임데이터에서 불러옴

    public int duration; //지속 시간 
    public int give_time; //아이템 지급 주기

    public item_ID special_item;//지급 아이템 ex)게임 시작 시 자석 아이템 한 개를 지급한다.
    public item_ID unique_item;//고유 능력 아이템 ex)자석 아이템 지속 시간 0.5초 증가
    public item_ID unique_item2;//고유 능력 아이템2 (그린벨라 전용)

    public Sprite sprite;

    public Character(JsonData gameData)
    {
        character_ID = (Character_ID)int.Parse(gameData["character_ID"].ToString());
        character_name = gameData["character_name"].ToString();

        maxlevel = int.Parse(gameData["maxlevel"].ToString());
        curlevel = BackendGameData_JGD.userData.character_info.character_dic[character_ID];

        duration = int.Parse(gameData["duration"].ToString());
        give_time = int.Parse(gameData["give_time"].ToString());

        special_item = (item_ID)int.Parse(gameData["special_item"].ToString());
        unique_item = (item_ID)int.Parse(gameData["unique_item"].ToString());

        if (character_ID == Character_ID.Green)
        {
            unique_item2 = (item_ID)int.Parse(gameData["unique_item2"].ToString());
        }
        //sprite = DataBaseManager.Instance.Num2Sprite(int.Parse(gameData["sprite"].ToString()));
    }

    public IEnumerator Special_co() //특수능력 - give_time초마다 아이템 지급
    {
        while (true)
        {
            Debug.Log("대충 인벤토리 접근해서 아이템 지급");
            yield return new WaitForSeconds(give_time);
        }
    }

    public void UniqueSkill() //고유능력 - 아이템 지속시간 증가
    {

    }

    public void Level_up()
    {
        curlevel++;
        Level_update();
    }

    public void Level_update()
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


