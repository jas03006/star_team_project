using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Item_game : MonoBehaviour //인게임에서 불러와서 사용할 스크립트
{
    public item_ID id;
    public int itemid_;
    protected Item data;
    private string item_name;
    private Sprite sprite;

    public float duration;
    public double percent;
    public int Num;
    protected GameObject game;
    public void Init() //차트에서 불러온 값 세팅
    {
        id = (item_ID)itemid_;
        data = BackendChart_JGD.chartData.item_list[(int)id];
        percent = data.percent;
        duration = data.duration;
        Num = data.num;
        //id = data.id;
        item_name = data.item_name;
        Debug.Log(data);
        //sprite = SpriteManager.instance.Num2Sprite(data.sprite);
    }

    //public void UseItem()
    //{
    //    //아이템 사용
    //}
    private void Awake()
    {
        //Init();
        game = GameObject.FindGameObjectWithTag("Player");
    }
}
