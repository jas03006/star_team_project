using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Post_Element : MonoBehaviour
{
    string separator = "%%%";
    public Post post;
    public Dictionary<Money, int> item_dic= new Dictionary<Money, int>();
    public string item_str;
    public string content_str = string.Empty;
    [SerializeField] private Button btn;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text item_info;
    [SerializeField] private TMP_Text date;

    public void init(Post post, UnityAction btn_callback) { 
        this.post = post;
        btn.onClick.AddListener(btn_callback);
        parse_reward();
        update_UI();
    }

    public void update_UI() {
        title.text = post.title;
        item_str = string.Empty;
        foreach (Money key in item_dic.Keys) {
            item_str += item_dic[key]+" " +key+ "\n";
        }
        item_info.text = item_str;
        //item_info.text = post.postReward.;
        date.text = post.inDate;
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
}
