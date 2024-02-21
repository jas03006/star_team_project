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

        //�����Ҷ� dic ����
        foreach (var imageData in spriteDB.ImageData)
        {
            spriteDictionary.Add(imageData.id, imageData.sprite);
        }
        Debug.Log("������!");
    }

    public Sprite Num2Sprite(int sprite_num) //��ȣ�� �´� ��������Ʈ ã�� �޼���
    {
        return spriteDictionary[sprite_num];
    }

    public Sprite Num2Sprite(housing_itemID id) //��ȣ�� �´� ��������Ʈ ã�� �޼���
    {
        return spriteDictionary[housingID2sprite_num(id)];
    }

    public int housingID2sprite_num(housing_itemID id) {
        //TODO: �Ͽ�¡ ������ ��������Ʈ �����ؾ���
        return 0;
    }

}
