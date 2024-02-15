using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StageInfo_JGD
{
    public int StageID;
    public int Stage_score_Json = 0;

    public StageInfo_JGD(JsonData json)
    {
        Stage_score_Json = Int32.Parse(json["Stage_score_Json"].ToString());
    }

}

public class StageObjectInfo_JGD
{
    public List<StageObjectInfo_JGD> StageObject = new List<StageObjectInfo_JGD>();
    public Obstacle_ID obstacle_ = Obstacle_ID.none; 
    public int Pos_X;
    public int Pos_Y;

    public StageObjectInfo_JGD()
    {

    }

    public StageObjectInfo_JGD(Obstacle_ID obstacle_ID , int positionx, int positiony)
    {
        obstacle_ = obstacle_ID;
        Pos_X = positionx; 
        Pos_Y = positiony;
    }



}
