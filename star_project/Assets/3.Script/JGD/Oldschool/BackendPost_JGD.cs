using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//뒤끝 SDK namespace 추가??
using BackEnd;

public class BackendPost_JGD : MonoBehaviour
{
    public bool isCanReceive = false;

    public string title; //우편 제목
    public string content; //우편 내용
    public string inDate; //우편 inDate

    //string은 우편 아이템 이름 int는 갯수
    public Dictionary<string, int> postRewawrd = new Dictionary<string, int>();

    public override string ToString()
    {
        string result = string.Empty;
        result += $"title : {title}\n";
        result += $"content : {content}\n";
        result += $"inDate : {inDate}\n";

        if (isCanReceive)
        {
            result += "우현 아이템 \n";

            foreach (string itemKey in postRewawrd.Keys)
            {
                result += $"| {itemKey} : {postRewawrd[itemKey]}개\n";
            }
        }
        else
        {
            result += "지원하지 않는 우편 아이템입니다.";
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
