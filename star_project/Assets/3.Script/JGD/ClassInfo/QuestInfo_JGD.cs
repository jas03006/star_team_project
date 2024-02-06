using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfo_JGD
{
    public int Quest = 0;

    public QuestInfo_JGD(JsonData json)
    {
        Quest = Int32.Parse(json["AchievementsInfo_JGD"].ToString());
    }
}
