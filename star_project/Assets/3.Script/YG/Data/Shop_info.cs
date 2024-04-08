using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Myplanet씬 및 Stage씬에서 캐릭터를 관리하는 스크립트.
/// 이 스크립트는 UI를 통해 캐릭터 정보를 표시하고, 캐릭터 선택 및 업그레이드 등의 기능을 관리함.
/// </summary>
public class Shop_info //userdata
{
    public List<int> index_list = new List<int>();
    public Shop_info()//신규 회원 - 데이터 생성
    {

    }

    public Shop_info(JsonData json)//기존 회원 - 데이터 불러오기
    {
        if (json.IsObject)
        {
            foreach (JsonData data in json["index_list"])
            {
                index_list.Add(int.Parse(data.ToString()));
            }
        }
    }
}


