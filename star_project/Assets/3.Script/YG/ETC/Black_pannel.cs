using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// logo 씬부터 있는 검은색 pannel을 관리하는 클래스
/// 검은색 pannel 만들어놓은 이유 : 모바일 빌드 시 잔상처럼 뒤에 오브젝트가 남아있는 오류가 생겨서 가려놓음 
/// </summary>
public class Black_pannel : MonoBehaviour
{
    public static Black_pannel instance;
    [SerializeField] Canvas canvas;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
