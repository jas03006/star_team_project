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
}

public class CharacterObj
{
    public Character_ID pet_id;
    public int level =1;
    //public Dictionary<pet_ID, int> dic = new Dictionary<pet_ID, int>();

    public CharacterObj(JsonData json)
    {
        if (json.IsObject)
        {
            level = int.Parse(json["level"].ToString());
            pet_id = (Character_ID)int.Parse(json["pet_id"].ToString());
        }
    }

    public CharacterObj(Character_ID pet_ID)
    {
        pet_id = pet_ID;
        level = 1;
    }
}

