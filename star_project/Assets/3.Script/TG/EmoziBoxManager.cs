using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Update is called once per frame
    void Update()
    {
        
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
            btn.onClick.AddListener(() => {TCP_Client_Manager.instance.my_player.show_emozi_net(i); AudioManager.instance.SFX_Click(); });
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
        show_btn.SetActive(false);

    }
}
