using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsInfo_JGD
{
    public int Achievements = 0;

    public AchievementsInfo_JGD(JsonData json)
    {
        Achievements = Int32.Parse(json["AchievementsInfo_JGD"].ToString());
    }
}
