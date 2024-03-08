using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tuto_Player_Controll : MonoBehaviour
{
    private bool isMove = true;
    public bool isUp = true;
    public bool invincibility = false;
    private int PlayerNumber = 0;
    [SerializeField] GameObject cameraCon;
    //[SerializeField] GameObject Itemmanager;
    //[SerializeField] ItemManager itemManager;
    [SerializeField] private float Jump;
    [SerializeField] public float Speed;
    Rigidbody2D rigi;
    [SerializeField] TMP_Text ScoreTxt;
    private int PlayerLevel;
    public double MaxHp = 100;
    public double currentHp;
    [SerializeField] public int PlayerScore;

    [SerializeField] int[] ItemInven = new int[2];   //아이템 저장소
    //[SerializeField] List<int> ItemInven = new List<int>();
    [SerializeField] private Image PlayerItem;
    [SerializeField] private Image PlayerItem2;
    [SerializeField] Sprite PlayerItemInven;
    [SerializeField] public List<string> Alphabet = new List<string>();
    SpriteRenderer character;
    [Header("PlayerUI")]
    [SerializeField] private Slider Hpslider;
    [SerializeField] private Slider Player_Progress;
    [SerializeField] private TMP_Text Player_CatchingStar_Count;
    [SerializeField] private List<Image> Player_Alphabet_progress;
    [SerializeField] private List<Image> Player_Alphabet_BackGround;
    [SerializeField] private Sprite Alphabet_BackGround;
    [Header("PlayerHitByCar")]
    Coroutine now_damage_co = null;
    private int Player_Alphabet_Count = 0;
    [Header("Tuto")]
    //[SerializeField] GameObject TutoObj;
    [SerializeField] TutorialSystem_JGD Tuto;
    [SerializeField] GameObject Magnet;




    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        currentHp = MaxHp;
        Hpslider.maxValue = (float)MaxHp;
        Hpslider.value = float.MaxValue;
        PlayerItem.sprite = PlayerItemInven;
    }
    private void Update()
    {
        Magnet.transform.position = this.transform.position;  //Magnet
        if (this.transform.position.x >= cameraCon.transform.position.x && cameraCon.transform.position.x < 191.3f)
        {
            cameraCon.transform.position = new Vector3(this.transform.position.x, cameraCon.transform.position.y, -3);
        }
        Player_Progress.value = this.transform.position.x;
    }
    private void FixedUpdate()
    {
        if (isMove)
        {
            this.transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
        if (Input.GetMouseButton(0) && isMove && !InputManager.IsPointerOverUI() && isUp)
        {
            rigi.velocity = Vector2.up * Jump;
        }
    }
    private IEnumerator OnDamage(int num)
    {
        if (!invincibility)
        {
            isMove = false;
            currentHp -= num;
            Hpslider.value -= num;
            //AudioManager.instance.SFX_hit();
            rigi.AddForce(Vector2.left * 2f, ForceMode2D.Impulse);
            rigi.AddForce(Vector2.up * 1f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
            isMove = true;
        }
        now_damage_co = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //string layer = collision.gameObject.layer.ToString();
        //if (collision.gameObject.layer == LayerMask.GetMask($"{layer}"))  //이거 되면 이거로
        //{
        //    switch (layer)
        //    {
        //        case "Water":
        //
        //        break;
        //        default:
        //            break;
        //    }
        //}

        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            //튜토리얼 가이드라인

            Tuto.GameStart();


        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            //일반 벽
            if (now_damage_co != null)
            {
                now_damage_co = null;
            }
            now_damage_co = StartCoroutine(OnDamage(50));
            Tuto.GameStart();
            collision.gameObject.SetActive(false);

        }
        else if (collision.gameObject.CompareTag("star_nest_UI"))               //별 아이템
        {
            PlayerScore += 5;
            Player_CatchingStar_Count.text = PlayerScore.ToString();
        }
        else if (collision.gameObject.CompareTag("post_box_UI"))               //별 아이템
        {
            PlayerScore += 1;
            Player_CatchingStar_Count.text = PlayerScore.ToString();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            currentHp = 100f;
            collision.gameObject.SetActive(false);
            Hpslider.value = (float)currentHp;
            if (Tuto.progress == 3)
            {
                Tuto.GameStart();
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall_YG"))  //자석아이템
        {
            if (ItemInven[0] != 1)
            {
                PlayerItem.sprite = SpriteManager.instance.Num2Sprite(4031);
                ItemInven[0] = 1;
                Tuto.GameStart();
            }
            else if (ItemInven[0] == 1)
            {
                PlayerItem2.sprite = SpriteManager.instance.Num2Sprite(4031);
                ItemInven[1] = 1;
            }
            else
            {
                return;
            }
            collision.gameObject.SetActive(false);
        }
        //else if (collision.gameObject.layer == LayerMask.NameToLayer("MoveWall"))
        //{
        //    //노란색 벽
        //    if (now_damage_co != null)  
        //    {
        //        now_damage_co = null;
        //    }
        //    now_damage_co = StartCoroutine(OnDamage(0));
        //    collision.gameObject.SetActive(false);
        //}
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Alphabet"))
        {
            Alphabet.Add("a");
            switch (Alphabet.Count)
            {
                case 1:
                    Player_Alphabet_progress[0].sprite = SpriteManager.instance.Num2Sprite(4018);
                    Player_Alphabet_BackGround[0].sprite = Alphabet_BackGround;
                    break;
                case 2:
                    Player_Alphabet_progress[1].sprite = SpriteManager.instance.Num2Sprite(4019);
                    Player_Alphabet_BackGround[1].sprite = Alphabet_BackGround;
                    break;
                case 3:
                    Player_Alphabet_progress[2].sprite = SpriteManager.instance.Num2Sprite(4000);
                    Player_Alphabet_BackGround[2].sprite = Alphabet_BackGround;
                    break;
                case 4:
                    Player_Alphabet_progress[3].sprite = SpriteManager.instance.Num2Sprite(4017);
                    Player_Alphabet_BackGround[3].sprite = Alphabet_BackGround;
                    break;

                default:
                    break;
            }
        }

    }
    public void UsingItem()
    {
        if (ItemInven[1] == 1)
        {
            ItemInven[1] = 0;
            PlayerItem2.sprite = PlayerItemInven;
            Magnet.SetActive(true);
        }
        else if (ItemInven[0] == 1)
        {
            ItemInven[0] = 0;
            PlayerItem.sprite = PlayerItemInven;
            Magnet.SetActive(true);
        }
        if (ItemInven[0] == 1 && Tuto.progress == 8)
        {
            Tuto.progress++;
            Tuto.GameStart();
        }
    }
}
