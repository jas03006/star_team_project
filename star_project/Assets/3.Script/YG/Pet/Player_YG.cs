using LitJson;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player_YG : MonoBehaviour
{
    private Character character;

    //아이템 상호작용 변수
    public int hp;
    public int star_num;

    public void Select_btn(int id) //펫 선택 버튼
    {
        character = BackendChart_JGD.chartData.character_list[id];
    }

    public void GameStart()
    {
        if (character == null)
        {
            character = BackendChart_JGD.chartData.character_list[0];
        }
        StartCoroutine(character.Special_co());
    }

    public void Use_item(item_ID id)
    {
        //switch 박아서 아이템사용
    }

}

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
        //curlevel = BackendGameData_JGD.userData.character_info.pet_dic[character_ID];

        duration = int.Parse(gameData["duration"].ToString());
        give_time = int.Parse(gameData["give_time"].ToString());

        special_item = (item_ID)int.Parse(gameData["special_item"].ToString());
        unique_item = (item_ID)int.Parse(gameData["unique_item"].ToString());
        unique_item2 = (item_ID)int.Parse(gameData["unique_item2"].ToString());

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
}


