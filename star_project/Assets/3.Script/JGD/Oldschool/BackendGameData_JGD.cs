using BackEnd;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using LitJson;
using System.Data.SqlTypes;
using System.Linq;
using System;


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


public class UserData
{
    int Stage_ID;
    bool is_clear = false;
    bool is_word_clear;
    int top_score;
    
    public int level = 1;
    public int character = 0;
    public int CP = 0; //challenge point
    public float atk = 3.5f;
    public string info = string.Empty;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public List<string> equipment = new List<string>();

    public List<string> Friend_UUID_List = new List<string>();                             //ģ������
    public List<int> Character_ID_List = new List<int>();                                  //���� ĳ���� ����Ʈ
    public List<int> Char_Item_ID_List = new List<int>();                                  //ĳ���� ������ ����Ʈ
    public List<adjective> Adjective_ID_List = new List<adjective>();                                  //����� Īȣ ����Ʈ
    public List<noun> Noun_ID_List = new List<noun>();                                       //��� Īȣ ����Ʈ
    public List<int> emozi_List = new List<int>();                                       //�̸�Ƽ�� ����Ʈ
    public List<int> background_List = new List<int>();                                       //������ ����Ʈ

    public House_Inventory_Info_JGD house_inventory = new House_Inventory_Info_JGD();
    //public List<House_Item_Info_JGD> House_Item_ID_List = new List<House_Item_Info_JGD>(); //�Ͽ�¡ ������ ����Ʈ
    public List<StageInfo_JGD> StageInfo_List = new List<StageInfo_JGD>();                 //�������� �� ����
    public List<QuestInfo_JGD> QuestInfo_List = new List<QuestInfo_JGD>();                 //����Ʈ �� Ŭ���� ����
    public List<int> Achievements_List = new List<int>();//������ ���� ����Ʈ 
    public List<Mission_userdata> mission_Userdatas = new List<Mission_userdata>();
    public List<Challenge_userdata> challenge_Userdatas = new List<Challenge_userdata>();

    public HousingInfo_JGD housing_Info = new HousingInfo_JGD();
    public CharacterInfo_YG character_info = new CharacterInfo_YG();
    public Memo_info memo_info = new Memo_info();
    public Catchingstar_info catchingstar_info = new Catchingstar_info();
    public Shop_info shop_info = new Shop_info();
    public Quest_info_YG quest_Info = new Quest_info_YG();
    public Tutorial_info tutorial_Info = new Tutorial_info();

    //profile
    public int profile_background = 0;
    public int profile_picture = 0;
    public int popularity = 0;
    public adjective title_adjective = adjective.none;
    public noun title_noun = noun.none;
    public string planet_name;
    public string nickname;
    public List<string> popularity_history = new List<string>();

