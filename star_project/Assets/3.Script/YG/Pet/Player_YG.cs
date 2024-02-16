using LitJson;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player_YG : MonoBehaviour
{
    private Character character;

    //������ ��ȣ�ۿ� ����
    public int hp;
    public int star_num;

    public void Select_btn(int id) //�� ���� ��ư
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
        //switch �ھƼ� �����ۻ��
    }

}

public class Character : MonoBehaviour
{
    public Character_ID character_ID;
    public string character_name;

    public int maxlevel;
    public int curlevel;//��Ʈ�ƴϰ� ���ӵ����Ϳ��� �ҷ���

    public int duration; //���� �ð� 
    public int give_time; //������ ���� �ֱ�

    public item_ID special_item;//���� ������ ex)���� ���� �� �ڼ� ������ �� ���� �����Ѵ�.
    public item_ID unique_item;//���� �ɷ� ������ ex)�ڼ� ������ ���� �ð� 0.5�� ����
    public item_ID unique_item2;//���� �ɷ� ������2 (�׸����� ����)

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


