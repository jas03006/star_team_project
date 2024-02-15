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
    None,
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
//��Ʈ���ٰ� ������ ������ �ֱ�
//��Ʈ�Ŵ������� ������ ������ �ҷ�����
//��Ʈ�Ŵ������� ������ �����ؼ� ���� ����ֱ�
//������ ��ų ��� ������� ���ϱ�

