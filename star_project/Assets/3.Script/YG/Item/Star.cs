using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Star : Item_game
{
    Item_game item_;
    int Score;
    private void Start()
    {
        item_ = GetComponent<Item_game>();
    }

    public void UseItem()
    {
        Score = game.GetComponent<Player_Controll_JGD>().PlayerScore;

        Score += (int)item_.Num;

        game.GetComponent <Player_Controll_JGD>().PlayerScore = Score;
    }

}