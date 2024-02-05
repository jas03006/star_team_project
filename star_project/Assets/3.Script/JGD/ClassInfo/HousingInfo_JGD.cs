using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousingInfo_JGD
{
    public int Housing = 0;

    public HousingInfo_JGD(JsonData json)
    {
        Housing = Int32.Parse(json["AchievementsInfo_JGD"].ToString());
    }
}
