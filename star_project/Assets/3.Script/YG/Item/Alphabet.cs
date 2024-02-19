using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alphabat : Item_game
{
    private char alphabet;
    override public void Init() //차트에서 불러온 값 세팅
    {
        alphabet = data.alphabet;
    }

    public override void UseItem()
    {
        //피씨방인데 42분남았다 돈이 살살 녹는다ㅋ
    }
}