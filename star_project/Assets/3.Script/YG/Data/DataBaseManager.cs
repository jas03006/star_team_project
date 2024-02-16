using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    private static DataBaseManager instance = null;
    public static DataBaseManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataBaseManager();
            }
            return instance;
        }
    }

    public SpriteDataBaseSO spriteDB;
    private Dictionary<int, Sprite> spriteDictionary = new Dictionary<int, Sprite>();

    public ItemDataBaseSO itemDB;

    private void Awake()
    {
        //시작할때 dic 생성
        foreach (var imageData in spriteDB.ImageData)
        {
            spriteDictionary.Add(imageData.id, imageData.sprite);
        }
    }

    public Sprite Num2Sprite(int sprite_num) //번호에 맞는 스프라이트 찾는 메서드
    {
        return spriteDictionary[sprite_num];
    }

}
