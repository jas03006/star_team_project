using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Item_game
{
    private int Score;
    private void Start()
    {
        Score = data.num;
    }
    override public void Init() //��Ʈ���� �ҷ��� �� ����
    {
        base.Init();
    }

    public override void UseItem()
    {
        //¯�Ե� �������ض� ���� ����ʹ� �̰Ը³� ����
    }

}