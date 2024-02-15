using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Obstacle_ID
{
    none = -1,
    StartPositopn = 0,
    NormalWall,
    RedWall,
    Star,
    BigStar,
    Heart,
    BigHeart,
    Alphabet

}

public class StageItemInfo_JGD : MonoBehaviour
{
    public Obstacle_ID obstacle_ID = Obstacle_ID.none;

    public int ObjectNum;
    public int Pos_X;
    public int Pos_Y;
    public GameObject Object;


    public List<GameObject> Objects = new List<GameObject>();
    string indd = "1:2:3,3:4:1,2:4:2,1:1:1";

    private void Start()
    {

        Debug.Log("-��-");
    }

    private void ReadStage(string Stagenum)
    {
        List<string[]> list = new List<string[]>();

        //string indd = "1:2:3,3:4:1,2:4:2,1:1:1";

        Stagenum = indd;

        string[] cutObj = Stagenum.Split(',');

        for (int i = 0; i < cutObj.Length; i++)
        {
            list.Add(cutObj[i].Split(':'));

            Debug.Log(list[i][0]);
            Debug.Log(list[i][1]);
            Debug.Log(list[i][2]);
        }

        for (int i = 0; i < list.Count; i++)
        {
            ObjectNum = int.Parse(list[i][0]);
            Pos_X = int.Parse(list[i][1]);
            Pos_Y = int.Parse(list[i][2]);

            Object.transform.position = new Vector2(Pos_X,Pos_Y);


            Instantiate(Objects[ObjectNum], Object.transform);
        }

    }
}
