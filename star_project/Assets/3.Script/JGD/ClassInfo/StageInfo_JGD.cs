using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo_JGD
{
    public int Stage_score_Json = 0;

    public StageInfo_JGD(JsonData json)
    {
        Stage_score_Json = Int32.Parse(json["Stage_score_Json"].ToString());
    }

}