using BackEnd;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


//Friend_UUID_List
//Character_ID_List
//Char_Item_ID_List
//Adjective_ID_List
//Noun_ID_List
//House_Item_ID_List
//Market_ID_List
//StageInfo_List
//Housing_List
//QuestInfo_List
//Achievements_List
//
//


public class UserData
{
    int Stage_ID;
    bool is_clear = false;
    bool is_word_clear;
    int top_score;
    //public int popularity = 0;
    public int level = 1;
    public float atk = 3.5f;
    public string info = string.Empty;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public List<string> equipment = new List<string>();
    //���� ������????????????��ô��?????ĳ���Ͷ� ������ ����Ʈ �̷���
    public List<string> Friend_UUID_List = new List<string>();                             //ģ������
    public List<int> Character_ID_List = new List<int>();                                  //���� ĳ���� ����Ʈ
    public List<int> Char_Item_ID_List = new List<int>();                                  //ĳ���� ������ ����Ʈ
    public List<int> Adjective_ID_List = new List<int>();                                  //����� Īȣ ����Ʈ
    public List<int> Noun_ID_List = new List<int>();                                       //��� Īȣ ����Ʈ
    public List<House_Item_Info_JGD> House_Item_ID_List = new List<House_Item_Info_JGD>(); //�Ͽ�¡ ������ ����Ʈ
    //public List<int> Market_ID_List = new List<int>();                                   //���� ���� ����/////////////////////////////////////////////////////////////////////
    public List<StageInfo_JGD> StageInfo_List = new List<StageInfo_JGD>();                 //�������� �� ����
    //public List<HousingInfo_JGD> Housing_List = new List<HousingInfo_JGD>();              //�Ͽ�¡ ����
    public List<QuestInfo_JGD> QuestInfo_List = new List<QuestInfo_JGD>();                 //����Ʈ �� Ŭ���� ����
    public List<AchievementsInfo_JGD> Achievements_List = new List<AchievementsInfo_JGD>();//���� �� Ŭ���� ���� 

