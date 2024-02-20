using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Controll_JGD : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] private float Jump;
    [SerializeField] public float Speed;
    Rigidbody2D rigi;
    [SerializeField] TMP_Text ScoreTxt;

    //int PlayerLevel = BackendGameData_JGD.userData.level;
    private int MaxHp = 100;
    public int currentHp;
    [SerializeField]private int PlayerScore;

    [SerializeField] int[] ItmeInven = new int[2];   //아이템 저장소

    ItemID_JGD itemID;


    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
       
    }
    private void Start()
    {
        //MaxHp += PlayerLevel * 10;  //이거 근대 기획문서대로하면 만렙이면 최대체력 390임ㅋㅋ
        currentHp = MaxHp;

    }
    private void Update()
    {
        if (this.transform.position.x >=camera.transform.position.x && camera.transform.position.x < 67.85f)
        {
            camera.transform.position = new Vector3(this.transform.position.x, camera.transform.position.y, -3);
        }

    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigi.velocity = Vector2.up *Jump;
        }
    }
    private void TimetoScore()
    {

    }
    private void OnDamage()
    {
        currentHp -= 20;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Destroy(collision.gameObject);
            switch (itemID.obstacle_ID)
            {
                case Obstacle_ID.big_heart:

                    break;
                case Obstacle_ID.small_heart:

                    break;
                case Obstacle_ID.small_star:

                    break;
                case Obstacle_ID.big_star:

                    break;
                case Obstacle_ID.CheckBox:
                    
                    break;
                case Obstacle_ID.Shield:

                    break;
                case Obstacle_ID.Megnet:

                    break;
                case Obstacle_ID.SpeedUp:

                    break;
                case Obstacle_ID.SizeUp:

                    break;
                case Obstacle_ID.SizeDown:

                    break;
                case Obstacle_ID.Random:

                    break;
                default:
                    break;
            }
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(collision.gameObject); //이거 나중에 벽 부서지는효과로 변경
            switch (itemID.obstacle_ID)
            {
                case Obstacle_ID.NomalWall:
                case Obstacle_ID.Nomal_Rockwall:
                    OnDamage();
                    break;
                case Obstacle_ID.BlueWall:
                case Obstacle_ID.Blue_Rockwall:
                    OnDamage();
                    break;
                case Obstacle_ID.GreenWall:
                case Obstacle_ID.Green_Rockwall:

                    OnDamage();
                    break;
                case Obstacle_ID.PurpleWall:
                case Obstacle_ID.Purple_Rockwall:

                    OnDamage();
                    break;
                case Obstacle_ID.RedWall:
                case Obstacle_ID.Red_Rockwall:

                    OnDamage();
                    break;
                case Obstacle_ID.YellowWall:
                case Obstacle_ID.Yellow_Rockwall:

                    OnDamage();
                    break;

                case Obstacle_ID.BlackHole:
                case Obstacle_ID.Meteor:

                    break;

                default:
                    break;
            }
        }
    }

}
