using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//이모지 리스트 UI를 담당하는 클래스 (마이플래닛 좌측 상단)
public class EmoziBoxManager : MonoBehaviour
{
    [SerializeField] private GameObject emozi_panel;
    [SerializeField] private GameObject hide_btn;
    [SerializeField] private GameObject show_btn;

    [SerializeField] private GameObject emozi_element_prefab;
    [SerializeField] private Transform container;
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    public void init()
    {
        for (int i = 0; i < container.childCount; i++)
        {
            Destroy(container.GetChild(i).gameObject);
        }
        //TODO: read emozi inventory data and creat emozi element and add it into container
        foreach (int i in BackendGameData_JGD.userData.emozi_List)
        {
            GameObject go = Instantiate(emozi_element_prefab, container);
            go.GetComponent<Image>().sprite = SpriteManager.instance.Num2emozi(i);
            Button btn = go.GetComponent<Button>();
            btn.onClick.AddListener(() => {TCP_Client_Manager.instance.my_player.show_emozi_net(i); AudioManager.instance.SFX_Click(); }); //클릭 시 이모지 전송
        }
    }

    public void hide_box()
    {
        emozi_panel.SetActive(false);
        hide_btn.SetActive(false);
        show_btn.SetActive(true);
    }
    public void show_box()
    {
  
        emozi_panel.SetActive(true);
        hide_btn.SetActive(true);
        init();
        show_btn.SetActive(false);

    }
}
