using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 abstract public class Item_game : MonoBehaviour //인게임에서 불러와서 사용할 스크립트
{
    public item_ID id;
    protected Item data;
    private string item_name;
    private Sprite sprite;

    public double duration;
    public double percent;

    virtual public void Init() //차트에서 불러온 값 세팅
    {
        data = BackendChart_JGD.chartData.item_list[(int)id];
        id = data.id;
        item_name = data.item_name;
        sprite = SpriteManager.instance.Num2Sprite(data.sprite);
    } 

    abstract public void UseItem(); //아이템 사용

    private void Start()
    {
        Init();
    }
}
