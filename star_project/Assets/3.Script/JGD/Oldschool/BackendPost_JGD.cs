using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ڳ� SDK namespace �߰�??
using BackEnd;

public class Post
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

                if (BackendGameData_JGD.userData.inventory.ContainsKey(itemName))    //////////���� �ִ� ���
                {
                    BackendGameData_JGD.userData.inventory[itemName] += itemCount;
                }
                else
                {
                    BackendGameData_JGD.userData.inventory.Add(itemName, itemCount);
                }
                Debug.Log($"�������� �����߽��ϴ�. : {itemName} - {itemCount}��");
            }
            else
            {
                Debug.LogError("�������� �ʴ� item�Դϴ�.");
            }
        }
    }
    public void PostListGet(PostType postType)
    {
        //���� �ҷ�����
        var bro = Backend.UPost.GetPostList(postType);

        string chartName = "Teat";

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("���� �ҷ����� �� ���� �߻�");
            return;
        }
        Debug.Log("���� �ҷ����� ��û�� ����");

        if (bro.GetFlattenJSON()["postList"].Count <=0)
        {
            Debug.LogWarning("���� ������ �������� �ʽ��ϴ�.");
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
        //���� ���� ���� �� �����ϱ�
    }

    public void PostReceiveAll(PostType postType)
    {
        //���� ��ü ���� �� ����
    }

}
