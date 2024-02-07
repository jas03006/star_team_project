using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
using System;
using UnityEngine.SceneManagement;

public class Inventory_JGD : MonoBehaviour
{
    public List<GameObject> InventoryList = new List<GameObject>();
    public List<TMP_Text> ItemCount = new List<TMP_Text>();

    TMP_Text nono;
    int Itemnum;
    [SerializeField] private GameObject Inven;
    
    private void Awake()
    {
        for (int i = 0; i < BackendGameData_JGD.userData.House_Item_ID_List.Count; i++)
        {
            GameObject go = Instantiate(Inven, transform, false);
            InventoryList.Add(go);
            Itemnum = BackendGameData_JGD.userData.House_Item_ID_List[i].HouseItemCount;
            nono = go.GetComponentInChildren<TMP_Text>();
            nono.text = Itemnum.ToString();
        }
    }
    private void Start()
    {
        Reading();
    }


    public void Reading()
    {
        var HouseItem = Resources.Load<TextAsset>("JSON/HouseItemCount");
        var HouseName = Resources.Load<TextAsset>("JSON/ItemName");
        Debug.Log(HouseItem);
        Debug.Log(HouseName);
        Debug.Log(BackendGameData_JGD.userData.House_Item_ID_List[0].HouseItemCount);
    }

}