using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
public class Post_Box_UI : MonoBehaviour
{
    [SerializeField] private GameObject main_UI;
    [SerializeField] private GameObject post_element_prefab;

    [SerializeField] private Transform post_list_container;

    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text content;
    [SerializeField] private TMP_Text item_info;
    [SerializeField] private Button get_reward_btn;


    List<Post> _postList = new List<Post>();
    public void hide_UI() {
        main_UI.SetActive(false);
    }
    public void show_UI()
    {
        if (!main_UI.gameObject.activeSelf) {
            main_UI.SetActive(true);
            load_UI();
        }
       
    }
    public void show_post() { 
        
    }

    #region post
     public void clear_UI()
     {
         //memo_container
         int len = post_list_container.childCount;
         for (int i = 0; i < len; i++)
         {
             Destroy(post_list_container.GetChild(i).gameObject);
         }
     }

     public void load_UI()
     {
         PostListGet(PostType.User);
         clear_UI();
         for (int i = 0; i < _postList.Count; i++)
         {
            create_post(_postList[i], PostType.User);
         }
     }

     public void create_post(Post post, PostType post_type)
     {
         GameObject post_go = Instantiate(post_element_prefab);
         post_go.transform.SetParent(post_list_container);
         post_go.transform.localScale = Vector3.one;

         Post_Element pe = post_go.GetComponent<Post_Element>();

         pe.init(post, delegate () { show_post(pe); }, post_type);        

     }

    public void show_post(Post_Element post_e) {
        title.text = post_e.post.title;
        content.text = post_e.content_str;
        item_info.text = post_e.item_str;

        get_reward_btn.onClick.RemoveAllListeners();
        get_reward_btn.onClick.AddListener(delegate () { receive_post(post_e); });
    }

    public void receive_post(Post_Element post_e) {
        if (post_e.is_received == true) {
            return;
        }

        var bro = Backend.UPost.ReceivePostItem(post_e.post_type, post_e.post.inDate);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError($"{post_e.post_type.ToString()}의 {post_e.post.inDate}");
            return;
        }

        post_e.receive();


        Debug.Log($"{post_e.post_type.ToString()}의 {post_e.post.inDate} 우편수령에 성공했습니다. : " + bro);
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

        if (bro.GetFlattenJSON()["postList"].Count <= 0)
        {
            Debug.LogWarning("받을 우편이 존재하지 않습니다.");
            return;
        }

        _postList = new List<Post>();

        foreach (LitJson.JsonData postListJson in bro.GetFlattenJSON()["postList"])
        {
            Post post = new Post();

            post.title = postListJson["title"].ToString();
            post.content = postListJson["content"].ToString();
            post.inDate = postListJson["inDate"].ToString();

           /* if (postType == PostType.User)
            {
                if (postListJson["itemLocation"]["tableName"].ToString() == "USER_DATA")
                {
                    if (postListJson["itemLocation"]["column"].ToString() == "house_inventory")
                    {
                        foreach (string itemKey in postListJson["item"].Keys)
                        {
                            post.postReward.Add(itemKey, int.Parse(postListJson["item"][itemKey].ToString()));
                        }
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
            }*/
            _postList.Add(post);
        }
        /*for (int i = 0; i < _postList.Count; i++)
        {
            Debug.Log($"{i}번 째 우편\n" + _postList[i].ToString());
        }*/
    }

    #endregion
}
