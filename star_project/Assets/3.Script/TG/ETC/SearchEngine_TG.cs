
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

// container �Ʒ��� UI Element �� ���ϴ� text�� ���� ���Ҹ� �˻�
public class SearchEngine_TG : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Transform conatainer;

    private string input_text = string.Empty;
    [SerializeField] UIManager_JGD uimanager; //���� ģ�� ��õ�� ���, ���� ����� �޶����� ������ ģ��â���� ���� ���õ� ���� �ڵ� ģ����õ���� �˱� ���� ����
    Transform[] m_Children; //conatainer �� �ڽ� Ʈ�������� ���� �迭
    private void OnEnable()
    {
        input.text = string.Empty;

        if (m_Children !=null && uimanager != null && uimanager.now_selection == 1) { //���� ģ�� ��õ�� ���, 10���� ������� ǥ��
            for (int i = 0; i < m_Children.Length; i++)
            {
                m_Children[i].gameObject.SetActive(i < 10);
            }
        }
        // sort();
    }
    private void Awake()
    {
        if (uimanager == null) { 
            uimanager = FindFirstObjectByType<UIManager_JGD>();
        }
    }
    public IEnumerator sort_co() { 
        yield return null;
        sort();
    }
    public void clear_input() {
        input.text = string.Empty;
    }
    //�˻� �� ����
    public void sort()
    {
        input_text = input.text;
        bool is_random =false;
        if (input_text == string.Empty && uimanager !=null &&uimanager.now_selection==1) { //���� ģ�� ��õ�� ���
            is_random = true;
        }

        m_Children = new Transform[conatainer.childCount];
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

        Debug.Log(m_Children.Length);
        for (int i = 0; i < m_Children.Length; i++)
        {
            m_Children[i].SetSiblingIndex(i);
        }

        if (is_random) {
            for (int i = 0; i < m_Children.Length; i++)
            {
                m_Children[i].gameObject.SetActive(i < 10);
            }
        }

    }

    //���� ���� ����
    public int get_random_score(Transform go) {
        string target = go.GetComponentInChildren<TMP_Text>()?.text;
        if (target == null)
        {
            go.gameObject.SetActive(false);
            Debug.Log("no target element");
            return 0;
        }
        return Random.Range(0,101);
    }

    //�˻�� ���� ���� ����
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
