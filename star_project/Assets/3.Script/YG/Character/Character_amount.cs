using LitJson;
/// <summary>
/// 캐릭터 레벨업 시 필요한 재화의 정보를 저장하는 클래스.
/// 뒤끝DB에서 차트 데이터를 불러와 저장함.
/// </summary>
public class Character_amount
{
    public int level;
    public int gold;
    public int ark;

    public Character_amount(JsonData gameData)
    {
        level = int.Parse(gameData["Level"].ToString());
        gold = int.Parse(gameData["Gold"].ToString());
        ark = int.Parse(gameData["Ark"].ToString());
    }
}


