
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

// container 아래의 UI Element 중 원하는 text를 가진 원소를 검색
public class SearchEngine_TG : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Transform conatainer;

    private string input_text = string.Empty;
    [SerializeField] UIManager_JGD uimanager; //램던 친구 추천의 경우, 정렬 방식이 달라지기 때문에 친구창에서 현재 선택된 탭이 자동 친구추천인지 알기 위해 참조
    Transform[] m_Children; //conatainer 의 자식 트랜스폼을 담을 배열
    private void OnEnable()
    {
        input.text = string.Empty;

        if (m_Children !=null && uimanager != null && uimanager.now_selection == 1) { //랜덤 친구 추천의 경우, 10개의 결과만을 표시
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
    //검색 및 정렬
    public void sort()
    {
        input_text = input.text;
        bool is_random =false;
        if (input_text == string.Empty && uimanager !=null &&uimanager.now_selection==1) { //랜덤 친구 추천인 경우
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

    //랜덤 점수 산출
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

    //검색어에 따른 점수 산출
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
