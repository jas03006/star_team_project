using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
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
    Move_NomalWall,
    Move_BlueWall,
    Move_GreenWall,
    Move_PurpleWall,
    Move_RedWall,
    Move_YellowWall,
}


public class StageItemInfo_JGD : MonoBehaviour
{
    public static StageItemInfo_JGD Instance;
    public Obstacle_ID obstacle_ID = Obstacle_ID.None;
    [SerializeField]public int Stage;
    //장애물정보=============================================================================================
    public int ObjectNum;
    public float Pos_X;
    public float Pos_Y;
    public float Rot;
    private float Scale_X = 1;
    private float Scale_Y = 1;
    private int discrimination;
    private float distance = 0;
    public GameObject _Objects;
    public List<GameObject> Objects = new List<GameObject>();
    //========================================================================================================


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
            Rot = float.Parse(list[i][3].Trim());
            Scale_X = int.Parse(list[i][4].Trim());
            Scale_Y = int.Parse(list[i][5].Trim());


            if (ObjectNum == 50)
            {
                discrimination = int.Parse(list[i][6].Trim());
                distance = int.Parse(list[i][7].Trim());
            }
            else if (ObjectNum > 50)
            {
                discrimination = int.Parse(list[i][6].Trim());
                distance = 0;
            }
            else
            {
                discrimination = 0; 
                distance = 0;
            }


            GameObject gameObject = Instantiate(Objects[ObjectNum], new Vector2(Pos_X, Pos_Y), Quaternion.Euler(0, 0, Rot));
            gameObject.transform.localScale = new Vector3 (Scale_X, Scale_Y, 0);
            gameObject.GetComponent<ItemID_JGD>().ID = ObjectNum;
            gameObject.GetComponent<ItemID_JGD>().discrimination = discrimination;
            gameObject.GetComponent<ItemID_JGD>().distance = distance;
            if (gameObject.GetComponent<Item_game>() != null)
            {
                gameObject.GetComponent<Item_game>().itemid_ = ObjectNum;   // 이놈이 문제다
                gameObject.GetComponent<Item_game>().Init();
                
            }

        }

    }
}