    //money
    public int ark =0;
    public int gold =0;
    public int ruby = 0;

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"level : {level}");
        result.AppendLine($"atk : {atk}");
        result.AppendLine($"info : {info}");
        result.AppendLine($"popularity : {popularity}");

        result.AppendLine($"Housing_Info : {housing_Info}");
        result.AppendLine($"CharacterInfo_YG : {character_info}");
        result.AppendLine($"memo_info : {memo_info}");

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
        /*result.AppendLine($"| {House_Item_ID_List}");
        foreach (var equip in House_Item_ID_List)
        {
            result.AppendLine($"| {equip}");
        }*/
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
        result.AppendLine($"| {mission_Userdatas}");
        foreach (var mission in mission_Userdatas)
        {
            result.AppendLine($"| {mission}");
        }result.AppendLine($"| {challenge_Userdatas}");
        foreach (var mission in challenge_Userdatas)
        {
            result.AppendLine($"| {mission}");
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
    public BackendGameData_JGD() {
        _instance = this;
    }

    public static UserData userData;

    public string gameDataRowInDate = string.Empty;

    public void GameDataInsert(string nickname = "")
    {

        //�������� ����
        if (userData == null)
        {
            Backend.BMember.UpdateNickname(nickname);

            userData = new UserData();

            if (nickname.Contains("888")) {
                userData.tutorial_Info.state = Tutorial_state.clear;
                foreach (housing_itemID hid in Enum.GetValues(typeof(housing_itemID))) {
                    if (hid == housing_itemID.star_nest || hid == housing_itemID.none || (hid >= housing_itemID.chair_old && hid <= housing_itemID.airship)) {
                        continue;
                    }
                    userData.house_inventory.Add(new House_Item_Info_JGD(hid, 1));
                }
            }

            //����
            userData.level = 1;
            userData.character = 0;
            userData.info = "ģ�� ȯ��";
            userData.CP = 0;

            // userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.ark_cylinder));
            //userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.airship));
            userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.star_nest, new Vector2(-1, -1), 0));
            // userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.chair));
            // userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.bed));

            //userData.house_inventory.Add(new House_Item_Info_JGD(housing_itemID.ark_cylinder, 3));
            //userData.house_inventory.Add(new House_Item_Info_JGD(housing_itemID.airship, 1));
            userData.house_inventory.Add(new House_Item_Info_JGD(housing_itemID.star_nest, 1));
            //userData.house_inventory.Add(new House_Item_Info_JGD(housing_itemID.post_box, 1));
            //userData.house_inventory.Add(new House_Item_Info_JGD(housing_itemID.chair, 1));
            //userData.house_inventory.Add(new House_Item_Info_JGD(housing_itemID.bed, 1));

            //ĳ���� ���� ����
            userData.character_info.Add_object(new CharacterObj(Character_ID.Yellow, 1));
            userData.character_info.Add_object(new CharacterObj(Character_ID.Red, 0));
            userData.character_info.Add_object(new CharacterObj(Character_ID.Blue, 0));
            userData.character_info.Add_object(new CharacterObj(Character_ID.Purple, 0));
            userData.character_info.Add_object(new CharacterObj(Character_ID.Green, 0));

            int[] except_emozi = {6, 10, 12,14,15};
            for (int i =0; i < 24; i++) {
                if (except_emozi.Contains(i)) {
                    continue;
                }
                userData.emozi_List.Add(i);
            }
            for (int i = 0; i < 13; i++)
            {
                userData.background_List.Add(i);
            }

            //ĳĪ��Ÿ �������� ����
            userData.catchingstar_info = new Catchingstar_info();

            //Memo ���� - i = ���� ����
            userData.memo_info = new Memo_info();

            //Īȣ ����
            userData.title_adjective = adjective.none;
            userData.title_noun = noun.none;
            userData.Adjective_ID_List.Add(adjective.none);
            userData.Noun_ID_List.Add(noun.none);
            userData.planet_name = nickname;
            userData.nickname = nickname;

            //�� ����
            userData.gold = 1000;
            userData.ruby = 1000;
            userData.ark = 1000;

            //�̼� ����(��)
            for (int i = 0; i < 9; i++)
            {
                userData.mission_Userdatas.Add(new Mission_userdata());
            }

            //���� ����(��)
            for (int i = 0; i < 27; i++)
            {
                userData.challenge_Userdatas.Add(new Challenge_userdata());
            }

            //����Ʈ ����(��)
            userData.quest_Info.Init_info_data();

            //���� ����
            userData.shop_info.Insert_data();

        }

        Debug.Log("�����͸� �ʱ�ȭ �մϴ�.");
        Debug.Log("�ڳ� ������Ʈ ��Ͽ� ������ �߰�");

        Param param = new Param();
        param.Add("level", userData.level);
        param.Add("nickname", userData.nickname);
        param.Add("character", userData.character);
        param.Add("info", userData.info);
        param.Add("CP",userData.CP);
        param.Add("memo_info", userData.memo_info);
        param.Add("equipment", userData.equipment);///////////////////////////�����ڵ� ���� ����?
        param.Add("inventory", userData.inventory);///////////////////////////�����ڵ� ���� ����?
        param.Add("Friend_UUID_List", userData.Friend_UUID_List);                       //ģ������
        param.Add("Character_ID_List", userData.Character_ID_List);                     //���� ĳ���� ����Ʈ
        param.Add("Char_Item_ID_List", userData.Char_Item_ID_List);                     //ĳ���� ������ ����Ʈ
        param.Add("Adjective_ID_List", userData.Adjective_ID_List);                     //����� Īȣ ����Ʈ
        param.Add("Noun_ID_List", userData.Noun_ID_List);                               //��� Īȣ ����Ʈ
        param.Add("emozi_List", userData.emozi_List);                               //�̸��� ����Ʈ
        param.Add("background_List", userData.background_List);                               //��� ���� ����Ʈ
        param.Add("house_inventory", userData.house_inventory);                               //�Ͽ�¡ ������ ����Ʈ
        //param.Add("House_Item_ID_List", userData.House_Item_ID_List);                   //�Ͽ�¡ ������ ����Ʈ
        //param.Add("Market_ID_List", userData.Market_ID_List);                           //���� ���� ����
        param.Add("StageInfo_List", userData.StageInfo_List);                           //�������� �� ����
        //param.Add("Housing_List", userData.Housing_List);                               //�Ͽ�¡ ����
        param.Add("QuestInfo_List", userData.QuestInfo_List);                           //����Ʈ �� Ŭ���� ����
        param.Add("Achievements_List", userData.Achievements_List);                     //���� �� Ŭ���� ���� 
        //param.Add("mission_Userdatas", userData.mission_Userdatas);                     
        param.Add("challenge_Userdatas", userData.challenge_Userdatas);                              

        param.Add("Housing_Info", userData.housing_Info);   //�Ͽ�¡ ������
        param.Add("character_info", userData.character_info);   //ĳ���� ������
        param.Add("catchingstar_info", userData.catchingstar_info);
        param.Add("shop_info", userData.shop_info);
        param.Add("quest_Info", userData.quest_Info);
        param.Add("tutorial_Info", userData.tutorial_Info);

        param.Add("profile_background", userData.profile_background);
        param.Add("profile_picture", userData.profile_picture);
        param.Add("popularity", userData.popularity);
        param.Add("title_adjective", userData.title_adjective);
        param.Add("title_noun", userData.title_noun);
        param.Add("planet_name", userData.planet_name);
        param.Add("popularity_history", userData.popularity_history);

        param.Add("ark", userData.ark);
        param.Add("gold", userData.gold);
        param.Add("ruby", userData.ruby);
   

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
                //userData.level = int.Parse(gameDataJson[0]["level"].ToString());
                
                userData.info = gameDataJson[0]["info"].ToString();
                userData.nickname = gameDataJson[0]["nickname"].ToString();
                userData.CP = int.Parse(gameDataJson[0]["CP"].ToString());
                userData.character = int.Parse(gameDataJson[0]["character"].ToString());
                userData.housing_Info = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                userData.memo_info = new Memo_info(gameDataJson[0]["memo_info"]);
                userData.catchingstar_info = new Catchingstar_info(gameDataJson[0]["catchingstar_info"]);
                userData.character_info = new CharacterInfo_YG(gameDataJson[0]["character_info"]);
                userData.shop_info = new Shop_info(gameDataJson[0]["shop_info"]);
                userData.quest_Info = new Quest_info_YG(gameDataJson[0]["quest_Info"]);
                userData.tutorial_Info = new Tutorial_info(gameDataJson[0]["tutorial_Info"]);

                //foreach (JsonData mission in gameDataJson[0]["mission_Userdatas"]) 
                //{
                //    userData.mission_Userdatas.Add(new Mission_userdata(mission));
                //}                
                foreach (JsonData mission in gameDataJson[0]["challenge_Userdatas"]) 
                {
                    userData.challenge_Userdatas.Add(new Challenge_userdata(mission));
                }                
                
                foreach (JsonData data in gameDataJson[0]["Achievements_List"]) 
                {
                    userData.Achievements_List.Add(int.Parse(data.ToString()));
                }



                userData.house_inventory = new House_Inventory_Info_JGD(gameDataJson[0]["house_inventory"]);
                /* foreach (JsonData equip in gameDataJson[0]["House_Item_ID_List"])  //�Ͽ�¡ ������ ����Ʈ
                 {
                     userData.House_Item_ID_List.Add(new House_Item_Info_JGD(equip));
                 }*/

                foreach (LitJson.JsonData equip in gameDataJson[0]["Adjective_ID_List"])  //����� Īȣ ����Ʈ
                {
                    userData.Adjective_ID_List.Add((adjective)int.Parse(equip.ToString()));
                }
                foreach (LitJson.JsonData equip in gameDataJson[0]["Noun_ID_List"])  //��� Īȣ ����Ʈ
                {
                    userData.Noun_ID_List.Add((noun)int.Parse(equip.ToString()));
                }
                foreach (LitJson.JsonData equip in gameDataJson[0]["emozi_List"]) 
                {
                    userData.emozi_List.Add(int.Parse(equip.ToString()));
                }
                foreach (LitJson.JsonData equip in gameDataJson[0]["background_List"])  
                {
                    userData.background_List.Add(int.Parse(equip.ToString()));
                }

                #region �ּ�
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
                #endregion
                //���� �ڵ�
                foreach (string itemKey in gameDataJson[0]["inventory"])
                {
                    userData.inventory.Add(itemKey, int.Parse(gameDataJson[0]["inventory"][itemKey].ToString()));
                }

                foreach (LitJson.JsonData equip in gameDataJson[0]["equipment"])
                {
                    userData.equipment.Add(equip.ToString());
                }

                userData.profile_background = int.Parse(gameDataJson[0]["profile_background"].ToString());
                userData.profile_picture = int.Parse(gameDataJson[0]["profile_picture"].ToString());
                userData.popularity = int.Parse(gameDataJson[0]["popularity"].ToString());
                userData.title_adjective = (adjective)int.Parse(gameDataJson[0]["title_adjective"].ToString());
                userData.title_noun = (noun)int.Parse(gameDataJson[0]["title_noun"].ToString());
                userData.planet_name = gameDataJson[0]["planet_name"].ToString();

                foreach (LitJson.JsonData equip in gameDataJson[0]["popularity_history"])
                {
                    userData.popularity_history.Add(equip.ToString());
                }

                userData.ark = int.Parse(gameDataJson[0]["ark"].ToString());
                userData.gold = int.Parse(gameDataJson[0]["gold"].ToString());
                userData.ruby = int.Parse(gameDataJson[0]["ruby"].ToString());
               
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
        param.Add("character", userData.character);
        param.Add("nickname", userData.nickname);
        param.Add("CP", userData.CP);
        param.Add("info", userData.info);
        param.Add("Friend_UUID_List", userData.Friend_UUID_List);
        param.Add("Character_ID_List", userData.Character_ID_List);
        param.Add("Char_Item_ID_List", userData.Char_Item_ID_List);
        param.Add("Adjective_ID_List", userData.Adjective_ID_List);
        param.Add("Noun_ID_List", userData.Noun_ID_List);
        param.Add("emozi_List", userData.emozi_List);                               //�̸��� ����Ʈ
        param.Add("background_List", userData.background_List);                               //��� ���� ����Ʈ
        param.Add("tutorial_Info", userData.tutorial_Info);                               //��� ���� ����Ʈ

        param.Add("house_inventory", userData.house_inventory);
        //param.Add("House_Item_ID_List", userData.House_Item_ID_List);
        //param.Add("Market_ID_List", userData.Market_ID_List);/////////////////////////////////////////////////////////////////
        param.Add("StageInfo_List", userData.StageInfo_List);
        param.Add("quest_Info", userData.quest_Info);
        param.Add("Housing_Info", userData.housing_Info);
        param.Add("shop_info", userData.shop_info);
        param.Add("QuestInfo_List", userData.QuestInfo_List);
        param.Add("Achievements_List", userData.Achievements_List);

        param.Add("profile_background", userData.profile_background);
        param.Add("profile_picture", userData.profile_picture);
        param.Add("popularity", userData.popularity);
        param.Add("title_adjective", userData.title_adjective);
        param.Add("title_noun", userData.title_noun);
        param.Add("planet_name", userData.planet_name);
        param.Add("popularity_history", userData.popularity_history);

        param.Add("ark", userData.ark);
        param.Add("gold", userData.gold);
        param.Add("ruby", userData.ruby);

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

    public void GameDataUpdate(string[] select)//��ü ������ ����
    {
        //�������� ���� ����
        if (userData == null)
        {
            Debug.LogError("�������� �ٿ�ްų�  ���� ������ �����Ͱ� �������� �ʽ��ϴ�. Insert Ȥ�� Get�� ���� �����͸� �������ּ���.");
            return;
        }
        
        Param param = new Param();

        for (int i = 0; i < select.Length; i++)
        {
            switch (select[i])
            {
                case "Friend_UUID_List":
                    param.Add(select[i], userData.Friend_UUID_List);
                    break;
                case "inventory":
                    param.Add(select[i], userData.inventory);
                    break;
                case "Achievements_List":
                    param.Add(select[i], userData.Achievements_List);
                    break;
                case "QuestInfo_List":
                    param.Add(select[i], userData.QuestInfo_List);
                    break;
                case "Pet_Info":
                    param.Add(select[i], userData.character_info);
                    break;
                case "Housing_Info":
                    param.Add(select[i], userData.housing_Info);
                    break;
                case "Char_Item_ID_List":
                    param.Add(select[i], userData.Char_Item_ID_List);
                    break;
                case "StageInfo_List":
                    param.Add(select[i], userData.StageInfo_List);
                    break;
                case "house_inventory":
                    param.Add(select[i], userData.house_inventory);
                    break;
                case "info":
                    param.Add(select[i], userData.info);
                    break;
                case "memo_info":
                    param.Add(select[i], userData.memo_info);
                    break;
                case "Adjective_ID_List":
                    param.Add(select[i], userData.Adjective_ID_List);
                    break;
                case "level":
                    param.Add(select[i], userData.level);
                    break;
                case "Noun_ID_List":
                    param.Add(select[i], userData.Noun_ID_List);
                    break;
                case "equipment":
                    param.Add(select[i], userData.equipment);
                    break;
                case "profile_background":
                    param.Add(select[i], userData.profile_background);
                    break;
                case "profile_picture":
                    param.Add(select[i], userData.profile_picture);
                    break;
                case "popularity":
                    param.Add(select[i], userData.popularity);
                    break;
                case "title_adjective":
                    param.Add(select[i], userData.title_adjective);
                    break;
                case "title_noun":
                    param.Add(select[i], userData.title_noun);
                    break;
                case "planet_name":
                    param.Add(select[i], userData.planet_name);
                    break;
                case "popularity_history":
                    param.Add(select[i], userData.popularity_history);
                    break;
                case "ark":
                    param.Add(select[i], userData.ark);
                    break;
                case "gold":
                    param.Add(select[i], userData.gold);
                    break;
                case "ruby":
                    param.Add(select[i], userData.ruby);
                    break;
                default:
                    break;
            }
        }

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
               // Debug.Log($" level: " + housing_info.level);
                return housing_info;
            }
        }
        else
        {
            Debug.Log("Fail");
        }
        return null;
    }

    public UserData get_userdata_by_nickname(string nickname, string[] select)
    {
        //select = { "Housing_Info" };
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
                UserData user_data = new UserData();
                for (int i = 0; i < select.Length; i++)
                {
                    switch (select[i])
                    {
                        case "Friend_UUID_List":
                            //user_data.Friend_UUID_List = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "inventory":
                            //user_data.inventory = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "Achievements_List":
                            foreach (LitJson.JsonData equip in gameDataJson[0]["Achievements_List"])
                            {
                                user_data.Achievements_List.Add(int.Parse(equip.ToString()));
                            }
                            break;
                        case "QuestInfo_List":
                            //user_data.QuestInfo_List = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "Pet_Info":
                            //user_data.Pet_Info = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "Housing_Info":
                            user_data.housing_Info = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "Char_Item_ID_List":
                            // user_data.Char_Item_ID_List = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "StageInfo_List":
                            // user_data.StageInfo_List = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "house_inventory":
                            user_data.house_inventory = new House_Inventory_Info_JGD(gameDataJson[0]["house_inventory"]);
                            break;
                        case "info":
                            user_data.info = gameDataJson[0]["info"].ToString();
                            break;
                        case "memo_info":
                            user_data.memo_info = new Memo_info(gameDataJson[0]["memo_info"]);
                            break;
                        case "Adjective_ID_List":
                            foreach (LitJson.JsonData equip in gameDataJson[0]["Adjective_ID_List"])  //����� Īȣ ����Ʈ
                            {
                                user_data.Adjective_ID_List.Add((adjective)int.Parse(equip.ToString()));
                            }                            
                            break;
                        case "level":
                            user_data.level = int.Parse(gameDataJson[0]["level"].ToString());
                            break;
                        case "Noun_ID_List":
                            foreach (LitJson.JsonData equip in gameDataJson[0]["Noun_ID_List"])  //��� Īȣ ����Ʈ
                            {
                                user_data.Noun_ID_List.Add((noun)int.Parse(equip.ToString()));
                            }
                            break;
                        case "equipment":
                            //user_data.equipment = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "profile_background":
                            user_data.profile_background = int.Parse(gameDataJson[0]["profile_background"].ToString());
                            break;
                        case "profile_picture":
                            user_data.profile_picture = int.Parse(gameDataJson[0]["profile_picture"].ToString());
                            break;
                        case "popularity":
                            user_data.popularity = int.Parse(gameDataJson[0]["popularity"].ToString());
                            break;
                        case "title_adjective":
                            user_data.title_adjective = (adjective)int.Parse(gameDataJson[0]["title_adjective"].ToString());
                            break;
                        case "title_noun":
                            user_data.title_noun = (noun)int.Parse(gameDataJson[0]["title_noun"].ToString());
                            break;
                        case "planet_name":
                            user_data.planet_name = gameDataJson[0]["planet_name"].ToString();
                            break;
                        case "nickname":
                            user_data.nickname = gameDataJson[0]["nickname"].ToString();
                            break;
                        case "ark":
                            user_data.ark = int.Parse(gameDataJson[0]["ark"].ToString());
                            break;
                        case "gold":
                            user_data.gold = int.Parse(gameDataJson[0]["gold"].ToString());
                            break;
                        case "ruby":
                            user_data.ruby = int.Parse(gameDataJson[0]["ruby"].ToString());
                            break;
                        case "catchingstar_info":
                            user_data.catchingstar_info = new Catchingstar_info(gameDataJson[0]["catchingstar_info"]);
                            break;
                        case "emozi_List":
                            foreach (LitJson.JsonData equip in gameDataJson[0]["emozi_List"]) 
                            {
                                user_data.emozi_List.Add(int.Parse(equip.ToString()));
                            }
                            break;
                        case "background_List":
                            foreach (LitJson.JsonData equip in gameDataJson[0]["background_List"])  
                            {
                                user_data.background_List.Add(int.Parse(equip.ToString()));
                            }
                            break;
                        case "challenge_Userdatas":
                            foreach (JsonData mission in gameDataJson[0]["challenge_Userdatas"])
                            {
                                user_data.challenge_Userdatas.Add(new Challenge_userdata(mission));
                            }
                            break;
                        case "popularity_history":
                            foreach (LitJson.JsonData equip in gameDataJson[0]["popularity_history"])
                            {
                                user_data.popularity_history.Add(equip.ToString());
                            }
                            break;
                        case "character":
                            user_data.character =int.Parse( gameDataJson[0]["character"].ToString());
                            break;
                        default:
                            break;
                    }
                }
                return user_data;
            }
        }
        
        else
        {
            Debug.Log("Fail");
        }
        return null;
    }

    public void update_userdata_by_nickname(string nickname, string[] select, UserData user_data)
    {
        if (user_data == null)
        {
            return;
        }
        string[] select_temp = { "info" };
        var n_bro = Backend.Social.GetUserInfoByNickName(nickname);
        string gamer_indate = n_bro.GetReturnValuetoJSON()["row"]["inDate"].ToString();

        BackendReturnObject bro = Backend.PlayerData.GetOtherData("USER_DATA", gamer_indate, select_temp);

        if (bro.IsSuccess())
        {
            string gameDataRowInDate_ = bro.GetInDate();
            Param param = new Param();

            for (int i = 0; i < select.Length; i++)
            {
                switch (select[i])
                {
                    case "Friend_UUID_List":
                        param.Add(select[i], user_data.Friend_UUID_List);
                        break;
                    case "inventory":
                        param.Add(select[i], user_data.inventory);
                        break;
                    case "Achievements_List":
                        param.Add(select[i], user_data.Achievements_List);
                        break;
                    case "QuestInfo_List":
                        param.Add(select[i], user_data.QuestInfo_List);
                        break;
                    case "Pet_Info":
                        param.Add(select[i], user_data.character_info);
                        break;
                    case "Housing_Info":
                        param.Add(select[i], user_data.housing_Info);
                        break;
                    case "Char_Item_ID_List":
                        param.Add(select[i], user_data.Char_Item_ID_List);
                        break;
                    case "StageInfo_List":
                        param.Add(select[i], user_data.StageInfo_List);
                        break;
                    case "house_inventory":
                        param.Add(select[i], user_data.house_inventory);
                        break;
                    case "info":
                        param.Add(select[i], user_data.info);
                        break;
                    case "memo_info":
                        param.Add(select[i], user_data.memo_info);
                        break;
                    case "Adjective_ID_List":
                        param.Add(select[i], user_data.Adjective_ID_List);
                        break;
                    case "level":
                        param.Add(select[i], user_data.level);
                        break;
                    case "Noun_ID_List":
                        param.Add(select[i], user_data.Noun_ID_List);
                        break;
                    case "equipment":
                        param.Add(select[i], user_data.equipment);
                        break;
                    case "profile_background":
                        param.Add(select[i], user_data.profile_background);                        
                        break;
                    case "profile_picture":
                        param.Add(select[i], user_data.profile_picture);                        
                        break;
                    case "popularity":
                        param.Add(select[i], user_data.popularity);                       
                        break;
                    case "title_adjective":
                        param.Add(select[i], user_data.title_adjective);                        
                        break;
                    case "title_noun":
                        param.Add(select[i], user_data.title_noun);                        
                        break;
                    case "planet_name":
                        param.Add(select[i], user_data.planet_name);                        
                        break;
                    case "nickname":
                        param.Add(select[i], user_data.nickname);
                        break;
                    case "ark":
                        param.Add(select[i], user_data.ark);                        
                        break;
                    case "gold":
                        param.Add(select[i], user_data.gold);                        
                        break;
                    case "ruby":
                        param.Add(select[i], user_data.ruby);
                        break;
                    case "popularity_history":
                        param.Add(select[i], user_data.popularity_history);
                        break;
                    default:
                        break;
                }
            }
            var bro_ = Backend.PlayerData.UpdateOtherData("USER_DATA", gameDataRowInDate_, gamer_indate, param);
            if (bro_.IsSuccess())
            {
                return;
            }
            else
            {
                Debug.Log("Fail");
            }
        }
        else
        {
            Debug.Log("Fail");
        }
        return;
    }
}