using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player_Controll_JGD : MonoBehaviour
{
    private bool isMove = true;
    public bool invincibility = false;
    public bool Shild = false;
    [SerializeField] GameObject cameraCon;
    [SerializeField] GameObject Itemmanager;
    [SerializeField] ItemManager itemManager;
    [SerializeField] private float Jump;
    [SerializeField] public float Speed;
    Rigidbody2D rigi;
    [SerializeField] TMP_Text ScoreTxt;

    private int PlayerLevel=1;
    public double MaxHp = 100;
    public double currentHp;
    [SerializeField]public int PlayerScore;
    [SerializeField] private GameObject PlayerDieUI;

    [SerializeField] int[] ItemInven = new int[2];   //아이템 저장소
    //[SerializeField] List<int> ItemInven = new List<int>();
    [SerializeField] Image PlayerItem;
    [SerializeField] Image PlayerItem2;
    [SerializeField] Sprite PlayerItemInven;
    [SerializeField] public List<string> Alphabet = new List<string>();

    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        ItemInven[0] = 0;
        PlayerDieUI.SetActive(false);
        PlayerLevel = BackendGameData_JGD.userData.character_info.character_dic[0];
        MaxHp += (PlayerLevel - 1) * 10;  
        currentHp = MaxHp;

    }
    private void Update()
    {
        Itemmanager.transform.position = this.transform.position;
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
        if (Input.GetMouseButton(0) && isMove && !InputManager.IsPointerOverUI())
        {
            rigi.velocity = Vector2.up *Jump;
        }
    }
   

    Coroutine now_damage_co = null;
    private IEnumerator OnDamage()
    {
        if (!invincibility && !Shild)
        {
            isMove = false;
            currentHp -= 20;
            if (currentHp <= 0)
            {
                Time.timeScale = 0;
                AudioManager.instance.SFX_game_over();
                PlayerDieUI.SetActive(true);
                StopCoroutine(now_damage_co);
                if (now_damage_co != null)    //코루틴 쓰는 방법
                {
                    StopCoroutine(now_damage_co);
                }

            }
            AudioManager.instance.SFX_hit();
            rigi.AddForce(Vector2.left * 2f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
            isMove = true;
        }
        Shild = false;
        now_damage_co = null;
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
                    AudioManager.instance.SFX_collect_heart();
                    itemManager.UsingHeart((int)Obstacle_ID.big_heart);
                    //collision.GetComponent<Heart>().UseItem();
                    Debug.Log(currentHp);
                    break;
                case Obstacle_ID.small_heart:
                    AudioManager.instance.SFX_collect_heart();
                    itemManager.UsingHeart((int)Obstacle_ID.small_heart);
                    //collision.GetComponent<Heart>().UseItem();
                    Debug.Log(currentHp);
                    break;
                case Obstacle_ID.small_star:
                    AudioManager.instance.SFX_collect_star();
                    itemManager.UsingStar((int)Obstacle_ID.small_star);
                    //collision.GetComponent<Star>().UseItem();
                    Debug.Log(PlayerScore);
                    break;
                case Obstacle_ID.big_star:
                    AudioManager.instance.SFX_collect_star();
                    itemManager.UsingStar((int)Obstacle_ID.big_star);
                    //collision.GetComponent<Star>().UseItem();
                    Debug.Log(PlayerScore);
                    break;
                case Obstacle_ID.CheckBox:
                    
                    break;
                case Obstacle_ID.Shield:
                    AudioManager.instance.SFX_collect_item();
                    if (ItemInven[0] == 0)
                    {
                        ItemChangeSlot1((int)Obstacle_ID.Shield);
                    }
                    else
                    {
                        ItemChangeSlot2((int)Obstacle_ID.Shield);
                    }

                    break;
                case Obstacle_ID.Megnet:
                    AudioManager.instance.SFX_collect_item();
                    if (ItemInven[0]== 0)
                    {
                        ItemChangeSlot1((int)Obstacle_ID.Megnet);
                    }
                    else
                    {
                        ItemChangeSlot2((int)Obstacle_ID.Megnet);

                    }
                    break;
                case Obstacle_ID.SpeedUp:
                    AudioManager.instance.SFX_collect_item();
                    if (ItemInven[0] == 0)
                    {
                        ItemChangeSlot1((int)Obstacle_ID.SpeedUp);
                    }
                    else
                    {
                        ItemChangeSlot2((int)Obstacle_ID.SpeedUp);
                    }
                    break;
                case Obstacle_ID.SizeUp:
                    AudioManager.instance.SFX_collect_item();
                    if (ItemInven[0] == 0)
                    {
                        ItemChangeSlot1((int)Obstacle_ID.SizeUp);
                    }
                    else
                    {
                        ItemChangeSlot2((int)Obstacle_ID.SizeUp);
                    }
                    break;
                case Obstacle_ID.SizeDown:
                    AudioManager.instance.SFX_collect_item();
                    if (ItemInven[0] == 0)
                    {
                        ItemChangeSlot1((int)Obstacle_ID.SizeDown);
                    }
                    else
                    {
                        ItemChangeSlot2((int)Obstacle_ID.SizeDown);
                    }
                    break;
                case Obstacle_ID.Random:
                    AudioManager.instance.SFX_collect_item();
                    int ran;
                    ran = Random.Range(30, 35);
                    if (ItemInven[0] == 0)
                    {
                        ItemChangeSlot1(ran);
                    }
                    else
                    {
                        ItemChangeSlot2(ran);
                    }
                    break;
                default:
                    break;
                    

            }
            collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);
            return;
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("MoveWall"))
        {
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
            if (now_damage_co != null)    //코루틴 쓰는 방법
            {
                StopCoroutine(now_damage_co);
            }
            now_damage_co = StartCoroutine(OnDamage());
            
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
            return;
        }
    }
    private void ItemChangeSlot1(int Num)
    {
        ItemInven[0] = Num;
        PlayerItem.sprite = SpriteManager.instance.Num2Sprite(4000 + ItemInven[0]);
    }
    private void ItemChangeSlot2(int Num)
    {
        ItemInven[1] = Num;
        PlayerItem2.sprite = SpriteManager.instance.Num2Sprite(4000 + ItemInven[1]);
    }
    public void UseItem()
    {
        int slot1 = ItemInven[1];
        int slot2 = 0;
        itemnum(ItemInven[0]);
        PlayerItem.sprite = PlayerItemInven;
        ItemInven[0] = slot1;
        if (ItemInven[1] == 0)
        {
            return;
        }
        ItemInven[1] = slot2;
        PlayerItem.sprite = SpriteManager.instance.Num2Sprite(4000 + ItemInven[0]);
        PlayerItem2.sprite = PlayerItemInven;
    }
    public void ItemChange()
    {
        if (ItemInven[1] == 0)
        {
            return;
        }
        int slot1 = ItemInven[0];
        int slot2 = ItemInven[1];
        ItemInven[0] = slot2;
        ItemInven[1] = slot1;

        PlayerItem.sprite = SpriteManager.instance.Num2Sprite(4000+ItemInven[0]);
        PlayerItem2.sprite = SpriteManager.instance.Num2Sprite(4000 + ItemInven[1]);
    }
    private void itemnum(int num)
    {
        switch (num)
        {
            case 30:
                itemManager.UsingShild();
                break;
            case 31:
                itemManager.UsingMegnet();
                break;
            case 32:
                itemManager.UsingSpeedUP(32);
                break;
            case 33:
                itemManager.UsingSize(33);
                break;
            case 34:
                itemManager.UsingSize(34);
                break;
            case 35:
                int ran;
                ran = Random.Range(30, 35); /////////////나중에 지우기
                itemnum(ran);
                break;
            default:
                break;
        }
    }
}
