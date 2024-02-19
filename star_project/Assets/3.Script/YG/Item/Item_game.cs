using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 abstract public class Item_game : MonoBehaviour //�ΰ��ӿ��� �ҷ��ͼ� ����� ��ũ��Ʈ
{
    public item_ID id;
    protected Item data;
    private string item_name;
    private Sprite sprite;

    public double duration;
    public double percent;

    virtual public void Init() //��Ʈ���� �ҷ��� �� ����
    {
        data = BackendChart_JGD.chartData.item_list[(int)id];
        id = data.id;
        item_name = data.item_name;
        sprite = SpriteManager.instance.Num2Sprite(data.sprite);
    } 

    abstract public void UseItem(); //������ ���

    private void Start()
    {
        Init();
    }
}
