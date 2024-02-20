using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Post_Box_UI : MonoBehaviour
{
    [SerializeField] private GameObject main_UI;
    [SerializeField] private GameObject post_element_prefab;
    public void hide_UI() {
        main_UI.SetActive(false);
    }



    #region post
   /* public void clear_memos_UI()
    {
        //memo_container
        int len = memo_container.childCount;
        for (int i = 0; i < len; i++)
        {
            Destroy(memo_container.GetChild(i).gameObject);
        }
    }

    public void load_memos_UI()
    {
        if (user_data == null || user_data.memo_info == null)
        {
            Debug.Log("user data memo is null!");
            return;
        }
        clear_memos_UI();
        for (int i = 0; i < user_data.memo_info.memo_list.Count; i++)
        {
            create_memo(user_data.memo_info.memo_list[i].UUID, user_data.memo_info.memo_list[i].content);
        }

    }

    public void add_memo()
    {
        if (user_data == null || user_data.memo_info == null)
        {
            Debug.Log("user data memo is null!");
            return;
        }
        if (memo_input.text.Equals(string.Empty))
        {
            return;
        }
        Memo memo = new Memo();
        memo.Change(TCP_Client_Manager.instance.my_player.object_id, memo_input.text);
        user_data.memo_info.Add_object(memo);
        memo_input.text = "";
        memo_input.Select();

        create_memo(memo.UUID, memo.content);

        string[] select = { "memo_info" };
        BackendGameData_JGD.Instance.update_userdata_by_nickname(TCP_Client_Manager.instance.now_room_id, select, user_data);
        if (TCP_Client_Manager.instance.now_room_id == TCP_Client_Manager.instance.my_player.object_id)
        {
            BackendGameData_JGD.userData.memo_info = user_data.memo_info;
        }
    }
    public void create_memo(string uuid_, string content_)
    {

        if (content_.Equals(string.Empty))
        {
            return;
        }
        GameObject memo_go = Instantiate(memo_prefab);
        memo_go.transform.SetParent(memo_container);
        memo_go.transform.localScale = Vector3.one;
        TMP_Text[] text_arr = memo_go.GetComponentsInChildren<TMP_Text>();
        text_arr[0].text = uuid_;
        text_arr[1].text = content_;
    }*/

    #endregion
}
