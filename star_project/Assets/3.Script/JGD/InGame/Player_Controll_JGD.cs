using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Controll_JGD : MonoBehaviour
{
    private bool isMove = true;
    [SerializeField] GameObject cameraCon;
    [SerializeField] private float Jump;
    [SerializeField] public float Speed;
    Rigidbody2D rigi;
    [SerializeField] TMP_Text ScoreTxt;

    private int PlayerLevel=1;
    public double MaxHp = 100;
    public double currentHp;
    [SerializeField]public int PlayerScore;
    [SerializeField] private GameObject PlayerDieUI;

    [SerializeField] int[] ItmeInven = new int[2];   //아이템 저장소
    [SerializeField] public List<string> Alphabet = new List<string>();



    Touch touch = Input.GetTouch(0);

    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        PlayerDieUI.SetActive(false);
        PlayerLevel = BackendGameData_JGD.userData.character_info.character_dic[0];
        MaxHp += (PlayerLevel - 1) * 10;  
        currentHp = MaxHp;

    }
    private void Update()
    {
        if (this.transform.position.x >= cameraCon.transform.position.x && cameraCon.transform.position.x < 84f)
        {
            cameraCon.transform.position = new Vector3(this.transform.position.x, cameraCon.transform.position.y, -3);
        }

    }
    private void FixedUpdate()
    {
        if (isMove)
        {
            this.transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
        if (Input.GetMouseButton(0))
        {
            rigi.velocity = Vector2.up *Jump;
        }
    }

    private IEnumerator OnDamage()
    {
        isMove = false;
        currentHp -= 80;
        if (currentHp < 0)
        {
            Time.timeScale = 0;
            PlayerDieUI.SetActive(true);
        }
        rigi.AddForce(Vector2.left * 2f,ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isMove = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Alphabet"))
        {
            Alphabet.Add(collision.gameObject.GetComponent<ItemID_JGD>().obstacle_ID.ToString());
            Destroy(collision.gameObject);
            return;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            switch (collision.gameObject.GetComponent<ItemID_JGD>().obstacle_ID)
            {
                case Obstacle_ID.big_heart:
                    collision.GetComponent<Heart>().UseItem();
                    Debug.Log(currentHp);
                    break;
                case Obstacle_ID.small_heart:
                    collision.GetComponent<Heart>().UseItem();
                    Debug.Log(currentHp);
                    break;
                case Obstacle_ID.small_star:
                    collision.GetComponent<Star>().UseItem();
                    Debug.Log(PlayerScore);
                    break;
                case Obstacle_ID.big_star:
                    collision.GetComponent<Star>().UseItem();
                    Debug.Log(PlayerScore);
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
            Destroy(collision.gameObject);
            return;
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("MoveWall"))
        {
            //Destroy(collision.gameObject); //이거 나중에 벽 부서지는효과로 변경
            switch (collision.gameObject.GetComponentInParent<ItemID_JGD>().obstacle_ID)
            {
                case Obstacle_ID.NomalWall:
                case Obstacle_ID.Nomal_Rockwall:
                case Obstacle_ID.Move_NomalWall:
                    
                    break;
                case Obstacle_ID.BlueWall:
                case Obstacle_ID.Blue_Rockwall:
                case Obstacle_ID.Move_BlueWall:

                    break;
                case Obstacle_ID.GreenWall:
                case Obstacle_ID.Green_Rockwall:
                case Obstacle_ID.Move_GreenWall:


                    break;
                case Obstacle_ID.PurpleWall:
                case Obstacle_ID.Purple_Rockwall:
                case Obstacle_ID.Move_PurpleWall:


                    break;
                case Obstacle_ID.RedWall:
                case Obstacle_ID.Red_Rockwall:
                case Obstacle_ID.Move_RedWall:


                    break;
                case Obstacle_ID.YellowWall:
                case Obstacle_ID.Yellow_Rockwall:
                case Obstacle_ID.Move_YellowWall:


                    break;

                case Obstacle_ID.BlackHole:
                case Obstacle_ID.Meteor:

                    break;

                default:
                    break;
            }
            StartCoroutine(OnDamage());
            Destroy(collision.gameObject);
            return;
        }
    }

}
