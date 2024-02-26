using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star_UI
{

    //data
    public Star_info data
    {
        get
        { return data_; }

        set
        {
            data_ = value;

            //UI업데이트
            Update_star();
            Update_is_clear();
            Update_get_housing();
        }
    }
    public Star_info data_;

    //is_clear
    public List<Image> isclear_list = new List<Image>();
    [SerializeField] private Sprite clear_O;
    [SerializeField] private Sprite clear_X;

    //is_star
    private List<Image> star_list = new List<Image>();
    [SerializeField] private Sprite star_O;
    [SerializeField] private Sprite star_X;

    //get_housing
    [SerializeField] Image get_housing;

    public void Update_star()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < data.star)
            {
                star_list[i].sprite = star_O;
            }
            else
            {
                star_list[i].sprite = star_X;
            }
        }
    }

    public void Update_is_clear()
    {
        for (int i = 0; i < isclear_list.Count; i++)
        {
            if (data.is_clear)
            {
                star_list[i].sprite = clear_O;
            }
            else
            {
                star_list[i].sprite = clear_X;
            }
        }
    }

    public void Update_get_housing()
    {
        get_housing.enabled = data.get_housing;
    }

}