    public HousingInfo_JGD housing_Info = new HousingInfo_JGD();
    public CharacterInfo_YG character_info = new CharacterInfo_YG();
    public Memo_info memo_info = new Memo_info();

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"level : {level}");
        result.AppendLine($"atk : {atk}");
        result.AppendLine($"info : {info}");
        // result.AppendLine($"popularity : {popularity}");

        result.AppendLine($"Housing_Info : {housing_Info}");
        result.AppendLine($"CharacterInfo_YG : {character_info}");
        //result.AppendLine($"memo_info : {memo_info}");

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
        result.AppendLine($"| {Friend_UUID_List}");
        foreach (var equip in Friend_UUID_List)
        {
            result.AppendLine($"| {equip}");
        }
        result.AppendLine($"| {Character_ID_List}");
        foreach (var equip in Character_ID_List)
        {
            result.AppendLine($"| {equip}");
        }
        result.AppendLine($"| {Char_Item_ID_List}");
        foreach (var equip in Char_Item_ID_List)
        {
            result.AppendLine($"| {equip}");
        }
        result.AppendLine($"| {Adjective_ID_List}");
        foreach (var equip in Adjective_ID_List)
        {
            result.AppendLine($"| {equip}");
        }
        result.AppendLine($"| {Noun_ID_List}");
        foreach (var equip in Noun_ID_List)
        {
            result.AppendLine($"| {equip}");
        }
        result.AppendLine($"| {House_Item_ID_List}");
        foreach (var equip in House_Item_ID_List)
        {
            result.AppendLine($"| {equip}");
        }
        result.AppendLine($"| {StageInfo_List}");
        foreach (var equip in StageInfo_List)
        {
            result.AppendLine($"| {equip}");
        }
        result.AppendLine($"| {QuestInfo_List}");
        foreach (var equip in QuestInfo_List)
        {
            result.AppendLine($"| {equip}");
        }
        result.AppendLine($"| {Achievements_List}");
        foreach (var equip in Achievements_List)
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

    private string gameDataRowInDate = string.Empty;

    public void GameDataInsert(string nickname = "")
    {
        
        //�������� ����
        if (userData == null)
        {
            Backend.BMember.UpdateNickname(nickname);

            userData = new UserData();

            userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.ark_cylinder));
            userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.airship));
            userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.star_nest));
            userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.chair));
            userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.bed));

            //ĳ���� ���� ����
            userData.character_info.Add_object(new CharacterObj(Character_ID.Yellow));
            userData.character_info.Add_object(new CharacterObj(Character_ID.Red));
            userData.character_info.Add_object(new CharacterObj(Character_ID.Blue));
            userData.character_info.Add_object(new CharacterObj(Character_ID.Purple));
            userData.character_info.Add_object(new CharacterObj(Character_ID.Green));

            //Memo ���� - i = ���� ����

            for (int i = 0; i < 5; i++)
            {
                userData.memo_info.Add_object();
            }



        }

        Debug.Log("�����͸� �ʱ�ȭ �մϴ�.");
        userData.level = 1;
        userData.info = "ģ�� ȯ��";

        Debug.Log("�ڳ� ������Ʈ ��Ͽ� ������ �߰�");

        Param param = new Param();
        param.Add("level", userData.level);
        param.Add("info", userData.info);
        //param.Add("popularity", userData.popularity);
        param.Add("memo_info", userData.memo_info);
        param.Add("equipment", userData.equipment);///////////////////////////�����ڵ� ���� ����?
        param.Add("inventory", userData.inventory);///////////////////////////�����ڵ� ���� ����?
        param.Add("Friend_UUID_List", userData.Friend_UUID_List);                       //ģ������
        param.Add("Character_ID_List", userData.Character_ID_List);                     //���� ĳ���� ����Ʈ
        param.Add("Char_Item_ID_List", userData.Char_Item_ID_List);                     //ĳ���� ������ ����Ʈ
        param.Add("Adjective_ID_List", userData.Adjective_ID_List);                     //����� Īȣ ����Ʈ
        param.Add("Noun_ID_List", userData.Noun_ID_List);                               //��� Īȣ ����Ʈ
        param.Add("House_Item_ID_List", userData.House_Item_ID_List);                   //�Ͽ�¡ ������ ����Ʈ
        //param.Add("Market_ID_List", userData.Market_ID_List);                           //���� ���� ����////////////////////////////////////////////////////////////
        param.Add("StageInfo_List", userData.StageInfo_List);                           //�������� �� ����
        //param.Add("Housing_List", userData.Housing_List);                               //�Ͽ�¡ ����
        param.Add("QuestInfo_List", userData.QuestInfo_List);                           //����Ʈ �� Ŭ���� ����
        param.Add("Achievements_List", userData.Achievements_List);                     //���� �� Ŭ���� ���� 
        param.Add("Housing_Info", userData.housing_Info);   //�Ͽ�¡ ������
        param.Add("character_info", userData.character_info);   //�� ������

        Debug.Log("�������� ������ ������ ��û");

        var bro = Backend.GameData.Insert("USER_DATA", param);

        if (bro.IsSuccess())
        {
            Debug.Log("�������� ������ ���Կ� �����߽��ϴ�." + bro);

            //������ ���������� ������
            gameDataRowInDate = bro.GetInDate();
        }
        else
        {
            Debug.LogError("�������� ������ ���Կ� �����߽��ϴ�." + bro);
        }


    }

    public void GameDataGet()//��ü ������ �ҷ�����
    {
        //�������� �ҷ�����
        Debug.Log("���� ���� ��ȸ �Լ��� ȣ���մϴ�.");
        var bro = Backend.GameData.GetMyData("USER_DATA", new Where());
        if (bro.IsSuccess())
        {
            Debug.Log("���� ���� ��ȸ�� �����߽��ϴ�. : " + bro);

            LitJson.JsonData gameDataJson = bro.FlattenRows();    //Json���� ���ϵ� ������

            if (gameDataJson.Count <= 0)
            {
                Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�.");
            }
            else
            {
                gameDataRowInDate = gameDataJson[0]["inDate"].ToString();  //�ҷ��� ���������� ������

                userData = new UserData();
                //Debug.Log("gamer id: "+gameDataJson[0]["gamer_id"].ToString());
                userData.level = int.Parse(gameDataJson[0]["level"].ToString());
                //userData.popularity = int.Parse(gameDataJson[0]["popularity"].ToString());
                userData.info = gameDataJson[0]["info"].ToString();

                userData.housing_Info = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                userData.memo_info = new Memo_info(gameDataJson[0]["memo_info"]);
                //userData.Pet_Info = new PetInfo_YG(gameDataJson[0]["Pet_Info"]);


                //foreach(LitJson.JsonData equip in gameDataJson[0]["Friend_UUID_List"])  //ģ������
                //{
                //    userData.Friend_UUID_List.Add(equip.ToString());
                //}
                //foreach (LitJson.JsonData equip in gameDataJson[0]["Character_ID_List"]) //���� ĳ���� ����Ʈ
                //{
                //    userData.Character_ID_List.Add(int.Parse(equip.ToString()));
                //}
                //foreach (LitJson.JsonData equip in gameDataJson[0]["Char_Item_ID_List"])   //ĳ���� ������ ����Ʈ
                //{
                //    userData.Char_Item_ID_List.Add(int.Parse(equip.ToString()));
                //}
                //foreach (LitJson.JsonData equip in gameDataJson[0]["Adjective_ID_List"])  //����� Īȣ ����Ʈ
                //{
                //    userData.Adjective_ID_List.Add(int.Parse(equip.ToString()));
                //}
                //foreach (LitJson.JsonData equip in gameDataJson[0]["Noun_ID_List"])  //��� Īȣ ����Ʈ
                //{
                //    userData.Noun_ID_List.Add(int.Parse(equip.ToString()));
                //}
                foreach (LitJson.JsonData equip in gameDataJson[0]["House_Item_ID_List"])  //�Ͽ�¡ ������ ����Ʈ
                {
                    userData.House_Item_ID_List.Add(new House_Item_Info_JGD(equip));
                }
                ////foreach (string itemKey in gameDataJson[0]["Market_ID_List"]) //���� ���� ����
                ////{
                ////    userData.inventory.Add(itemKey, int.Parse(gameDataJson[0]["Market_ID_List"][itemKey].ToString()));
                ////}
                //foreach (LitJson.JsonData equip in gameDataJson[0]["StageInfo_JGD"]) //�������� �� ����
                //{
                //    userData.StageInfo_List.Add(new StageInfo_JGD(equip));
                //}
                //foreach (LitJson.JsonData equip in gameDataJson[0]["QuestInfo_List"])//����Ʈ �� Ŭ���� ����
                //{
                //    userData.QuestInfo_List.Add(new QuestInfo_JGD(equip));
                //}
                //foreach (LitJson.JsonData equip in gameDataJson[0]["Achievements_List"])//���� �� Ŭ���� ���� 
                //{
                //    userData.Achievements_List.Add(new AchievementsInfo_JGD(equip));
                //}
                //���� �ڵ�
                foreach (string itemKey in gameDataJson[0]["inventory"])
                {
                    userData.inventory.Add(itemKey, int.Parse(gameDataJson[0]["inventory"][itemKey].ToString()));
                }

                foreach (LitJson.JsonData equip in gameDataJson[0]["equipment"])
                {
                    userData.equipment.Add(equip.ToString());
                }
                //
            }
            Debug.Log(userData.ToString());

        }
        else
        {
            Debug.LogError("���� ���� ��ȸ�� �����߽��ϴ�. : " + bro);
        }
    }

    public void Send_level()//���� ������ ����
    {
        //�������� ���� ����
        if (userData == null)
        {
            Debug.LogError("�������� �ٿ�ްų�  ���� ������ �����Ͱ� �������� �ʽ��ϴ�. Insert Ȥ�� Get�� ���� �����͸� �������ּ���.");
            return;
        }

        Param param = new Param();
        param.Add("level", userData.level);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(gameDataRowInDate))
        {
            Debug.Log("�� ���� �ֽ� �������� ������ ������ ��û");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }

        else
        {
            Debug.Log($"{gameDataRowInDate}�� �������� ������ ������ ��û�մϴ�.");

            bro = Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend.UserInDate, param);
        }
        if (bro.IsSuccess())
        {
            Debug.Log("�������� ������ ������ �����߽��ϴ�. : " + bro);
        }
        else
        {
            Debug.LogError("�������� ������ ������ �����߽��ϴ�. : " + bro);
        }
    }

    public void GameDataUpdate()//��ü ������ ����
    {
        //�������� ���� ����
        if (userData == null)
        {
            Debug.LogError("�������� �ٿ�ްų�  ���� ������ �����Ͱ� �������� �ʽ��ϴ�. Insert Ȥ�� Get�� ���� �����͸� �������ּ���.");
            return;
        }

        Param param = new Param();
        param.Add("level", userData.level);
        param.Add("info", userData.info);
        // param.Add("popularity", userData.popularity);
        param.Add("Friend_UUID_List", userData.Friend_UUID_List);
        param.Add("Character_ID_List", userData.Character_ID_List);
        param.Add("Char_Item_ID_List", userData.Char_Item_ID_List);
        param.Add("Adjective_ID_List", userData.Adjective_ID_List);
        param.Add("Noun_ID_List", userData.Noun_ID_List);
        param.Add("House_Item_ID_List", userData.House_Item_ID_List);
        //param.Add("Market_ID_List", userData.Market_ID_List);/////////////////////////////////////////////////////////////////
        param.Add("StageInfo_List", userData.StageInfo_List);
        param.Add("Housing_Info", userData.housing_Info);
        param.Add("QuestInfo_List", userData.QuestInfo_List);
        param.Add("Achievements_List", userData.Achievements_List);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(gameDataRowInDate))
        {
            Debug.Log("�� ���� �ֽ� �������� ������ ������ ��û");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }
        else
        {
            Debug.Log($"{gameDataRowInDate}�� �������� ������ ������ ��û�մϴ�.");

            bro = Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend.UserInDate, param);
        }
        if (bro.IsSuccess())
        {
            Debug.Log("�������� ������ ������ �����߽��ϴ�. : " + bro);
        }
        else
        {
            Debug.LogError("�������� ������ ������ �����߽��ϴ�. : " + bro);
        }
    }

    public HousingInfo_JGD get_data_by_nickname(string nickname) //���� ������ �ҷ�����
    {
        string[] select = { "Housing_Info" };
        var n_bro = Backend.Social.GetUserInfoByNickName(nickname);
        string gamer_indate = n_bro.GetReturnValuetoJSON()["row"]["inDate"].ToString();

        BackendReturnObject bro = Backend.PlayerData.GetOtherData("USER_DATA", gamer_indate, select);
        if (bro.IsSuccess())
        {
            LitJson.JsonData gameDataJson = bro.FlattenRows();
            if (gameDataJson.Count <= 0)
            {
                Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�.");
            }
            else
            {
                HousingInfo_JGD housing_info = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                Debug.Log($" level: " + housing_info.level);
                return housing_info;
            }
        }
        else
        {
            Debug.Log("Fail");
        }
        return null;
    }

}