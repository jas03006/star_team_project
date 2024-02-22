using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage001 : MonoBehaviour
{
    [SerializeField] public List<string> StageList = new List<string>();
    [SerializeField] public int thisStage;          //스테이지 1~5 넘어올때 받아야함
    # region stage
    private string stage1 = "28:10:0:0:1:1," +
        "28:11:0:0:1:1," +
        "28:12:0:0:1:1," +
        "28:13:0:0:1:1," +
        "28:14:0:0:1:1," +
        "28:17:3:0:1:1," +
        "28:17:4:0:1:1," +
        "28:18:3:0:1:1," +
        "28:18:4:0:1:1," +
        "28:19:4:0:1:1," +
        "28:19:5:0:1:1," +
        "28:19:4:0:1:1," +
        "28:20:5:0:1:1," +
        "18:21:5:0:1:1," +
        "0:25:5:0:1:1," +
        "28:22:0:0:1:1," +
        "28:23:0:0:1:1," +
        "28:24:1:0:1:1," +
        "28:24:7:0:1:1," +
        "28:24:8:0:1:1," +
        "28:25:1:0:1:1," +
        "28:25:7:0:1:1," +
        "28:25:8:0:1:1," +
        "28:26:0:0:1:1," +
        "28:26:7:0:1:1," +
        "28:26:8:0:1:1," +
        "28:27:0:0:1:1," +
        "29:27:0:0:1:1," +
        "26:28:0:0:1:1," +
        "27:29:0:0:1:1," +
        "28:30:0:0:1:1," +
        "29:31:0:0:1:1," +
        "30:32:0:0:1:1," +
        "31:33:0:0:1:1," +
        "32:34:0:0:1:1," +
        "33:35:0:0:1:1," +
        "34:36:0:0:1:1," +
        "35:37:0:0:1:1," +
        "36:38:0:0:1:1," +
        "37:39:0:0:1:1," +
        "38:40:0:0:1:1," +
        "39:41:0:0:1:1," +
        "40:42:0:0:1:1," +
        "41:43:0:0:1:1," +
        "42:44:0:0:1:1," +
        "43:45:0:0:1:1," +
        "44:46:0:0:1:1," +
        "45:47:0:0:1:1," +
        "46:48:0:0:1:1," +
        "47:49:0:0:1:1," +
        "48:50:0:0:1:1," +
        "49:51:0:0:1:1," +
        "50:10:0:0:1:1:2:100," +
        "51:20:0:180:1:1:2," +
        "52:22:9:180:1:1:2," +
        "53:24:9:135:1:1:2," +
        "54:26:9:180:1:1:1," +
        "55:28:9:135:1:1:1," +
        "56:30:9:180:1:1:1," +
        "28:17:4:0:1:1";
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
