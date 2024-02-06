using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChatBoxManager : MonoBehaviour
{
    [SerializeField] private Transform chat_box;
    [SerializeField] private GameObject chat_line_prefab;
    private List<GameObject> chat_line_list;
    public TMP_Text chat_input;
    public TMP_InputField chat_input_field;
    // Start is called before the first frame update
    void Start()
    {
        chat_line_list = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chat(string msg) {
        GameObject go = Instantiate(chat_line_prefab);
        go.GetComponentInChildren<TMP_Text>().text = msg;
        go.transform.SetParent(chat_box);
        chat_line_list.Add(go);
    }

    public void clear_input() {
        chat_input.text = string.Empty;
        chat_input_field.text = string.Empty;
    }
    public void clear() {
        for (int i = 0; i < chat_line_list.Count; i++) {
            Destroy(chat_line_list[i]);
        }
        chat_line_list.Clear();
    }
}
