using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage003 : MonoBehaviour
{
    [SerializeField] public List<string> StageList = new List<string>();
    [SerializeField] public int thisStage;          //스테이지 1~5 넘어올때 받아야함
    #region stage
    private string stage1;
    private string stage2;
    private string stage3;
    private string stage4;
    private string stage5;
    # endregion
    private void Awake()
    {
        StageList.Add(stage1);
        StageList.Add(stage2);
        StageList.Add(stage3);
        StageList.Add(stage4);
        StageList.Add(stage5);
        thisStage = LevelSelectMenuManager_JGD.currLevel;
    }
    private void Start()
    {
        StageItemInfo_JGD.Instance.ReadStage(StageList[thisStage]);
    }
}
