using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChatBoxManager : MonoBehaviour
{
    [SerializeField] private GameObject chat_panel;
    [SerializeField] private GameObject hide_btn;
    [SerializeField] private GameObject show_btn;
    [SerializeField] private Transform global_chat_box;
    [SerializeField] private Transform global_chat_UI;
    [SerializeField] private Scrollbar global_scroll_bar;
    [SerializeField] private Transform local_chat_box;
    [SerializeField] private Transform local_chat_UI;
    [SerializeField] private Scrollbar local_scroll_bar;
    [SerializeField] private GameObject chat_line_prefab;
    
    private List<GameObject> global_chat_line_list;
    private List<GameObject> local_chat_line_list;
    public TMP_Text chat_input;
    public TMP_InputField chat_input_field;

    public bool is_global_chat = true;
    // Start is called before the first frame update
    void Start()
    {
        local_chat_line_list = new List<GameObject>();
        global_chat_line_list = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change_is_global(bool value_) {
        is_global_chat = value_;
    }

    public void update_display() {
        global_chat_UI.gameObject.SetActive(is_global_chat);
        local_chat_UI.gameObject.SetActive(!is_global_chat);
    }
    
    public void chat(string msg, bool is_global) {
        
        if (is_global)
        {
            GameObject go = Instantiate(chat_line_prefab, global_chat_box);
            go.GetComponentInChildren<TMP_Text>().text = msg;
            global_chat_line_list.Add(go);
            StartCoroutine(scroll_to_bottom(global_scroll_bar));
        }
        else {
            GameObject go = Instantiate(chat_line_prefab, local_chat_box);
            go.GetComponentInChildren<TMP_Text>().text = msg;
            local_chat_line_list.Add(go);
            StartCoroutine(scroll_to_bottom(local_scroll_bar));
        }
    }

    public IEnumerator scroll_to_bottom(Scrollbar sb) {
        yield return null;
        Canvas.ForceUpdateCanvases();
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
        sb.value = 0f;
    }

    public void clear_input() {
        chat_input.text = string.Empty;
        chat_input_field.text = string.Empty;
    }
    public void clear() {
        if (local_chat_line_list != null) {
            for (int i = 0; i < local_chat_line_list.Count; i++)
            {
                if (local_chat_line_list[i] != null)
                {
                    Destroy(local_chat_line_list[i]);
                }                
            }
            local_chat_line_list.Clear();
        }
        
    }

    public void hide_chat() {
        chat_panel.SetActive(false);
        hide_btn.SetActive(false);
        show_btn.SetActive(true);
    }
    public void show_chat()
    {
        chat_panel.SetActive(true);
        update_display();
        hide_btn.SetActive(true);
        show_btn.SetActive(false);

    }
}
