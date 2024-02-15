using JetBrains.Annotations;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    private Character select_character;

    private void Start()
    {
        
    }

    public void Select_btn(int id) //펫 선택 버튼에 달려있음
    {
        Character_ID pet_id = (Character_ID)id;

        switch (pet_id)
        {
            case Character_ID.Yellow:
                break;
            case Character_ID.Red:
                break;
            case Character_ID.Blue:
                break;
            case Character_ID.Purple:
                break;
            case Character_ID.Green:
                break;
            default:
                break;
        }
    }
}

public class Character
{
    public Character_ID character_ID;
    public string pet_name;

    public int maxlevel;
    public int curlevel;//csvX

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

    public IEnumerator UniqueSkill() //N초마다 아이템 지급
    {
        Debug.Log("대충 인벤토리 접근해서 아이템 지급");
        yield return new WaitForSeconds(give_time);
    }


}


