using LitJson;

public class Tutorial_info
{
    public Tutorial_state state;

    public Tutorial_info()
    {
        state = Tutorial_state.catchingstar_chapter;
    }
    public Tutorial_info(JsonData json)
    {
        state = (Tutorial_state)(item_ID)int.Parse(json["state"].ToString());
    }
}
