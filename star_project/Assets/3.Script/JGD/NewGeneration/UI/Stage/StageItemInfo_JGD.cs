using OpenCover.Framework.Model;
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
    NomalWall,
    BlueWall,
    GreenWall,
    PurpleWall,
    RedWall,
    YellowWall,
    Nomal_Rockwall,
    Blue_Rockwall,
    Green_Rockwall,
    Purple_Rockwall,
    Red_Rockwall,
    Yellow_Rockwall,
    BlackHole,
    Meteor,
    CheckBox,

}


public class StageItemInfo_JGD : MonoBehaviour
{
    public static StageItemInfo_JGD Instance;
    public Obstacle_ID obstacle_ID = Obstacle_ID.None;
    [SerializeField]public int Stage;
    public int ObjectNum;
    public float Pos_X;
    public float Pos_Y;
    public GameObject _Objects;
    public int Rot;
    public List<GameObject> Objects = new List<GameObject>();
    public static List<GameObject> dfddd = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Debug.Log("-완-");
    }
    
    public void ReadStage(string Stagenum)
    {
        List<string[]> list = new List<string[]>();


        string[] cutObj = Stagenum.Split(',');

        for (int i = 0; i < cutObj.Length; i++)
        {
            list.Add(cutObj[i].Split(':'));

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
