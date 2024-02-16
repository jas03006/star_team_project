using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item
{
    [field: SerializeField]
    public item_ID id;

    [field: SerializeField]
    public string item_name;

    [field: SerializeField]
    public Sprite sprite;
}

public enum item_ID
{
    None =-1,
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
    K,
    L,
    M, 
    N,
    O, 
    P,
    Q,
    R,
    S, 
    T, 
    U, 
    V, 
    W, 
    X, 
    Y, 
    Z,
    small_heart,
    big_heart,
    small_star,
    big_star,
    Shield,
    Megnet,
    SpeedUp,
    SizeUp,
    SizeDown,
    Random
}

//0215 todo
//��Ʈ�Ŵ������� ������ ������ �ҷ�����
//��Ʈ�Ŵ������� ������ �����ؼ� ���� ����ֱ�
//������ ��ų ��� ������� ���ϱ�

