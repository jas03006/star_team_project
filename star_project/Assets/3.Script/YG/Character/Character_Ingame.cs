using UnityEngine;

public class Character_Ingame : MonoBehaviour //인게임에서 사용할 스크립트
{
    private int level;//차트아니고 게임데이터에서 불러옴
    private float duration; //지속 시간
    private double percent; //지속 시간
    //private int give_time; //아이템 지급 주기

    private item_ID special_item;//지급 아이템 ex)게임 시작 시 자석 아이템 한 개를 지급한다.
    private item_ID unique_item;//고유 능력 아이템 ex)자석 아이템 지속 시간 0.5초 증가
    private item_ID unique_item2;//고유 능력 아이템 ex)자석 아이템 지속 시간 0.5초 증가

    public void Setting(int index) //캐릭터 정보 옮겨오기
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

        //인벤토리 생기면 아이템 지급 구현예정
    }
    private void Start()
    {
        //(PlayerPrefs.SetInt 하는 스크립트 -> CharacterManager
        Setting(PlayerPrefs.GetInt("select"));
        PlayerPrefs.DeleteKey("select");
    }
    public void UniqueSkill(Item_game item) //고유능력 - 아이템 지속시간 증가<아이템하고 부딪혔을때 호출>
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
