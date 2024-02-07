using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//뒤끝 SDK namespace 추가??
using BackEnd;

public class Post
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
}
public class BackendPost_JGD : MonoBehaviour
{
    private static BackendPost_JGD instance;
    public static BackendPost_JGD Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BackendPost_JGD();
            }
            return instance;
        }
    }

    private List<BackendPost_JGD> _postList = new List<BackendPost_JGD>();

    public void SavePostToLocal(LitJson.JsonData item)
    { 
        foreach(LitJson.JsonData itemJson in item)
        {
            if (itemJson["item"].ContainsKey("itemType")) 
            {
                int itemId = int.Parse(itemJson["item"]["itemId"].ToString());
                string itemType = itemJson["item"]["itemType"].ToString();
                string itemName = itemJson["item"]["itemName"].ToString();
                int itemCount = int.Parse(itemJson["itemCount"].ToString());

                if (BackendGameData_JGD.userData.inventory.ContainsKey(itemName))    //////////우편 넣는 방식
                {
                    BackendGameData_JGD.userData.inventory[itemName] += itemCount;
                }
                else
                {
                    BackendGameData_JGD.userData.inventory.Add(itemName, itemCount);
                }
                Debug.Log($"아이템을 수령했습니다. : {itemName} - {itemCount}개");
            }
            else
            {
                Debug.LogError("지원하지 않는 item입니다.");
            }
        }
    }
    public void PostListGet(PostType postType)
    {
        //우편 불러오기
        var bro = Backend.UPost.GetPostList(postType);

        string chartName = "Teat";

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("우편 불러오기 중 에러 발생");
            return;
        }
        Debug.Log("우편 불러오기 요청에 성공");

        if (bro.GetFlattenJSON()["postList"].Count <=0)
        {
            Debug.LogWarning("받을 우편이 존재하지 않습니다.");
            return;
        }

        foreach (LitJson.JsonData postListJson in bro.GetFlattenJSON()["postList"])
        {
            Post post = new Post();

            post.title = postListJson["title"].ToString();
            post.content = postListJson["content"].ToString();
            post.inDate = postListJson["inDate"].ToString();

            if (true)
            {

            }
        }

    }
    public void PostTeceive(PostType postType, int index)
    {
        //우편 개별 수령 및 저장하기
    }

    public void PostReceiveAll(PostType postType)
    {
        //우편 전체 수령 및 저장
    }

}
