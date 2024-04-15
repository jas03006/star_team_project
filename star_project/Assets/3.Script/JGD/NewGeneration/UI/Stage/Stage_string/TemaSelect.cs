using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemaSelect : MonoBehaviour
{//테마별 스테이지 선택
    [SerializeField] private List<GameObject> Stages = new List<GameObject>();   
    private void Awake()
    {
        Stages[LevelSelectMenuManager_JGD.GalaxyLevel].SetActive(true);
    }
}
