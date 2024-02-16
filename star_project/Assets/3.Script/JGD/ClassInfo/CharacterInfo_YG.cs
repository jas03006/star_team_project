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
    public List<CharacterObj> pet_list = new List<CharacterObj>();
    public Dictionary<Character_ID, int> pet_dic = new Dictionary<Character_ID, int>(); //레벨추출을 위한 딕셔너리

    public CharacterInfo_YG()
    {

    }

    public CharacterInfo_YG(JsonData json)
    {
        if (json.IsObject)
        {
            foreach (JsonData pet in json["characters"])
            {
                CharacterObj pet_obj = new CharacterObj(pet);
                pet_list.Add(pet_obj);
                pet_dic.Add(pet_obj.pet_id, pet_obj.level);
            }
        }
    }

    public void Add_object(CharacterObj obj)
    {
        pet_list.Add(obj);
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
            pet_id = (Character_ID)int.Parse(json["pet_ID"].ToString());
        }
    }

    public CharacterObj(Character_ID pet_ID)
    {
        pet_id = pet_ID;
        level = 1;
    }
}

