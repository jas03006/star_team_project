using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alphabat : Item_game
{
    private char alphabet;
    override public void Init() //��Ʈ���� �ҷ��� �� ����
    {
        alphabet = data.alphabet;
    }

    public override void UseItem()
    {
        //�Ǿ����ε� 42�г��Ҵ� ���� ��� ��´٤�
    }
}