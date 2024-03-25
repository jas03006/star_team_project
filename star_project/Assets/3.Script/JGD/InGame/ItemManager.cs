using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ItemManager : MonoBehaviour
{
    private Item data;
    [SerializeField] private Player_Controll_JGD Player;
    [SerializeField] private GameObject Player_obj;
    [SerializeField] GameObject Magnet;
    [SerializeField] GameObject speedUp_Effect;
    [SerializeField] GameObject speedUp_Boost;
    [SerializeField] GameObject size_up;
    [SerializeField] GameObject size_Down;
    [SerializeField] GameObject Damage;

    public float Megnetnum = 0;
    public float SpeedUP = 0;
    public float Size = 0;
    public float Heal = 0;

    private void Start()
    {
    }

    public void UsingHeart(int ID)
    {
        data = BackendChart_JGD.chartData.item_list[ID];
        Player.currentHp += Player.MaxHp * data.percent + ((Player.MaxHp * data.percent) * Heal);
        if (Player.currentHp >= Player.MaxHp)
        {
            Player.currentHp = Player.MaxHp;
        }
    }
    public void UsingStar(int ID)
    {
        data = BackendChart_JGD.chartData.item_list[ID];

        Player.PlayerScore += (int)data.num;
    }
    public void UsingSize(int ID)
    {

        if (SizeControll != null)
        {
            StopCoroutine(SizeControll);
            size_Down.SetActive(false);
            size_up.SetActive(false);
        }
        SizeControll = StartCoroutine(Sizecon(ID));

    }
    public void UsingShield()
    {
        Player.Shild = true;
        AudioManager.instance.SFX_Using_Shield();
        Player.shield.SetActive(true);
    }
    public void UsingSpeedUP(int ID)
    {
        if (Speed_Up != null)
        {
            StopCoroutine(Speed_Up);
        }
        Speed_Up = StartCoroutine(SpeedUp(ID));

    }
    public void UsingMegnet()
    {
        StartCoroutine(Magnetcon());
    }

    Coroutine Speed_Up = null;
    Coroutine Speed_Up_effect = null;
    private IEnumerator SpeedUp(int ID)
    {
        if (Speed_Up_effect != null)
        {
            StopCoroutine(Speed_Up_effect);
        }
        Speed_Up_effect = StartCoroutine(Speed_Up_effect_co());
        data = BackendChart_JGD.chartData.item_list[ID];
        float Speed = Player.Speed; 
        Player.Speed = Player.Speed * (float)data.num;
        yield return new WaitForSecondsRealtime(data.duration + SpeedUP);
        Player.Speed = Speed;
    }
    private IEnumerator Speed_Up_effect_co()
    {
        speedUp_Effect.SetActive(true);
        speedUp_Boost.SetActive(true);
        AudioManager.instance.SFX_Using_SpeedUp();
        yield return new WaitForSeconds(1f);
        speedUp_Effect.SetActive(false);
        yield return new WaitForSeconds(2f);
        speedUp_Boost.SetActive(false);
    }
    Coroutine SizeControll = null;
    private IEnumerator Sizecon(int ID)              //나중에 Lerp사용
    {
        Player.invincibility = false;
        data = BackendChart_JGD.chartData.item_list[ID];
        StartCoroutine(Sizecon_Effect_co(ID));  //사이즈 이펙트
        var scale = new Vector3(0.25f, 0.25f,0.25f);
        if (ID == 33)
        {
            Player.invincibility = true;
        }
        Player_obj.transform.localScale = new Vector3(0.25f, 0.25f,0.25f) * (float)data.num;
        //Player.transform.localScale = Vector2.Ler p(scale, scale * data.Num, Time.deltaTime);

        yield return new WaitForSecondsRealtime(data.duration+ Size);

        Player_obj.transform.localScale = scale;
        Player.invincibility = false;

    }
    private IEnumerator Sizecon_Effect_co(int num)
    {
        switch (num)
        {
            case 33:
                size_up.SetActive(true);
                AudioManager.instance.SFX_Using_SizeUp();
                yield return new WaitForSeconds(1f);
                size_up.SetActive(false);
                break;
            case 34:
                size_Down.SetActive(true);
                AudioManager.instance.SFX_Using_SizeDown();
                yield return new WaitForSeconds(1f);
                size_Down.SetActive(false);
                break;
            default:
                break;
        }
    }
    private IEnumerator Magnetcon()
    {
        data = BackendChart_JGD.chartData.item_list[(int)item_ID.Megnet];
        Magnet.SetActive(true);
        AudioManager.instance.SFX_Using_Magnet();
        yield return new WaitForSecondsRealtime(data.duration+ Megnetnum);

        Magnet.SetActive(false);

    }


}