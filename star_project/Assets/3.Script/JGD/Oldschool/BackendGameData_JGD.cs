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
    //유저 데이터????????????진척도?????캐릭터랑 아이템 리스트 이런거
    public List<string> Friend_UUID_List = new List<string>();                             //친구정보
    public List<int> Character_ID_List = new List<int>();                                  //보유 캐릭터 리스트
    public List<int> Char_Item_ID_List = new List<int>();                                  //캐릭터 아이템 리스트
    public List<int> Adjective_ID_List = new List<int>();                                  //형용사 칭호 리스트
    public List<int> Noun_ID_List = new List<int>();                                       //명사 칭호 리스트
    public List<House_Item_Info_JGD> House_Item_ID_List = new List<House_Item_Info_JGD>(); //하우징 아이템 리스트
    //public List<int> Market_ID_List = new List<int>();                                   //상점 상태 정보/////////////////////////////////////////////////////////////////////
    public List<StageInfo_JGD> StageInfo_List = new List<StageInfo_JGD>();                 //스테이지 별 정보
    //public List<HousingInfo_JGD> Housing_List = new List<HousingInfo_JGD>();              //하우징 정보
    public List<QuestInfo_JGD> QuestInfo_List = new List<QuestInfo_JGD>();                 //퀘스트 별 클리어 여부
    public List<AchievementsInfo_JGD> Achievements_List = new List<AchievementsInfo_JGD>();//업적 별 클리어 여부 

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
            result.AppendLine($"| {itemkey} : {inventory[itemkey]}개");
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
        
        //게임정보 삽입
        if (userData == null)
        {
            Backend.BMember.UpdateNickname(nickname);

            userData = new UserData();

            userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.ark_cylinder));
            userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.airship));
            userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.star_nest));
            userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.chair));
            userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.bed));

            //캐릭터 레벨 정보
            userData.character_info.Add_object(new CharacterObj(Character_ID.Yellow));
            userData.character_info.Add_object(new CharacterObj(Character_ID.Red));
            userData.character_info.Add_object(new CharacterObj(Character_ID.Blue));
            userData.character_info.Add_object(new CharacterObj(Character_ID.Purple));
            userData.character_info.Add_object(new CharacterObj(Character_ID.Green));

            //Memo 정보 - i = 정보 갯수

            for (int i = 0; i < 5; i++)
            {
                userData.memo_info.Add_object();
            }



        }

        Debug.Log("데이터를 초기화 합니다.");
        userData.level = 1;
        userData.info = "친추 환영";

        Debug.Log("뒤끝 업데이트 목록에 데이터 추가");

        Param param = new Param();
        param.Add("level", userData.level);
        param.Add("info", userData.info);
        //param.Add("popularity", userData.popularity);
        param.Add("memo_info", userData.memo_info);
        param.Add("equipment", userData.equipment);///////////////////////////예제코드 추후 삭제?
        param.Add("inventory", userData.inventory);///////////////////////////예제코드 추후 삭제?
        param.Add("Friend_UUID_List", userData.Friend_UUID_List);                       //친구정보
        param.Add("Character_ID_List", userData.Character_ID_List);                     //보유 캐릭터 리스트
        param.Add("Char_Item_ID_List", userData.Char_Item_ID_List);                     //캐릭터 아이템 리스트
        param.Add("Adjective_ID_List", userData.Adjective_ID_List);                     //형용사 칭호 리스트
        param.Add("Noun_ID_List", userData.Noun_ID_List);                               //명사 칭호 리스트
        param.Add("House_Item_ID_List", userData.House_Item_ID_List);                   //하우징 아이템 리스트
        //param.Add("Market_ID_List", userData.Market_ID_List);                           //상점 상태 정보////////////////////////////////////////////////////////////
        param.Add("StageInfo_List", userData.StageInfo_List);                           //스테이지 별 정보
        //param.Add("Housing_List", userData.Housing_List);                               //하우징 정보
        param.Add("QuestInfo_List", userData.QuestInfo_List);                           //퀘스트 별 클리어 여부
        param.Add("Achievements_List", userData.Achievements_List);                     //업적 별 클리어 여부 
        param.Add("Housing_Info", userData.housing_Info);   //하우징 데이터
        param.Add("character_info", userData.character_info);   //펫 데이터

        Debug.Log("게임정보 데이터 삽입을 요청");

        var bro = Backend.GameData.Insert("USER_DATA", param);

        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 삽입에 성공했습니다." + bro);

            //삽입한 게임정보의 고유값
            gameDataRowInDate = bro.GetInDate();
        }
        else
        {
            Debug.LogError("게임정보 데이터 삽입에 실패했습니다." + bro);
        }


    }

    public void GameDataGet()//전체 데이터 불러오기
    {
        //게임정보 불러오기
        Debug.Log("게임 정보 조회 함수를 호출합니다.");
        var bro = Backend.GameData.GetMyData("USER_DATA", new Where());
        if (bro.IsSuccess())
        {
            Debug.Log("게임 정보 조회에 성공했습니다. : " + bro);

            LitJson.JsonData gameDataJson = bro.FlattenRows();    //Json으로 리턴된 데이터

            if (gameDataJson.Count <= 0)
            {
                Debug.LogWarning("데이터가 존재하지 않습니다.");
            }
            else
            {
                gameDataRowInDate = gameDataJson[0]["inDate"].ToString();  //불러온 게임정보의 고유값

                userData = new UserData();
                //Debug.Log("gamer id: "+gameDataJson[0]["gamer_id"].ToString());
                userData.level = int.Parse(gameDataJson[0]["level"].ToString());
                //userData.popularity = int.Parse(gameDataJson[0]["popularity"].ToString());
                userData.info = gameDataJson[0]["info"].ToString();

                userData.housing_Info = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                userData.memo_info = new Memo_info(gameDataJson[0]["memo_info"]);
                //userData.Pet_Info = new PetInfo_YG(gameDataJson[0]["Pet_Info"]);


                //foreach(LitJson.JsonData equip in gameDataJson[0]["Friend_UUID_List"])  //친구정보
                //{
                //    userData.Friend_UUID_List.Add(equip.ToString());
                //}
                //foreach (LitJson.JsonData equip in gameDataJson[0]["Character_ID_List"]) //보유 캐릭터 리스트
                //{
                //    userData.Character_ID_List.Add(int.Parse(equip.ToString()));
                //}
                //foreach (LitJson.JsonData equip in gameDataJson[0]["Char_Item_ID_List"])   //캐릭터 아이템 리스트
                //{
                //    userData.Char_Item_ID_List.Add(int.Parse(equip.ToString()));
                //}
                //foreach (LitJson.JsonData equip in gameDataJson[0]["Adjective_ID_List"])  //형용사 칭호 리스트
                //{
                //    userData.Adjective_ID_List.Add(int.Parse(equip.ToString()));
                //}
                //foreach (LitJson.JsonData equip in gameDataJson[0]["Noun_ID_List"])  //명사 칭호 리스트
                //{
                //    userData.Noun_ID_List.Add(int.Parse(equip.ToString()));
                //}
                foreach (LitJson.JsonData equip in gameDataJson[0]["House_Item_ID_List"])  //하우징 아이템 리스트
                {
                    userData.House_Item_ID_List.Add(new House_Item_Info_JGD(equip));
                }
                ////foreach (string itemKey in gameDataJson[0]["Market_ID_List"]) //상점 상태 정보
                ////{
                ////    userData.inventory.Add(itemKey, int.Parse(gameDataJson[0]["Market_ID_List"][itemKey].ToString()));
                ////}
                //foreach (LitJson.JsonData equip in gameDataJson[0]["StageInfo_JGD"]) //스테이지 별 정보
                //{
                //    userData.StageInfo_List.Add(new StageInfo_JGD(equip));
                //}
                //foreach (LitJson.JsonData equip in gameDataJson[0]["QuestInfo_List"])//퀘스트 별 클리어 여부
                //{
                //    userData.QuestInfo_List.Add(new QuestInfo_JGD(equip));
                //}
                //foreach (LitJson.JsonData equip in gameDataJson[0]["Achievements_List"])//업적 별 클리어 여부 
                //{
                //    userData.Achievements_List.Add(new AchievementsInfo_JGD(equip));
                //}
                //예제 코드
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
            Debug.LogError("게임 정보 조회에 실패했습니다. : " + bro);
        }
    }

    public void Send_level()//개별 데이터 수정
    {
        //게임정보 수정 구현
        if (userData == null)
        {
            Debug.LogError("서버에서 다운받거나  새로 삽입한 데이터가 존재하지 않습니다. Insert 혹은 Get을 통해 데이터를 생성해주세요.");
            return;
        }

        Param param = new Param();
        param.Add("level", userData.level);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(gameDataRowInDate))
        {
            Debug.Log("내 제일 최신 게임정보 데이터 수정을 요청");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }

        else
        {
            Debug.Log($"{gameDataRowInDate}의 게임정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend.UserInDate, param);
        }
        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 수정에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임정보 데이터 수정에 실패했습니다. : " + bro);
        }
    }

    public void GameDataUpdate()//전체 데이터 수정
    {
        //게임정보 수정 구현
        if (userData == null)
        {
            Debug.LogError("서버에서 다운받거나  새로 삽입한 데이터가 존재하지 않습니다. Insert 혹은 Get을 통해 데이터를 생성해주세요.");
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
            Debug.Log("내 제일 최신 게임정보 데이터 수정을 요청");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }
        else
        {
            Debug.Log($"{gameDataRowInDate}의 게임정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend.UserInDate, param);
        }
        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 수정에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임정보 데이터 수정에 실패했습니다. : " + bro);
        }
    }

    public HousingInfo_JGD get_data_by_nickname(string nickname) //개별 데이터 불러오기
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
                Debug.LogWarning("데이터가 존재하지 않습니다.");
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