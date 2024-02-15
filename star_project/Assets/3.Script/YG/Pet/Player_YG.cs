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

    public void Select_btn(int id) //�� ���� ��ư
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
    public int curlevel;//��Ʈ�ƴϰ� ���ӵ����Ϳ��� �ҷ���

    public int duration; //���� �ð� 
    public int give_time; //������ ���� �ֱ�

    public item_ID item;//���� ������ 
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
    public IEnumerator Special_co() //Ư���ɷ� - give_time�ʸ��� ������ ����
    {
        while (true)
        {
            Debug.Log("���� �κ��丮 �����ؼ� ������ ����");
            yield return new WaitForSeconds(give_time);
        }
    }

    public void UniqueSkill() //�����ɷ� - ������ ���ӽð� ����
    {
        
    }
}


