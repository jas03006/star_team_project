using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage001 : MonoBehaviour
{
    [SerializeField] public List<string> StageList = new List<string>();
    [SerializeField] public int thisStage;          //스테이지 1~5 넘어올때 받아야함
    # region stage
    private string stage1 = "28:10:0," +
        "28:11:0," +
        "28:12:0," +
        "28:13:0," +
        "28:14:0," +
        "28:17:3," +
        "28:17:4," +
        "28:18:3," +
        "28:18:4," +
        "28:19:4," +
        "28:19:5," +
        "28:19:4," +
        "28:20:5," +
        "18:21:5," +
        "28:22:0," +
        "28:23:0," +
        "28:24:1," +
        "28:24:7," +
        "28:24:8," +
        "28:25:1," +
        "28:25:7," +
        "28:25:8," +
        "28:26:0," +
        "28:26:7," +
        "28:26:8," +
        "28:27:0," +
        "29:27:0," +
        "26:28:0," +
        "27:29:0," +
        "28:30:0," +
        "29:31:0," +
        "30:32:0," +
        "31:33:0," +
        "32:34:0," +
        "33:35:0," +
        "34:36:0," +
        "35:37:0," +
        "36:38:0," +
        "37:39:0," +
        "38:40:0," +
        "39:41:0," +
        "40:42:0," +
        "41:43:0," +
        "42:44:0," +
        "43:45:0," +
        "44:46:0," +
        "45:47:0," +
        "46:48:0," +
        "47:49:0," +
        "48:50:0," +
        "49:51:0," +
        "50:53:0," +
        "28:17:4";
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
    }
    private void Start()
    {
        StageItemInfo_JGD.Instance.ReadStage(StageList[thisStage]);
    }
    
}
