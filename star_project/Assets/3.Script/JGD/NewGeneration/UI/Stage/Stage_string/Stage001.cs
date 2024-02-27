using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage001 : MonoBehaviour
{
    [SerializeField] public List<string> StageList = new List<string>();
    [SerializeField] public int thisStage;          //스테이지 1~5 넘어올때 받아야함
    #region stage
    #region stage001
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
        "28:20:4:0:1:1," +
        "28:20:5:0:1:1," +
        "18:21:5:0:1:1," +
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
        "28:27:7.5:0:1:1," +
        "28:30:7:0:1:1," +
        "28:30:6:0:1:1," +
        "28:31:7:0:1:1," +
        "28:31:6:0:1:1," +
        "19:32:8:0:1:1," +
        "28:33:0:0:1:1," +
        "28:34:0:0:1:1," +
        "28:35:0:0:1:1," +
        "28:35:8:0:1:1," +
        "28:36:1:0:1:1," +
        "28:36:8:0:1:1," +
        "28:37:1:0:1:1," +
        "29:37:8:0:1:1," +
        "28:38:1:0:1:1," +
        "28:38:8:0:1:1," +
        "28:39:8:0:1:1," +
        "28:39:7:0:1:1," +
        "28:39:5:0:1:1," +
        "28:40:5:0:1:1," +
        "28:40:7:0:1:1," +
        "28:41:5:0:1:1," +
        "28:41:7:0:1:1," +
        "28:42:6:0:1:1," +
        "40:45:0:0:1:1," +
        "28:46:6:0:1:1," +
        "28:46:7:0:1:1," +
        "26:47:6:0:1:1," +
        "26:48:6:0:1:1," +
        "28:49:6:0:1:1," +
        "28:49:5:0:1:1," +
        "28:50:6:0:1:1," +
        "28:50:5:0:1:1," +
        "26:51:5:0:1:1," +
        "26:52:4:0:1:1," +
        "26:53:3:0:1:1," +
        "28:53:7:0:1:1," +
        "28:53:8:0:1:1," +
        "0:54:2:0:1:1," +
        "28:54:7:0:1:1," +
        "28:54:8:0:1:1," +
        "28:55:7:0:1:1," +
        "28:55:8:0:1:1," +
        "28:56:7:0:1:1," +
        "28:56:8:0:1:1," +
        "29:57:7:0:1:1," +
        "28:57:0:0:1:1," +
        "28:58:0:0:1:1," +
        "28:59:0:0:1:1," +
        "26:60:0:0:1:1," +
        "26:61:1:0:1:1," +
        "40:61:9:180:1:1," +
        "26:62:1:0:1:1," +
        "26:63:1:0:1:1," +
        "28:64:1:0:1:1," +
        "27:64:6:0:1:1," +
        "28:64:7:0:1:1," +
        "28:65:0:0:1:1," +
        "28:65:6:0:1:1," +
        "28:65:7:0:1:1," +
        "28:65:8:0:1:1," +
        "27:66:0.5:0:1:1," +
        "17:67:8:0:1:1," +
        "28:68:8:0:1:1," +
        "28:69:8:0:1:1," +
        "28:69:0:0:1:1," +
        "28:70:0:0:1:1," +
        "28:70:1:0:1:1," +
        "28:70:8:0:1:1," +
        "28:72:5:0:1:1," +
        "28:73:5:0:1:1," +
        "28:73:6:0:1:1," +
        "28:74:6:0:1:1," +
        "28:74:7:0:1:1," +
        "28:75:6:0:1:1," +
        "28:75:7:0:1:1," +
        "40:76:0:0:1:1," +
        "28:76:6:0:1:1," +
        "28:76:7:0:1:1," +
        "28:77:6:0:1:1," +
        "28:77:7:0:1:1," +
        "29:78:6.5:0:1:1,";
    #endregion
    #region stage002
    private string stage2 = "28:10:0:0:1:1," +
        "32:10:2:0:1:1," +
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
    #endregion
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
