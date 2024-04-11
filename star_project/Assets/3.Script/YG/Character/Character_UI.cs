using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Myplanet씬 및 Stage씬에서 캐릭터를 관리하는 스크립트.
/// 이 스크립트는 UI를 통해 캐릭터 정보를 표시하고, 캐릭터 선택 및 업그레이드 등의 기능을 관리함.
/// </summary>
public class Character_UI : MonoBehaviour
{
    List<GameObject> prefab_btn_list = new List<GameObject>();// 생성한 버튼을 저장하는 리스트

    [Header("btn_prefab")]
    [SerializeField] GameObject btn_prefab;// 버튼 프리팹
    [SerializeField] GameObject content_zone;// 버튼을 생성할 부모 위치

    private int index_UI;//현재 띄우고 있는 캐릭터 인덱스
    private Character index_character; //현재 띄우고 있는 캐릭터 정보
    private Character cur_character; //데이터에 넣을 캐릭터 정보

    [Header("cur_character")]// 현재 선택됨 캐릭터에 대한 UI 컴포넌트
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text character_name;
    [SerializeField] private TMP_Text level;
    [SerializeField] private Button is_equip_btn;// 장착 여부 버튼
    [SerializeField] private Button leveluppannel_btn;// 레벨업 패널 버튼
    [SerializeField] private TMP_Text HP;
    [SerializeField] private TMP_Text special; //특수 능력
    [SerializeField] private TMP_Text unique; //고유 능력

    [Header("sprite")]// 버튼 상태에 따라 변경될 스프라이트 이미지들
    [SerializeField] private Sprite equip_btn_O;
    [SerializeField] private Sprite equip_btn_X;
    [SerializeField] private Sprite levelup_O;
    [SerializeField] private Sprite levelup_X;
    [SerializeField] private Sprite select_border;
    [SerializeField] private Sprite notselect_border;

    [Header("Levelup")]// 레벨업 패널 UI 컴포넌트
    [SerializeField] private TMP_Text next_hp;
    [SerializeField] private TMP_Text cur_hp;
    [SerializeField] private TMP_Text next_level;
    [SerializeField] private TMP_Text cur_level;
    [SerializeField] private TMP_Text gold;
    [SerializeField] private TMP_Text ark;
    [SerializeField] private TMP_Text basic;
    [SerializeField] private Button levelup_btn;
    [SerializeField] private GameObject max_level;
    [SerializeField] private GameObject max_hp;
    [SerializeField] private GameObject levelup_done;

    private void OnEnable()
    {
        Click_inven();
    }

