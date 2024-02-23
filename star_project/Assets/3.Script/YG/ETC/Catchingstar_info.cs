using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catchingstar_info
{
    public List<Galaxy_info> galaxy_Info_list = new List<Galaxy_info>();

    public Catchingstar_info()
    {
        for (int i = 0; i < 5; i++)
        {
            galaxy_Info_list.Add(new Galaxy_info());
        }
    }

    public Catchingstar_info(JsonData jsonData)
    {
        foreach (JsonData json in jsonData["galaxy_Info_list"])
        {
            galaxy_Info_list.Add(new Galaxy_info(json));
        }
    }
}

public enum Galaxy_state
{
    incomplete = 0,
    can_reward,
    complete
}

public class Galaxy_info
{
    public List<star_info> star_Info_list = new List<star_info>();
    public List<Galaxy_state> galaxy_state = new List<Galaxy_state>();

    public Galaxy_info()
    {
        //star_Info_list
        for (int i = 0; i < 5; i++)
        {
            star_Info_list.Add(new star_info());
        }

        //galaxy_state
        for (int i = 0; i < 3; i++)
        {
            galaxy_state.Add(Galaxy_state.incomplete);
        }
    }

    public Galaxy_info(JsonData jsonData)
    {
        //star_Info_list
        foreach (JsonData json in jsonData["star_Info_list"])
        {
            star_Info_list.Add(new star_info(json));
        }

        //galaxy_state
        foreach (JsonData json in jsonData["galaxy_state"])
        {
            galaxy_state.Add((Galaxy_state)int.Parse(json.ToString()));
        }
    }
}

public class star_info
{
    public bool is_clear;
    public int star;
    public bool get_housing;

    public star_info()
    {
        is_clear = false;
        star = 0;
        get_housing = false;
    }

    public star_info(JsonData jsonData)
    {
        is_clear = bool.Parse(jsonData["is_clear"].ToString());
        star = int.Parse(jsonData["star"].ToString());
        get_housing = bool.Parse(jsonData["get_housing"].ToString());
    }
}
