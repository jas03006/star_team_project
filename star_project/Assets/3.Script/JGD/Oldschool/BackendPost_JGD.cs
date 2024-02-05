using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ڳ� SDK namespace �߰�??
using BackEnd;

public class BackendPost_JGD : MonoBehaviour
{
    public bool isCanReceive = false;

    public string title; //���� ����
    public string content; //���� ����
    public string inDate; //���� inDate

    //string�� ���� ������ �̸� int�� ����
    public Dictionary<string, int> postRewawrd = new Dictionary<string, int>();

    public override string ToString()
    {
        string result = string.Empty;
        result += $"title : {title}\n";
        result += $"content : {content}\n";
        result += $"inDate : {inDate}\n";

        if (isCanReceive)
        {
            result += "���� ������ \n";

            foreach (string itemKey in postRewawrd.Keys)
            {
                result += $"| {itemKey} : {postRewawrd[itemKey]}��\n";
            }
        }
        else
        {
            result += "�������� �ʴ� ���� �������Դϴ�.";
        }
        return result;
    }

    private List<BackendPost_JGD> _postList = new List<BackendPost_JGD>();

    public void SavePostToLocal(LitJson.JsonData item)
    { 
        foreach(LitJson.JsonData itemJson in item)
        {

        }
    }


}
