using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageItemInfo_JGD : MonoBehaviour
{
    private void Start()
    {
        List<string> list = new List<string>();

        string indd = "1:2:3,3:4:1,2:4:2,1:1:1";

        string[] cutObj = indd.Split(',');


        for (int i = 0; i < cutObj.Length; i++)
        {

//            list[i] = cutObj[i].Split(':');
//
//            Debug.Log(obj[i]);
        }


    }
}
