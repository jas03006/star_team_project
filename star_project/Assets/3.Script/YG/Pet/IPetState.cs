using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPetState
{
    public void Buff();//지속시간 증가
    public IEnumerator Skill_co(); //N초마다 아이템 지급
}

public class BlueState : IPetState
{
    public item_ID item_ID;
    private int time; //지속시간

    public void Buff()
    {
        
    }

    public IEnumerator Skill_co()
    {
        yield return new WaitForSeconds(time);
    }
}

