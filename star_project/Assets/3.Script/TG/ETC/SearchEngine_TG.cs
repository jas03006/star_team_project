
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
public class SearchEngine_TG : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Transform conatainer;

    private string input_text = string.Empty;
    [SerializeField] UIManager_JGD uimanager;
    
    private void OnEnable()
    {
        input.text = string.Empty;
       // sort();
    }
    private void Awake()
    {
        if (uimanager == null) { 
            uimanager = FindFirstObjectByType<UIManager_JGD>();
        }
    }
    public void sort()
    {
        input_text = input.text;
        bool is_random =false;
        if (input_text == string.Empty && uimanager !=null &&uimanager.now_selection==1) {
            is_random = true;
        }

        Transform[] m_Children = new Transform[conatainer.childCount];
        for (int i = 0; i < conatainer.childCount; i++)
        {
            m_Children[i] = conatainer.GetChild(i);
        }

        if (is_random)
        {
            m_Children = m_Children.OrderByDescending(go => get_random_score(go)).ToArray();
        }
        else {
            m_Children = m_Children.OrderByDescending(go => get_score(go)).ToArray();
        }
        

        for (int i = 0; i < m_Children.Length; i++)
        {
            if (is_random )
            {
                if (i >= 10)
                {
                    m_Children[i].gameObject.SetActive(false);
                }
                else {
                    m_Children[i].gameObject.SetActive(true);
                }
            }
            m_Children[i].SetSiblingIndex(i);
        }
    }

    public int get_random_score(Transform go) {
        string target = go.GetComponentInChildren<TMP_Text>()?.text;
        if (target == null)
        {
            go.gameObject.SetActive(false);
            return 0;
        }
        return Random.Range(0,101);
    }

    public int get_score(Transform go) {
        string target = go.GetComponentInChildren<TMP_Text>()?.text;
        if (target == null) {
            go.gameObject.SetActive(false);
            return 0;
        }
        
        if (input_text.Equals(target)) {
            go.gameObject.SetActive(true);
            return 100;
        } else if (target.Contains(input_text)) { 
            go.gameObject.SetActive(true);
            return 100 - (target.Length - input_text.Length);
        }
        go.gameObject.SetActive(false);
        return 0;
    }
}
