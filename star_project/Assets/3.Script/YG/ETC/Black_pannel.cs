using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// logo ������ �ִ� ������ pannel�� �����ϴ� Ŭ����
/// ������ pannel �������� ���� : ����� ���� �� �ܻ�ó�� �ڿ� ������Ʈ�� �����ִ� ������ ���ܼ� �������� 
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
