using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//뒤끝 SDK namespace 추가??
using BackEnd;
using UnityEditor.Experimental.GraphView;

public class Post
{
    public bool isCanReceive = false;

    public string title; //우편 제목
    public string content; //우편 내용
    public string inDate; //우편 inDate

    //string은 우편 아이템 이름 int는 갯수
    public Dictionary<string, int> postReward = new Dictionary<string, int>();

    public override string ToString()
    {
        string result = string.Empty;
        result += $"title : {title}\n";
        result += $"content : {content}\n";
        result += $"inDate : {inDate}\n";

        if (isCanReceive)
        {
            result += "우현 아이템 \n";

            foreach (string itemKey in postReward.Keys)
            {
                result += $"| {itemKey} : {postReward[itemKey]}개\n";
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

    private List<Post> _postList = new List<Post>();

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

            if (postType == PostType.User)
            {
                if (postListJson["itemLocation"]["tableName"].ToString() == "USER_DATA")
                {
                    if (postListJson["itemLocation"]["column"].ToString() == "inventory")
                    {
                        foreach (string itemKey in postListJson["item"].Keys) post.postReward.Add(itemKey, int.Parse(postListJson["item"][itemKey].ToString()));
                    }
                    else
                    {
                        Debug.LogWarning("아직 지원되지 않는 컬럼 정보입니다. :" + postListJson["itemLocation"]["column"].ToString());
                    }
                }
                else
                {
                    Debug.LogWarning("아직 지원되지 않는 테이블 정보 입니다. : " + postListJson["itemLocation"]["tableName"].ToString());
                }
            }
            else
            {
                foreach (LitJson.JsonData itemJson in postListJson["items"])
                {
                    if (itemJson["chartName"].ToString() == chartName)
                    {
                        string itemName = itemJson["item"]["itemName"].ToString();
                        int itemCount = int.Parse(itemJson["itemCount"].ToString());

                        if (post.postReward.ContainsKey(itemName))
                        {
                            post.postReward[itemName] += itemCount;
                        }
                        else
                        {
                            post.postReward.Add(itemName, itemCount);
                        }

                        post.isCanReceive = true;
                    }
                    else
                    {
                        Debug.LogWarning("아직 지원되지 않는 차트 정보 입니다. : " + itemJson["chartName"].ToString());
                        post.isCanReceive = false;
                    }
                }
            }
            _postList.Add(post);
        }
        for (int i = 0; i < _postList.Count; i++)
        {
            Debug.Log($"{i}번 째 우편\n" + _postList[i].ToString());
        }
    }
    public void PostTeceive(PostType postType, int index)
    {
        //우편 개별 수령 및 저장하기
        if (_postList.Count <=0)
        {
            Debug.LogWarning("받을 수 있는 우편이 존재하지 않습니다. 혹은 우편 리스트 불러오기를 먼저 호출해주세요");
            return;
        }
        if (index >= _postList.Count)
        {
            Debug.LogError($"해당 우편은 존재하지 않습니다. : 요청 index{index} / 우편 최대 갯수 : {_postList.Count}");
            return;
        }
        Debug.Log($"{postType.ToString()}의 {_postList[index].inDate} 우편 수령을 요청합니다.");

        var bro = Backend.UPost.ReceivePostItem(postType, _postList[index].inDate);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError($"{postType.ToString()}의 {_postList[index].inDate}");
            return;
        }

        Debug.Log($"{postType.ToString()}의 {_postList[index].inDate} 우편수령에 성공했습니다. : " + bro);

        _postList.RemoveAt(index);

        if (bro.GetFlattenJSON()["postItems"].Count > 0)
        {
            SavePostToLocal(bro.GetFlattenJSON()["postItems"]);
        }
        else
        {
            Debug.LogWarning("수령 가능한 우편 아이템이 존재하지 않습니다.");
        }
        BackendGameData_JGD.Instance.GameDataUpdate();
    }

    public void PostReceiveAll(PostType postType)
    {
        //우편 전체 수령 및 저장
        if (_postList.Count <= 0)
        {
            Debug.LogWarning("받을 수 있는 우편이 존재하지 않습니다. 혹은 우편 리스트 불러오기를 먼저 호출해주세요");
            return;
        }

        Debug.Log($"{postType.ToString()}우편 모두 수령을 요청합니다.");

        var bro = Backend.UPost.ReceivePostItemAll(postType);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError($"{postType.ToString()}우편 수령중 에러가 발생했습니다. : "+bro);
            return;
        }

        Debug.Log("우편 모두 수령에 성공했습니다. : "+bro);

        _postList.Clear();

        foreach (LitJson.JsonData postItemsJson in bro.GetFlattenJSON()["postItems"])
        {
            SavePostToLocal(postItemsJson);
        }

        BackendGameData_JGD.Instance.GameDataUpdate();
    }

}
