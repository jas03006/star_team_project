using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item_game
{
    override public void Init() //��Ʈ���� �ҷ��� �� ����
    {
        percent = data.percent;
    }

    public override void UseItem()
    {
        //¯�Ե� �������ض� ���� ����ʹ� �̰Ը³� ����
    }
}