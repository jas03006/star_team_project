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

    private void Start()
    {

    }

    public void UsingHeart(int ID)
    {
        data = BackendChart_JGD.chartData.item_list[ID];
        Player.currentHp += Player.MaxHp * data.num;
    }
    public void UsingStar(int ID)
    {
        data = BackendChart_JGD.chartData.item_list[ID];

        Player.PlayerScore += (int)data.num;
    }
    public void UsingSize(int ID)
    {
        StartCoroutine(Sizecon(ID));

    }
    public void UsingShild()
    {
        Player.Shild = true;
    }
    public void UsingSpeedUP(int ID)
    {
        StartCoroutine(SpeedUp(ID));

    }
    public void UsingMegnet()
    {
        StartCoroutine(Magnetcon());
    }
    

    private IEnumerator SpeedUp(int ID)
    {
        data = BackendChart_JGD.chartData.item_list[ID];
        float Speed = Player.Speed; 
        Player.Speed = Player.Speed * (float)data.num;
        yield return new WaitForSecondsRealtime(data.duration);
        Player.Speed = Speed;
    }
    private IEnumerator Sizecon(int ID)
    {
        data = BackendChart_JGD.chartData.item_list[ID];
        var scale = new Vector3(0.25f, 0.25f,0.25f);

        Player.invincibility = true;
        Player_obj.transform.localScale = new Vector3(0.25f, 0.25f,0.25f) * (float)data.num;
        //Player.transform.localScale = Vector2.Ler p(scale, scale * data.Num, Time.deltaTime);

        yield return new WaitForSecondsRealtime(data.duration);

        Player_obj.transform.localScale = scale;
        Player.invincibility = false;

    }
    private IEnumerator Magnetcon()
    {
        Magnet.SetActive(true);

        yield return new WaitForSecondsRealtime(data.duration);

        Magnet.SetActive(false);

    }


}