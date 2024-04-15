using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : Item_game
{
    Item_game item_;
    Player_Controll_JGD Player;
    private void Start()
    {
        item_ = GetComponent<Item_game>();
        Player = FindAnyObjectByType<Player_Controll_JGD>();
    }
    public void UseItem()
    {
        StartCoroutine(Sizecon());
    }
    private IEnumerator Sizecon()
    {
        var scale = new Vector2(0.25f, 0.25f);
        Player.invincibility = true; 
        Player.transform.localScale = new Vector2(0.25f,0.25f) * (float)item_.Num;
        //Player.transform.localScale = Vector2.Lerp(scale, scale * item_.Num, Time.deltaTime);

        yield return new WaitForSecondsRealtime(item_.duration);

        Player.transform.localScale = scale;
        Player.invincibility = false;

    }
}