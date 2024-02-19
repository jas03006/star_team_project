using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item_game
{
    override public void Init() //차트에서 불러온 값 세팅
    {
        percent = data.percent;
    }

    public override void UseItem()
    {
        //짱규동 파이팅해라 집에 가고싶다 이게맞냐 ㅅㅂ
    }
}