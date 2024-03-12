using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemaSelect : MonoBehaviour
{
    [SerializeField] private List<GameObject> Stages = new List<GameObject>();  
    private void Awake()
    {
        Stages[LevelSelectMenuManager_JGD.GalaxyLevel].SetActive(true);
    }
}
