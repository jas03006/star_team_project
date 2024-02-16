using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public enum Obstacle_ID
{
    None = -1,
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
    K,
    L,
    M,
    N,
    O,
    P,
    Q,
    R,
    S,
    T,
    U,
    V,
    W,
    X,
    Y,
    Z,
    small_heart,
    big_heart,
    small_star,
    big_star,
    Shield,
    Megnet,
    SpeedUp,
    SizeUp,
    SizeDown,
    Random,


}


public class StageItemInfo_JGD : MonoBehaviour
{
    public Obstacle_ID obstacle_ID = Obstacle_ID.None;

    public int ObjectNum;
    public float Pos_X;
    public float Pos_Y;
    public GameObject _Objects;

    public int Rot;

    public List<GameObject> Objects = new List<GameObject>();
    string indd = 
        "1:20.5:6:180," +
        "2:4:1," +
        "2:10:2," +
        "1:15.5:0:45";

    private void Start()
    {
        ReadStage(indd);
        Debug.Log("-완-");
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

            //Debug.Log(list[i][0]);
            //Debug.Log(list[i][1]);
            //Debug.Log(list[i][2]);
        }

        for (int i = 0; i < list.Count; i++)
        {
            ObjectNum = int.Parse(list[i][0].Trim());
            Pos_X = float.Parse(list[i][1].Trim());
            Pos_Y = float.Parse(list[i][2].Trim());
            Rot = 0;
            Debug.Log("나 여기있어");
            if (list[i].Length <4)
            {
                Rot = 0;
            }
            else if (list[i].Length >= 4)
            {
                Debug.Log("나도 여기있어");
                Rot = int.Parse(list[i][3]);
            }

            Debug.Log($"{Objects[ObjectNum].ToString()}, {Pos_X}, {Pos_Y}");

            Instantiate(Objects[ObjectNum], new Vector2(Pos_X, Pos_Y), Quaternion.Euler(0, 0, Rot));
        }

    }
}
