using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPetState
{
    public void Buff();//���ӽð� ����
    public IEnumerator Skill_co(); //N�ʸ��� ������ ����
}

public class BlueState : IPetState
{
    public item_ID item_ID;
    private int time; //���ӽð�

    public void Buff()
    {
        
    }

    public IEnumerator Skill_co()
    {
        yield return new WaitForSeconds(time);
    }
}

