using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum pet_ID
{
    Yellow,
    Red,
    Blue,
    Purple,
    Green
}

public class PetInfo_YG : MonoBehaviour //각 펫 레벨 저장을 위한 클래스
{
    public List<PetObj> pets = new List<PetObj>();

    public PetInfo_YG()
    {
    }

    public PetInfo_YG(JsonData json)
    {
        if (json.IsObject)
        {
            foreach (JsonData pet in json["pets"])
            {
                pets.Add(new PetObj(pet));
            }
        }
    }

    public void Add_object(PetObj obj)
    {
        pets.Add(obj);
    }
}

public class PetObj
{
    public pet_ID pet_id;
    public int level =1;

    public PetObj(JsonData json)
    {
        if (json.IsObject)
        {
            level = int.Parse(json["level"].ToString());
            pet_id = (pet_ID)int.Parse(json["pet_ID"].ToString());
        }
    }

    public PetObj(pet_ID pet_ID)
    {
        pet_id = pet_ID;
        level = 1;
    }
}

