using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

 public class Item_game : MonoBehaviour //�ΰ��ӿ��� �ҷ��ͼ� ����� ��ũ��Ʈ
{
    public item_ID id;
    public int itemid_;
    public int Allstar;
    protected Item data;
    private string item_name;
    private Sprite sprite;
    private SpriteRenderer spriterender;

    public float duration;
    public double percent;
    public double Num;
    protected GameObject game;
    public void Init() //��Ʈ���� �ҷ��� �� ����
    {
        id = (item_ID)itemid_;
        data = BackendChart_JGD.chartData.item_list[(int)id];
        percent = data.percent;
        duration = data.duration;
        Num = data.num;
        //id = data.id;
        item_name = data.item_name;
        Debug.Log(data.sprite);
        sprite = SpriteManager.instance.Num2Sprite(data.sprite);
        SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            spriteRenderer.sprite = sprite;
        }
        else
        {
            spriteRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            spriteRenderer.sprite = sprite;
        }
    }

    //public void UseItem()
    //{
    //    //������ ���
    //}
    private void Awake()
    {
        //Init();
        game = GameObject.FindGameObjectWithTag("Player");
    }
}
