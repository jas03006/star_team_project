
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
public class SearchEngine_TG : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Transform conatainer;


    public void sort()
    {
        Transform[] m_Children = new Transform[conatainer.childCount];
        for (int i = 0; i < conatainer.childCount; i++)
        {
            m_Children[i] = conatainer.GetChild(i);
        }
        m_Children = m_Children.OrderByDescending(go => get_score(go)).ToArray();

        for (int i = 0; i < m_Children.Length; i++)
        {
            m_Children[i].SetSiblingIndex(i);
        }
    }

    public int get_score(Transform go) {
        string target = go.GetComponentInChildren<TMP_Text>()?.text;
        if (target == null) {
            go.gameObject.SetActive(false);
            return 0;
        }
        string input_text = input.text;
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
