using LitJson;
/// <summary>
/// Ʃ�丮�� ���� ���θ� �����ϴ� Ŭ����
/// </summary>
public class Tutorial_info
{
    public Tutorial_state state;

    public Tutorial_info()//�ű� ȸ�� - ������ ����
    {
        state = Tutorial_state.catchingstar_chapter;
    }
    public Tutorial_info(JsonData json)//���� ȸ�� - ������ �ҷ�����
    {
        state = (Tutorial_state)int.Parse(json["state"].ToString());
    }
}
