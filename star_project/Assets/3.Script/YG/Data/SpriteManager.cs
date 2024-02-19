using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager instance;

    public SpriteDataBaseSO spriteDB;
    private Dictionary<int, Sprite> spriteDictionary = new Dictionary<int, Sprite>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //시작할때 dic 생성
        foreach (var imageData in spriteDB.ImageData)
        {
            spriteDictionary.Add(imageData.id, imageData.sprite);
        }
        Debug.Log("생성끝!");
    }

    public Sprite Num2Sprite(int sprite_num) //번호에 맞는 스프라이트 찾는 메서드
    {
        return spriteDictionary[sprite_num];
    }

}
