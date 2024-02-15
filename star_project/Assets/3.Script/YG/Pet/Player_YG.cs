using LitJson;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player_YG : MonoBehaviour
{
    private Character character;
    private int hp;

    public void Select_btn(int id) //펫 선택 버튼
    {
        character = BackendChart_JGD.chartData.pet_list[id];
    }

    public void GameStart()
    {
        if (character == null)
        {
            character = BackendChart_JGD.chartData.pet_list[0];
        }

        StartCoroutine(character.Special_co());
    }

}

public class Character : MonoBehaviour
{
    public Character_ID character_ID;
    public string pet_name;

    public int maxlevel;
    public int curlevel;//차트아니고 게임데이터에서 불러옴

    public int duration; //지속 시간 
    public int give_time; //아이템 지급 주기

    public item_ID item;//지급 아이템 
    public Sprite sprite;

    public Character(JsonData gameData)
    {
        character_ID = (Character_ID)int.Parse(gameData["character_ID"].ToString());
        pet_name = gameData["pet_name"].ToString();

        maxlevel = int.Parse(gameData["maxlevel"].ToString());
        curlevel = BackendGameData_JGD.userData.Pet_Info.pet_dic[character_ID];

        duration = int.Parse(gameData["duration"].ToString());
        give_time = int.Parse(gameData["give_time"].ToString());
        item = (item_ID)int.Parse(gameData["item"].ToString());
        sprite = DataBaseManager.Instance.Num2Sprite(int.Parse(gameData["sprite"].ToString()));
    }

    public Character(Character_ID pet_id)
    {
        character_ID = pet_id;
        pet_name = pet_id.ToString();
        curlevel = BackendGameData_JGD.userData.Pet_Info.pet_dic[pet_id];

        maxlevel = 30;
        item = item_ID.None;
        duration = 3;
        give_time = 3;
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


