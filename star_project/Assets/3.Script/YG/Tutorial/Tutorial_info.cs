using LitJson;
/// <summary>
/// 튜토리얼 진행 여부를 저장하는 클래스
/// </summary>
public class Tutorial_info
{
    public Tutorial_state state;

    public Tutorial_info()//신규 회원 - 데이터 생성
    {
        state = Tutorial_state.catchingstar_chapter;
    }
    public Tutorial_info(JsonData json)//기존 회원 - 데이터 불러오기
    {
        state = (Tutorial_state)int.Parse(json["state"].ToString());
    }
}
