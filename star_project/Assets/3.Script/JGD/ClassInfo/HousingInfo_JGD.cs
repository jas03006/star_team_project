using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousingInfo_JGD
{
    public int Housing = 0;
    public string name = "as";
    public bool flag = false;
    public Vector2 position;
    public int direction = 0; //0,1,2,3 µ¿¼­³²ºÏ
    public HousingInfo_JGD()
    {
        position = new Vector2(0,UnityEngine.Random.Range(0,100));
    }
    public HousingInfo_JGD(JsonData json)
    {
        //Debug.Log(json.ToString());
        if (json.IsObject) {

            Housing = Int32.Parse(json["Housing"].ToString());

            position = new Vector2(Int32.Parse(json["position"][0].ToString()), Int32.Parse(json["position"][1].ToString()));        
        
        }
    }
}
