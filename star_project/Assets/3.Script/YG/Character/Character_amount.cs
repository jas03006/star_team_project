using LitJson;

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


