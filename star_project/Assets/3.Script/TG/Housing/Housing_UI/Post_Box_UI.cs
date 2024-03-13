using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using System.Linq;
using System;

public class Post_Box_UI : MonoBehaviour
{
    [SerializeField] private GameObject main_UI;
    [SerializeField] private GameObject post_element_prefab;

    [SerializeField] private Transform post_list_container;

    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text date;
    [SerializeField] private TMP_Text content;
    [SerializeField] private TMP_Text item_info;
    [SerializeField] private Button get_reward_btn;
    [SerializeField] private GameObject check_image;

    private GameObject now_elem_highlight = null; 

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
         for (int i = 0; i < post_list_container.childCount; i++)
         {
             Destroy(post_list_container.GetChild(i).gameObject);
         }
     }

    public void update_highlight(GameObject go) {
        if (now_elem_highlight != null) { 
            now_elem_highlight.SetActive(false);
        }
        now_elem_highlight = go;
        now_elem_highlight.SetActive(true);
    }

     public void load_UI()
     {
        clear_UI();
        PostListGet(PostType.User);
         for (int i = 0; i < _postList.Count; i++)
         {
            Debug.Log($"create post {i}");
            create_post(_postList[i], PostType.User);
         }
        sort();
     }

     public void create_post(Post post, PostType post_type)
     {
         GameObject post_go = Instantiate(post_element_prefab, post_list_container);
         //post_go.transform.localScale = Vector3.one;

         Post_Element pe = post_go.GetComponent<Post_Element>();

         pe.init(post, delegate () { show_post(pe); }, post_type);        

     }

    public void show_post(Post_Element post_e) {
        title.text = post_e.post.title;
        date.text = post_e.date_str;
        content.text = post_e.content_str;
        item_info.text = post_e.item_str;

        get_reward_btn.onClick.RemoveAllListeners();
        if (post_e.is_received)
        {
            get_reward_btn.interactable = false;           
        }
        else {
            get_reward_btn.interactable = true;
            get_reward_btn.onClick.AddListener(delegate () {
                if (receive_post(post_e)) {
                    get_reward_btn.onClick.RemoveAllListeners();
                    get_reward_btn.interactable = false;
                    check_image.SetActive(true);
                    sort();
                }                  
            });
        }
        check_image.SetActive(post_e.is_received);

        update_highlight(post_e.highlight_box);
    }

    public bool receive_post(Post_Element post_e) {
        if (post_e.is_received == true) {
            return true;
        }

        var bro = Backend.UPost.ReceivePostItem(post_e.post_type, post_e.post.inDate);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError($"{post_e.post_type.ToString()}의 {post_e.post.inDate}");
            return false;
        }

        post_e.receive();        
        Debug.Log($"{post_e.post_type.ToString()}의 {post_e.post.inDate} 우편수령에 성공했습니다. : " + bro);
        
        return true;

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

        LitJson.JsonData data = bro.GetFlattenJSON()["postList"];

        if (data.Count <= 0)
        {
            Debug.LogWarning("받을 우편이 존재하지 않습니다.");
            return;
        }

        _postList.Clear(); 

        foreach (LitJson.JsonData postListJson in data)
        {
            Post post = new Post();

            post.title = postListJson["title"].ToString();
            post.content = postListJson["content"].ToString();
            post.inDate = postListJson["inDate"].ToString();
            
            if (!postListJson.ContainsKey("receivedDate"))
            {
                post.isCanReceive = true;
            }
            else
            {
                Debug.Log(postListJson["receivedDate"].ToString());
                post.isCanReceive = false;

            }
            _postList.Add(post);
        }
        for (int i = 0; i < _postList.Count; i++)
        {
            Debug.Log($"{i}번 째 우편\n" + _postList[i].ToString());
        }
    }

    #endregion

    #region sort
    public void sort()
    {
        Transform[] m_Children = new Transform[post_list_container.childCount];
        for (int i = 0; i < post_list_container.childCount; i++)
        {
            m_Children[i] = post_list_container.GetChild(i);
        }
        m_Children = m_Children.OrderByDescending(go => get_score(go.GetComponentInChildren<Post_Element>())).ToArray();

        for (int i = 0; i < m_Children.Length; i++)
        {
            m_Children[i].SetSiblingIndex(i);
        }
    }

    public long get_score(Post_Element pe)
    {
        if (pe == null) {
            return 0;
        }
        string target = pe.date_str;
        long score = 0;
        if (pe.is_received) {
            score -= 100000000000000;
        }
        score += long.Parse( DateTime.Parse(target).ToString("yyyyMMddHHmmss")); ;

        return score;
    }

    #endregion
}
