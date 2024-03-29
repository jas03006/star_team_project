using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player_Controll_JGD : MonoBehaviour
{
    private bool isMove = true;
    public bool invincibility = false;
    public bool Shild = false;
    private int PlayerNumber = 0;
    [SerializeField] GameObject cameraCon;
    [SerializeField] GameObject Itemmanager;
    [SerializeField] ItemManager itemManager;
    [SerializeField] private float Jump;
    [SerializeField] public float Speed;
    Rigidbody2D rigi;
    [SerializeField] TMP_Text ScoreTxt;
    private int PlayerLevel;
    public double MaxHp = 100;
    public double currentHp;
    [SerializeField]public int PlayerScore;
    [SerializeField] private GameObject PlayerDieUI;

    [SerializeField] int[] ItemInven = new int[2];   //아이템 저장소
    //[SerializeField] List<int> ItemInven = new List<int>();
    [SerializeField] private Image PlayerItem;
    [SerializeField] private Image PlayerItem2;
    [SerializeField] Sprite PlayerItemInven;
    [SerializeField] public List<int> Alphabet = new List<int>();
    [SerializeField] private Character cur_character;
    SpriteRenderer character;
    [Header("PlayerUI")]
    [SerializeField] private Slider Hpslider;
    [SerializeField] private Slider Player_Progress;
    [SerializeField] private TMP_Text Player_CatchingStar_Count;
    [SerializeField] private List<Image> Player_Alphabet_progress;
    [SerializeField] private List<Image> Player_Alphabet_BackGround;
    [SerializeField] private Sprite Alphabet_BackGround;
    [Header("PlayerHitByCar")]
    private bool isHitOn = true;
    [SerializeField] private float DamageTime;
    public int Player_Alphabet_Count = 0;

    [SerializeField] public GameObject shield;
    [SerializeField] public GameObject DamageEffect;
    Coroutine damageeffect = null;



    [SerializeField] GameEnd_JGD endgame;


    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        character = GetComponent<SpriteRenderer>();
        
    }
    private void Start()
    {
        Init();
        for (int i = 0; i < endgame.ClearData.Count; i++)
        {
            Alphabet.Add(-1);
        }
        PlayerDieUI.SetActive(false);
        MaxHp += (PlayerLevel - 1) * 10;  
        currentHp = MaxHp;
        Hpslider.maxValue = (float)MaxHp;
        Hpslider.value = float.MaxValue;
        //성유경
        //cur_character = BackendChart_JGD.chartData.character_list[BackendGameData_JGD.userData.character];

    }
    private void FixedUpdate()
    {
        Itemmanager.transform.position = this.transform.position;
        if (this.transform.position.x >= cameraCon.transform.position.x && cameraCon.transform.position.x < 84f)
        {
            cameraCon.transform.position = new Vector3(this.transform.position.x, cameraCon.transform.position.y, cameraCon.transform.position.z);
        }
        Player_Progress.value = this.transform.position.x;
        if (isMove)
        {
            this.transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
        if (Input.GetMouseButton(0) && isMove && !InputManager.IsPointerOverUI())
        {
            rigi.velocity = Vector2.up *Jump;
        }
    }
   

    private void Init()
    {
        cur_character = BackendChart_JGD.chartData.character_list[BackendGameData_JGD.userData.character];
        character.sprite = SpriteManager.instance.Num2Sprite(cur_character.sprite);
        PlayerNumber = (int)cur_character.character_ID;
        PlayerLevel = cur_character.curlevel;
        ItemChangeSlot1((int)cur_character.special_item);

        switch (cur_character.character_ID)
        {
            case Character_ID.Yellow:
                itemManager.Megnetnum = (PlayerLevel - 1) * 0.1f + 0.5f; 
                break;
            case Character_ID.Red:
                itemManager.SpeedUP = (PlayerLevel - 1) * 0.1f + 0.5f;
                break;
            case Character_ID.Blue:
                itemManager.Size = (PlayerLevel - 1) * 0.1f + 0.5f;
                break;
            case Character_ID.Purple:
                itemManager.Size = (PlayerLevel - 1) * 0.1f + 0.5f;
                break;
            case Character_ID.Green:
                itemManager.Heal = (PlayerLevel - 1) * 0.005f + 0.05f;
                break;
        }

        //Yellow = 0,
        //Red,
        //Blue,
        //Purple,
        //Green
    }

    Coroutine now_damage_co = null;
    private IEnumerator OnDamage(int num, Collider2D collision)
    {
        if (!invincibility && !Shild)
        {
            if (damageeffect != null)
            {
                StopCoroutine(damageeffect);
                DamageEffect.SetActive(false);
            }
            damageeffect = StartCoroutine(OnDamageEffect_co());  // 데미지 이펙트 구간
            isMove = false;
            isHitOn = false;
            currentHp -= num;
            Hpslider.value -= num;
            if (currentHp <= 0)
            {
                Time.timeScale = 0;
                AudioManager.instance.SFX_game_over();
                PlayerDieUI.SetActive(true);
                if (now_damage_co != null)
                {
                    StopCoroutine(now_damage_co);
                }

            }
            AudioManager.instance.SFX_hit();
            Vector2 point = collision.ClosestPoint(transform.position);
            if (this.transform.position.x > point.x)
            {
                rigi.AddForce(Vector2.up * 1f, ForceMode2D.Impulse);
                rigi.AddForce(Vector2.right * 2f, ForceMode2D.Impulse);
            }
            else
            {
                rigi.AddForce(Vector2.up * 1f, ForceMode2D.Impulse);
                rigi.AddForce(Vector2.left * 2f, ForceMode2D.Impulse);
            }
            //Vector2 dir = ((Vector2)transform.position - collision.ClosestPoint(transform.position)).normalized + Vector2.up/2f;
            //rigi.AddForce(dir * 2f, ForceMode2D.Impulse);
            // rigi.AddForce(Vector2.left * 2f, ForceMode2D.Impulse);
            //rigi.AddForce(Vector2.up * 1f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
            rigi.velocity = Vector2.up * rigi.velocity.y;
            isMove = true;
        }
        if (Shild)
        {
            Shild = false;
            shield.SetActive(false);
        }
        now_damage_co = null;
    }
    private IEnumerator OnDamageEffect_co()
    {
        DamageEffect.transform.position = this.transform.position;
        DamageEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        DamageEffect.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Alphabet"))
        {
            AudioManager.instance.SFX_collect_item();
            for (int i = 0; i < endgame.ClearData.Count; i++)
            {
                if (endgame.ClearData[i] == collision.gameObject.GetComponent<ItemID_JGD>().obstacle_ID.ToString() && Alphabet[i] == -1)
                {
                    Alphabet[i] = (int)collision.gameObject.GetComponent<ItemID_JGD>().obstacle_ID;
                    Player_Alphabet_progress[i].sprite = SpriteManager.instance.Num2Sprite(4000 + (int)collision.gameObject.GetComponent<ItemID_JGD>().obstacle_ID);
                    Player_Alphabet_BackGround[i].sprite = Alphabet_BackGround;
                    Player_Alphabet_Count++;
                    break;
                }
            }
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
                    Hpslider.value = (float)currentHp;
                    Debug.Log(currentHp);
                    break;
                case Obstacle_ID.small_heart:
                    AudioManager.instance.SFX_collect_heart();
                    itemManager.UsingHeart((int)Obstacle_ID.small_heart);
                    //collision.GetComponent<Heart>().UseItem();
                    Hpslider.value = (float)currentHp;
                    Debug.Log(currentHp);
                    break;
                case Obstacle_ID.small_star:
                    AudioManager.instance.SFX_collect_star();
                    itemManager.UsingStar((int)Obstacle_ID.small_star);
                    //collision.GetComponent<Star>().UseItem();
                    Player_CatchingStar_Count.text = PlayerScore.ToString();
                    Debug.Log(PlayerScore);
                    break;
                case Obstacle_ID.big_star:
                    AudioManager.instance.SFX_collect_star();
                    itemManager.UsingStar((int)Obstacle_ID.big_star);
                    //collision.GetComponent<Star>().UseItem();
                    Player_CatchingStar_Count.text = PlayerScore.ToString();
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
                    if (PlayerNumber == 2)
                    {
                        //if (now_damage_co != null)
                        //{
                        //    StopCoroutine(now_damage_co);
                        //}
                        //now_damage_co = StartCoroutine(OnDamage(0, collision));
                        if (damageeffect != null)
                        {
                            StopCoroutine(damageeffect);
                            DamageEffect.SetActive(false);
                        }
                        damageeffect = StartCoroutine(OnDamageEffect_co());
                        collision.gameObject.SetActive(false);
                        return;
                    }
                    break;
                case Obstacle_ID.GreenWall:
                case Obstacle_ID.Green_Rockwall:
                case Obstacle_ID.Move_GreenWall:
                    if (PlayerNumber == 4)
                    {
                        //if (now_damage_co != null)
                        //{
                        //    StopCoroutine(now_damage_co);
                        //}
                        //now_damage_co = StartCoroutine(OnDamage(0, collision));
                        if (damageeffect != null)
                        {
                            StopCoroutine(damageeffect);
                            DamageEffect.SetActive(false);
                        }
                        damageeffect = StartCoroutine(OnDamageEffect_co());
                        collision.gameObject.SetActive(false);
                        return;
                    }

                    break;
                case Obstacle_ID.PurpleWall:
                case Obstacle_ID.Purple_Rockwall:
                case Obstacle_ID.Move_PurpleWall:
                    if (PlayerNumber == 3)
                    {
                        //if (now_damage_co != null)
                        //{
                        //    StopCoroutine(now_damage_co);
                        //}
                        //now_damage_co = StartCoroutine(OnDamage(0, collision));
                        if (damageeffect != null)
                        {
                            StopCoroutine(damageeffect);
                            DamageEffect.SetActive(false);
                        }
                        damageeffect = StartCoroutine(OnDamageEffect_co());
                        collision.gameObject.SetActive(false);
                        return;
                    }

                    break;
                case Obstacle_ID.RedWall:
                case Obstacle_ID.Red_Rockwall:
                case Obstacle_ID.Move_RedWall:
                    if (PlayerNumber == 1)
                    {
                        //if (now_damage_co != null)
                        //{
                        //    StopCoroutine(now_damage_co);
                        //}
                        //now_damage_co = StartCoroutine(OnDamage(0, collision));
                        if (damageeffect != null)
                        {
                            StopCoroutine(damageeffect);
                            DamageEffect.SetActive(false);
                        }
                        damageeffect = StartCoroutine(OnDamageEffect_co());
                        collision.gameObject.SetActive(false);
                        return;
                    }

                    break;
                case Obstacle_ID.YellowWall:
                case Obstacle_ID.Yellow_Rockwall:
                case Obstacle_ID.Move_YellowWall:
                    if (PlayerNumber == 0)
                    {
                        //if (now_damage_co != null)
                        //{
                        //    StopCoroutine(now_damage_co);
                        //}
                        //now_damage_co = StartCoroutine(OnDamage(0, collision));
                        if (damageeffect != null)
                        {
                            StopCoroutine(damageeffect);
                            DamageEffect.SetActive(false);
                        }
                        damageeffect = StartCoroutine(OnDamageEffect_co());
                        collision.gameObject.SetActive(false);
                        return;
                    }

                    break;

                case Obstacle_ID.Meteor:

                    break;
                case Obstacle_ID.Unbreakable_Wall:
                case Obstacle_ID.Unbreakable_MoveWall:
                    if (isMove)
                    {
                        if (now_damage_co != null)
                        {
                            StopCoroutine(now_damage_co);
                        }
                        now_damage_co = StartCoroutine(OnDamage(20, collision));
                    }
                    return;
                default:
                    break;
            }
            if (isMove)
            {
                if (now_damage_co != null)
                {
                    StopCoroutine(now_damage_co);
                }
                now_damage_co = StartCoroutine(OnDamage(20, collision));
            }
            //if (now_damage_co != null)    //코루틴 쓰는 방법
            //{
            //    StopCoroutine(now_damage_co);
            //}
            //now_damage_co = StartCoroutine(OnDamage(20, collision));
            
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
                itemManager.UsingShield();
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
            default:
                break;
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("MoveWall"))
    //    {
    //        if (isMove)
    //        {
    //            if (now_damage_co != null)
    //            {
    //                StopCoroutine(now_damage_co);
    //            }
    //            now_damage_co = StartCoroutine(OnDamage(20, collision));
    //        }
    //    }
    //}
}