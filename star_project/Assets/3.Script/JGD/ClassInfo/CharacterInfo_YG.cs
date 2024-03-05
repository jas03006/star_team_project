using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum Character_ID
{
    Yellow = 0,
    Red,
    Blue,
    Purple,
    Green
}

public class CharacterInfo_YG  //각 캐릭터 레벨 저장을 위한 클래스
{
    public List<CharacterObj> character_list = new List<CharacterObj>();
    public Dictionary<Character_ID, int> character_dic = new Dictionary<Character_ID, int>(); //레벨추출을 위한 딕셔너리

    public CharacterInfo_YG()
    {

    }

    public CharacterInfo_YG(JsonData json)
    {
        if (json.IsObject)
        {
            foreach (JsonData character in json["character_list"])
            {
                CharacterObj cha_obj = new CharacterObj(character);
                character_list.Add(cha_obj);
                character_dic.Add(cha_obj.pet_id, cha_obj.level);
            }
        }
    }

    public void Add_object(CharacterObj obj)
    {
        character_list.Add(obj);
        character_dic.Add(obj.pet_id, obj.level);
    }

    public void Change_dic(int index, int level)
    {
        character_dic[character_list[index].pet_id] = level;
    }

    public void Characterinfo_update()
    {
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

public class CharacterObj
{
    public Character_ID pet_id;
    public int level = 0;
    //public Dictionary<pet_ID, int> dic = new Dictionary<pet_ID, int>();

    public CharacterObj(JsonData json)
    {
        if (json.IsObject)
        {
            level = int.Parse(json["level"].ToString());
            pet_id = (Character_ID)int.Parse(json["pet_id"].ToString());
        }
    }

    public CharacterObj(Character_ID pet_ID,int lev)
    {
        pet_id = pet_ID;
        level = lev;
    }
}

