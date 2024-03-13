using BackEnd;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Post_Element : MonoBehaviour
{
    string separator = "%^";
    public Post post;
    public PostType post_type;
    public Dictionary<Money, int> item_dic= new Dictionary<Money, int>();
    public string item_str;
    public string date_str;
    public string content_str = string.Empty;

    public bool is_received = false;

    [SerializeField] private Button btn;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text item_info;
    [SerializeField] private GameObject item_info_image;
    [SerializeField] private GameObject item_info_check_image;
    [SerializeField] private TMP_Text date;

    public void init(Post post, UnityAction btn_callback, PostType post_type_) { 
        this.post = post;
        post_type = post_type_;
        is_received = !post.isCanReceive;
        btn.onClick.AddListener(btn_callback);
        parse_reward();
        update_UI();
    }

    public void update_UI() {
        title.text = post.title;
        item_str = string.Empty;
        foreach (Money key in item_dic.Keys) {
            item_str += item_dic[key];
        }
        item_info.text = item_str;

        if (item_dic.Keys.Count == 0)
        {
            item_info_image.SetActive(false);
        }
        else {
            item_info_image.SetActive(true);
        }

        item_info_check_image.SetActive(is_received);

        if (is_received)
        {
            item_info.text += "È¹µæ¿Ï·á";
        }
        //item_info.text = post.postReward.;
        date_str = post.inDate.Split(".")[0].Replace("T"," ");
        date.text = date_str;
    }
    public void parse_reward() {
        string[] arr = post.content.Split(separator);
        content_str = arr[0];
        if (arr.Length >1) {
            string[] item_info_arr = arr[1].Split("|");
            for (int i =0; i < item_info_arr.Length; i++) {
                string[] item_info = item_info_arr[i].Split(":");
                item_dic[(Money)int.Parse(item_info[0])] = int.Parse(item_info[1]);
            }
        }
    }

    public void receive(){
        if (is_received) {
            return;
        }
        foreach (Money key in item_dic.Keys) {
            MoneyManager.instance.Get_Money(key, item_dic[key]);
        }      

        is_received = true;
        item_info_check_image.SetActive(true);
    }
}
