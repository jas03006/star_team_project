using LitJson;

public enum Tutorial_state
{
    catchingstar_chapter = 0,
    catchingstar_play = 1,
    myplanet = 2,
    myplanet_housing = 3,
    myplanet_profile = 4,
    clear = 5
}
public class Tutorial_info
{
    Tutorial_state state;

    public Tutorial_info()
    {
        state = Tutorial_state.catchingstar_chapter;
    }
    public Tutorial_info(JsonData json)
    {
        state = (Tutorial_state)(item_ID)int.Parse(json["state"].ToString());
    }
}
