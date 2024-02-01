using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using BackEnd;

public class UserData
{
    public int level = 1;
    public float atk = 3.5f;
    public string info = string.Empty;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public List<string> equipment = new List<string>();

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"level : {level}");
        result.AppendLine($"atk : {atk}");
        result.AppendLine($"info : {info}");

        result.AppendLine($"inventory");
        foreach (var itemkey in inventory.Keys)
        {
            result.AppendLine($"| {itemkey} : {inventory[itemkey]}��");
        }
        result.AppendLine($"| {equipment}");
        foreach (var equip in equipment)
        {
            result.AppendLine($"| {equip}");
        }
        return result.ToString();
    }
}



public class BackendGameData_JGD : MonoBehaviour
{
    private static BackendGameData_JGD _instance = null;

    public static BackendGameData_JGD Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendGameData_JGD();
            }
            return _instance;   
        }
    }

    public static UserData userData;

    private string gameDataRowInData = string.Empty;

    public void GameDataInsert()
    {
        //�������� ����
        
    }
    public void GameDataGet()
    {
        //�������� �ҷ�����
    }
    public void LevelUp()
    {
        //�������� ���� ����
    }
    public void GameDataUpdate()
    {
        //�������� ���� ����
    }

    
}
