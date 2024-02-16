using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item
{
    [field: SerializeField]
    public item_ID id = item_ID.Alphabet;

    [field: SerializeField]
    public string item_name;

    [field: SerializeField]
    public Sprite sprite;
}

public enum item_ID
{
    None =-1,
    Alphabet,
    Heart,
    Star,
    Shield,
    Megnet,
    SpeedUp,
    SizeUp,
    SizeDown,
    Random
}

//0215 todo
//차트에다가 아이템 데이터 넣기 - 완료
//차트매니저에서 아이템 데이터 불러오기
//차트매니저에서 아이템 생성해서 정보 들고있기
//아이템 스킬 어디서 사용할지 정하기