    public void Click_inven()//인벤토리 클릭 시 실행됨
    {
        // 캐릭터 리스트 가져오기
        List<Character> list = BackendChart_JGD.chartData.character_list;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].curlevel >= 1)
            {
                Make_prefab(list[i].sprite, i);
            }
        }

        Setting();
    }

    private void OnDisable()
    {
        // 비활성화될 때 생성된 버튼들을 삭제하고 리스트를 비움
        for (int i = 0; i < content_zone.transform.childCount; i++)
        {
            Destroy(content_zone.transform.GetChild(i).gameObject);
        }

        prefab_btn_list.Clear();
    }

    private void Setting()
    {
        // 첫 번째 캐릭터를 현재 표시 중인 캐릭터로 설정
        index_character = BackendChart_JGD.chartData.character_list[0];
        // 현재 캐릭터를 사용자가 선택한 캐릭터로 설정
        cur_character = BackendChart_JGD.chartData.character_list[BackendGameData_JGD.userData.character];
        // UI 업데이트
        Update_UI();
        // 인덱스 초기화
        index_UI = 0;
    }

    private void Update_UI()// UI 요소들을 현재 캐릭터의 정보로 업데이트
    {
        image.sprite = SpriteManager.instance.Num2Sprite(index_character.sprite);

        character_name.text = index_character.character_name;
        level.text = index_character.curlevel.ToString();
        HP.text = (100 + ((index_character.curlevel - 1) * 10)).ToString();
        special.text = index_character.special;

        if (index_character.character_ID == Character_ID.Green)
        {
            unique.text = index_character.unique + index_character.percent + "% 추가 증가";
        }
        else
        {
            unique.text = index_character.unique + index_character.duration + "초 증가";
        }


        if (index_character.curlevel >= index_character.maxlevel)
        {
            Update_btn(false, levelup_btn, levelup_O, levelup_X);
        }
        else
        {
            Update_btn(true, levelup_btn, levelup_O, levelup_X);
        }

        Update_is_equip();
        btn_update();
    }

    private void btn_update()// 버튼 상태를 업데이트
    {
        for (int i = 0; i < prefab_btn_list.Count; i++)
        {

            if (i == index_UI)
            {
                prefab_btn_list[i].GetComponent<Image>().sprite = select_border;
            }
            else
            {
                prefab_btn_list[i].GetComponent<Image>().sprite = notselect_border;
            }


            if (i == BackendGameData_JGD.userData.character)
            {
                prefab_btn_list[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                prefab_btn_list[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    private void Update_is_equip()// 장착 상태를 업데이트
    {
        if (index_character == cur_character)
        {
            Update_btn(false, is_equip_btn, equip_btn_O, equip_btn_X);
        }
        else
        {
            Update_btn(true, is_equip_btn, equip_btn_O, equip_btn_X);
        }

        btn_update();
    }

    private void Update_btn(bool bool_, Button btn, Sprite O, Sprite X) // 버튼의 활성/비활성 상태 및 스프라이트 업데이트
    {
        if (bool_)
        {
            btn.interactable = true;
            btn.gameObject.GetComponent<Image>().sprite = O;
        }

        else
        {
            btn.interactable = false;
            btn.gameObject.GetComponent<Image>().sprite = X;
        }
    }

    private void Make_prefab(int sprite, int index)// 버튼 프리팹을 생성하고 리스트에 추가
    {
        GameObject newobj = Instantiate(btn_prefab, content_zone.transform);
        prefab_btn_list.Add(newobj);

        Button button = newobj.GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(() => Set_cur_character_btn(index));
        }

        newobj.transform.GetChild(0).GetComponent<Image>().sprite = SpriteManager.instance.Num2Sprite(sprite);
    }

    #region btn

    public void Set_cur_character_btn(int num)// 현재 표시 중인 캐릭터를 변경
    {
        index_UI = num;
        index_character = BackendChart_JGD.chartData.character_list[num];
        Update_UI();
        Debug.Log($"Set_cur_character_btn : {num}");
    }

    public void Equip_btn() //장착하기 버튼
    {
        cur_character = BackendChart_JGD.chartData.character_list[index_UI];
        cur_character.character_Data_update(index_UI);
        TCP_Client_Manager.instance.my_player.update_model((int)cur_character.character_ID);
        btn_update();
    }

    public void Inhance_btn() //캐릭터 강화 버튼
    {
        int gold_req, ark_req;

        if (index_character.maxlevel <= index_character.curlevel)
        {
            levelup_done.SetActive(true);
            return;
        }

        levelup_done.SetActive(false);

        if (index_character.CanLevelup(MoneyManager.instance.gold, MoneyManager.instance.ark, out gold_req, out ark_req))
        {
            index_character.Levelup(gold_req, ark_req);
            Update_UI();
            Enable_levelup_pannel_btn();
        }
        else
        {
            Debug.Log("돈이 부족해 강화 실패");
        }
    }

    #endregion
    #region levelup
    public void Enable_levelup_pannel_btn()// 레벨업 패널을 업데이트하고 표시
    {
        Character_amount cur_chartdata = BackendChart_JGD.chartData.Characteramount_list[index_character.curlevel - 1];
        Character_amount next_chartdata = null;

        if (index_character.curlevel < BackendChart_JGD.chartData.Characteramount_list.Count)
        {
            next_chartdata = BackendChart_JGD.chartData.Characteramount_list[index_character.curlevel];
        }

        if (next_chartdata == null)
        {
            max_hp.SetActive(true);
            max_level.SetActive(true);

            max_hp.transform.GetChild(0).GetComponent<TMP_Text>().text = (100 + ((cur_chartdata.level - 1) * 10)).ToString();
            max_level.transform.GetChild(0).GetComponent<TMP_Text>().text = cur_chartdata.level.ToString();

            Colored_text(false, "MAX", gold);
            Colored_text(false, "MAX", ark);

            levelup_btn.interactable = false;

            if (index_character.character_ID == Character_ID.Green)
            {
                basic.text = index_character.unique + index_character.percent + "% 추가 증가";
            }
            else
            {
                basic.text = index_character.unique + index_character.duration + "초 증가";
            }
            return;
        }

        max_hp.SetActive(false);
        max_level.SetActive(false);

        cur_level.text = cur_chartdata.level.ToString();
        next_level.text = next_chartdata.level.ToString();

        cur_hp.text = (100 + ((cur_chartdata.level - 1) * 10)).ToString();
        next_hp.text = (100 + ((next_chartdata.level - 1) * 10)).ToString();

        levelup_btn.interactable = true;

        if (index_character.character_ID == Character_ID.Green)
        {
            basic.text = index_character.unique + index_character.percent + "(<color=#B4FC11>" + 0.5 + "</color>)" + "% 추가 증가";
        }
        else
        {
            basic.text = index_character.unique + index_character.duration + "(<color=#B4FC11>" + 0.1 + "</color>)" + "초 증가";
        }

        Colored_text(MoneyManager.instance.gold > cur_chartdata.gold, cur_chartdata.gold, gold);
        Colored_text(MoneyManager.instance.ark > cur_chartdata.ark, cur_chartdata.ark, ark);

    }

    private void Colored_text(bool bool_, object obj, TMP_Text text)// 텍스트 색상을 변경하여 업데이트
    {
        if (bool_)
        {
            text.text = "<color=#FFFFFF>" + obj + "</color>";
        }
        else
        {
            text.text = "<color=#FF511A>" + obj + "</color>";
        }
        Debug.Log(obj);
    }
    #endregion
}
