using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item_game
{
    double Hp;
    double MaxHp;
    Item_game item_;

    private void Start()
    {
        item_ = GetComponent<Item_game>();
    }

    public void UseItem()
    {
        Hp = game.GetComponent<Player_Controll_JGD>().currentHp;
        MaxHp = game.GetComponent<Player_Controll_JGD>().MaxHp;

        Hp += MaxHp * item_.percent;
        if (Hp >= MaxHp)
        {
            Hp = MaxHp;
        }
        game.GetComponent<Player_Controll_JGD>().currentHp = Hp;
    }


    //public override void UseItem()
    //{
    //    //¯�Ե� �������ض� ���� ����ʹ� �̰Ը³� ����
    //}


}