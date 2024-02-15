using System.Collections;
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

    SpriteDataBaseSO spriteDB;
    ItemDataBaseSO itemDB;

    /*
         public Sprite Num2Sprite(int sprite_num)
    {

    }
     */

}
