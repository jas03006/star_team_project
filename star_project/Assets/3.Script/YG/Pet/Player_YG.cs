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

    public void Select_btn(int id) //ĳ���� ���� ��ư
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


