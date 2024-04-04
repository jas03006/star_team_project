using UnityEngine;

public class Character_Ingame : MonoBehaviour //�ΰ��ӿ��� ����� ��ũ��Ʈ
{
    private int level;//��Ʈ�ƴϰ� ���ӵ����Ϳ��� �ҷ���
    private float duration; //���� �ð�
    private double percent; //���� �ð�
    //private int give_time; //������ ���� �ֱ�

    private item_ID special_item;//���� ������ ex)���� ���� �� �ڼ� ������ �� ���� �����Ѵ�.
    private item_ID unique_item;//���� �ɷ� ������ ex)�ڼ� ������ ���� �ð� 0.5�� ����
    private item_ID unique_item2;//���� �ɷ� ������ ex)�ڼ� ������ ���� �ð� 0.5�� ����

    public void Setting(int index) //ĳ���� ���� �Űܿ���
    {
        Character character = BackendChart_JGD.chartData.character_list[index];
        level = character.curlevel;

        if (index == (int)Character_ID.Green)
        {
            percent = character.percent;
            unique_item2 = character.unique_item2;
        }
        else
        {
            duration = character.duration;
            unique_item2 = character.unique_item;
        }

        special_item = character.special_item;
        unique_item = character.unique_item;

        //�κ��丮 ����� ������ ���� ��������
    }
    private void Start()
    {
        //(PlayerPrefs.SetInt �ϴ� ��ũ��Ʈ -> CharacterManager
        Setting(PlayerPrefs.GetInt("select"));
        PlayerPrefs.DeleteKey("select");
    }
    public void UniqueSkill(Item_game item) //�����ɷ� - ������ ���ӽð� ����<�������ϰ� �ε������� ȣ��>
    {
        if (item.id == unique_item || item.id == unique_item2)
        {
            if (item is Heart)
            {
                item.percent += percent;
            }
            else
            {
                item.duration += duration;
            }
        }
    }
}
