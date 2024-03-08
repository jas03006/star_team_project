using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class Achiv_Select_Manager : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject achiv_elem_prefab;

    public List<int> select_list = new List<int>();
    List<Challenge_userdata> achiv_list;
    List<Toggle_Elem> toggle_list = new List<Toggle_Elem>();

    public int max_selection_cnt = 3;
    public TMP_Text selection_cnt_text;
    public void init(List<int> achiv_select_list, List<Challenge_userdata> achiv_list_)
    {
        select_list = new List<int>();
        for (int i =0; i < achiv_select_list.Count; i++) {
            select_list.Add(achiv_select_list[i]);
        }
        achiv_list = achiv_list_;
        create_achiv_elem_all();
        update_UI();
    }
    public void refresh(List<int> achiv_select_list)
    {
        select_list = new List<int>();
        for (int i = 0; i < achiv_select_list.Count; i++)
        {
            select_list.Add(achiv_select_list[i]);
        }
        update_UI();
    }

    public void create_achiv_elem() { 
    
    }

    public void create_achiv_elem_all()
    {
        clear_container();
        toggle_list = new List<Toggle_Elem>();
        for (int i =0; i < achiv_list.Count; i++) {
            if (achiv_list[i].is_clear) {
                GameObject go = Instantiate(achiv_elem_prefab, container);
                Toggle_Elem te = go.GetComponentInChildren<Toggle_Elem>();
                te.value = i;
                te.text.text = BackendChart_JGD.chartData.challenge_list[i].title;
                te.toggle.onValueChanged.AddListener((bool is_select)=> {
                    if (is_select)
                    {
                        on_toggle_select(te.value);
                    }
                    else {
                        on_toggle_deselect(te.value);
                    }
                });
                toggle_list.Add(te);
            }
        }
    }

    public void clear_container() {
        for (int i =0; i < container.childCount; i++) {
            Destroy(container.GetChild(i).gameObject);
        }
    }

    public void update_UI() {
        for (int i =0; i < toggle_list.Count; i++) {
            if (select_list.Contains(toggle_list[i].value)) {
                toggle_list[i].active();
                toggle_list[i].select();
            } else {
                toggle_list[i].deselect();
                if (select_list.Count >= max_selection_cnt)
                {
                    toggle_list[i].deactive();
                }
                else {
                    toggle_list[i].active();
                }
            }
        }
        selection_cnt_text.text = $"선택된 업적: {select_list.Count}개";
    }

    public void on_toggle_select(int value)
    {
        if (select_list.Count >= max_selection_cnt) {
            return;
        }
        if (!select_list.Contains(value)) {
            select_list.Add(value);
        }
        update_UI();
    }
    public void on_toggle_deselect(int value)
    {
        select_list.Remove(value);
        update_UI();
    }
}
