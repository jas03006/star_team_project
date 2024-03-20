using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage001 : MonoBehaviour
{//아이템 : X좌표 : Y좌표 : 로테이션 : X스케일 : Y스케일 : 식별번호 : 거리
    [SerializeField] public List<string> StageList = new List<string>();
    [SerializeField] public int thisStage;          //스테이지 1~5 넘어올때 받아야함
    #region stage
    #region stage001
    private string stage1 = "28:5:2:0:1:1," +
        "28:6:3:0:1:1," +
        "28:7:4:0:1:1," +
        "28:8:4:0:1:1," +
        "36:8:0:0:1:0.75," +
        "28:9:4:0:1:1," +
        "26:10:4:0:1:1," +
        "26:11:3:0:1:1," +
        "26:12:2:0:1:1," +
        "26:13:1:0:1:1," +
        "26:14:0:0:1:1," +
        "41:14:9:180:1:1.25," +
        "3:15:0:0:1:1," +
        "28:16:0:0:1:1," +
        "28:17:1:0:1:1," +
        "28:19:3:0:1:1," +
        "28:20:3:0:1:1," +
        "28:21:5:0:1:1," +
        "26:23:7:0:1:1," +
        "26:24:7:0:1:1," +
        "26:25:7:0:1:1," +
        "36:25:0:0:1:1.5," +
        "26:26:7:0:1:1," +
        "26:27:7:0:1:1," +
        "28:28:7:0:1:1," +
        "28:29:6:0:1:1," +
        "28:30:5:0:1:1," +
        "28:31:5:0:1:1," +
        "36:36:0:0:1:0.5," +
        "36:36:9:180:1:0.75," +
        "26:35:4:0:1:1," +
        "26:36:3:0:1:1," +
        "29:36:4:0:1:1," +
        "26:36:5:0:1:1," +
        "26:37:3:0:1:1," +
        "29:37:4:0:1:1," +
        "26:37:5:0:1:1," +
        "26:38:4:0:1:1," +
        "14:39:5:0:1:1," +
        "35:42:3:0:1:1," +
        "26:43:3:0:1:1," +
        "26:44:3:0:1:1," +
        "28:45:3:0:1:1," +
        "36:45:9:135:1:1," +
        "26:46:6:0:1:1," +
        "26:47:7:0:1:1," +
        "26:47:0:0:1:1," +
        "26:48:7:0:1:1," +
        "26:48:0:0:1:1," +
        "28:49:7:0:1:1," +
        "28:49:0:0:1:1," +
        "11:50:1:0:1:1," +
        "28:50:7:0:1:1," +
        "42:50.5:3.5:0:2:2:0," +
        "28:51:7:0:1:1," +
        "41:55:0:-45:1:1," +
        "28:55:8:0:1:1," +
        "28:56:8:0:1:1," +
        "28:57:7:0:1:1," +
        "28:58:6:0:1:1," +
        "28:59:5:0:1:1," +
        "29:59:1:0:1:1," +
        "28:60:1:0:1:1," +
        "28:61:1:0:1:1," +
        "26:62:1:0:1:1," +
        "36:62:9:180:1:1," +
        "28:63:1:0:1:1," +
        "28:64:1:0:1:1," +
        "11:65:1:0:1:1," +
        "26:67:6:0:1:1," +
        "26:68:5:0:1:1," +
        "29:68:6:0:1:1," +
        "26:68:7:0:1:1," +
        "26:69:5:0:1:1," +
        "29:69:6:0:1:1," +
        "26:69:7:0:1:1," +
        "26:70:6:0:1:1," +
        "36:71:0:0:1:1";
    #endregion
    #region stage002
    private string stage2 = "28:3:1:0:1:1," +
        "28:3:2:0:1:1," +
        "28:4:2:0:1:1," +
        "28:4:3:0:1:1," +
        "28:5:3:0:1:1," +
        "28:5:4:0:1:1," +
        "28:6:4:0:1:1," +
        "1:6:5:0:1:1," +
        "47:5.5:7.5:0:2:2:0," +
        "47:6.5:2.5:0:2:2:0," +
        "28:10:2:0:1:1," +
        "29:10:8:0:1:1," +
        "47:10.5:5.5:0:2:2:0," +
        "28:11:1:0:1:1," +
        "28:11:2:0:1:1," +
        "35:11:4:0:1:1," +
        "29:11:8:0:1:1," +
        "28:12:1:0:1:1," +
        "0:12:8:0:1:1," +
        "47:14.5:7.5:0:2:2:0," +
        "47:13.5:2.5:0:2:2:0," +
        "47:18.5:0.5:0:2:2:0," +
        "47:18.5:4.5:0:2:2:0," +
        "50:15:0:0:1:1:1:20," +
        "11:24:2:0:1:1," +
        "26:25:3:0:1:1," +
        "26:26:3:0:1:1," +
        "26:26:4:0:1:1," +
        "26:27:4:0:1:1," +
        "26:27:5:0:1:1," +
        "26:28:5:0:1:1," +
        "26:28:6:0:1:1," +
        "51:27:9:135:1:1.5:1," +
        "26:29:6:0:1:1," +
        "26:29:7:0:1:1," +
        "28:30:7:0:1:1," +
        "28:30:8:0:1:1," +
        "28:31:8:0:1:1," +
        "28:31:9:0:1:1," +
        "28:32:9:0:1:1," +
        "11:33:9:0:1:1.25," +
        "36:33:0:0:1:1," +
        "50:32:0:0:1:1:2:30," +
        "28:37:8:0:1:1," +
        "28:38:8:0:1:1," +
        "28:39:8:0:1:1," +
        "35:40:4:0:1:1," +
        "42:40.5:5.5:0:2:2:2," +
        "28:41:8:0:1:1," +
        "28:42:8:0:1:1," +
        "42:44.5:2.5:0:2:2:2," +
        "26:45:7:0:1:1," +
        "26:46:7:0:1:1," +
        "26:46:6:0:1:1," +
        "26:47:6:0:1:1," +
        "26:47:5:0:1:1," +
        "26:47:3:0:1:1," +
        "26:48:2:0:1:1," +
        "29:48:3:0:1:1," +
        "26:48:4:0:1:1," +
        "26:48:5:0:1:1," +
        "26:49:2:0:1:1," +
        "29:49:3:0:1:1," +
        "26:49:4:0:1:1," +
        "26:50:3:0:1:1," +
        "36:52:9:180:1:1.25," +
        "28:54:3:0:1:1," +
        "28:55:4:0:1:1," +
        "28:56:5:0:1:1," +
        "14:57:6:0:1:1," +
        "41:58:0:0:1:0.75," +
        "29:62:0:0:1:1," +
        "29:63:0:0:1:1," +
        "14:64:0:0:1:1," +
        "26:65:2:0:1:1," +
        "26:66:2:0:1:1," +
        "26:66:3:0:1:1," +
        "26:67:3:0:1:1," +
        "26:67:4:0:1:1," +
        "26:68:4:0:1:1," +
        "26:68:5:0:1:1," +
        "26:69:5:0:1:1," +
        "28:69:6:0:1:1," +
        "26:70:5:0:1:1," +
        "28:70:6:0:1:1," +
        "28:70:7:0:1:1," +
        "28:71:7:0:1:1," +
        "13:71:8:0:1:1," +
        "50:65:0:0:1:1:3:20," +
        "51:75:0:45:1:1.5:3," +
        "50:72:0:0:1:1:4:20," +
        "42:77.5:7.5:0:2:2:4";
    #endregion
    #region stage003
    private string stage3 = "28:4:0:0:1:1," +
        "28:5:0:0:1:1," +
        "28:6:1:0:1:1," +
        "28:7:2:0:1:1," +
        "28:8:3:0:1:1," +
        "28:9:4:0:1:1," +
        "35:11:5:0:1:1," +
        "57:11:0:0:1:1," +
        "28:14:7:0:1:1," +
        "28:15:6:0:1:1," +
        "26:15:7:0:1:1," +
        "28:15:8:0:1:1," +
        "28:16:6:0:1:1," +
        "1:16:7:0:1:1," +
        "28:16:8:0:1:1," +
        "28:17:7:0:1:1," +
        "41:18:0:0:1:0.75," +
        "11:20:0:0:1:1," +
        "29:21:0:0:1:1," +
        "57:21:9:0:1:1.25," +
        "29:22:0:0:1:1," +
        "28:23:0:0:1:1," +
        "28:24:0:0:1:1," +
        "28:25:1:0:1:1," +
        "28:26:2:0:1:1," +
        "26:27:3:0:1:1," +
        "26:28:4:0:1:1," +
        "26:29:5:0:1:1," +
        "35:30:6:0:1:1," +
        "28:31:7:0:1:1," +
        "28:33:3:0:1:1," +
        "28:33:4:0:1:1," +
        "28:34:3:0:1:1," +
        "28:34:4:0:1:1," +
        "36:34:9:0:1:1," +
        "28:35:3:0:1:1," +
        "28:35:4:0:1:1," +
        "28:36:3:0:1:1," +
        "28:36:4:0:1:1," +
        "28:37:3:0:1:1," +
        "28:37:4:0:1:1," +
        "42:38.5:0.5:0:2:2:0," +
        "42:41.5:6.5:0:2:2:0," +
        "26:44:5:0:1:1," +
        "42:45.5:1.5:0:2:2:0," +
        "26:45:6:0:1:1," +
        "26:46:7:0:1:1," +
        "42:47.5:5.5:0:2:2:0," +
        "28:47:8:0:1:1," +
        "28:48:8:0:1:1," +
        "14:49:8:0:1:1," +
        "51:55:9:130:0.75:1.5:1," +
        "50:44:0:0:1:1:1:20," +
        "28:52:3:0:1:1," +
        "28:53:3:0:1:1," +
        "28:53:4:0:1:1," +
        "28:54:4:0:1:1," +
        "26:54:5:0:1:1," +
        "26:55:5:0:1:1," +
        "26:55:6:0:1:1," +
        "26:56:6:0:1:1," +
        "26:57:5:0:1:1," +
        "27:57:6:0:1:1," +
        "26:58:4:0:1:1," +
        "28:59:4:0:1:1," +
        "51:59:0:40:0.75:1.25:2," +
        "50:50:0:0:1:1:2:20," +
        "2:60:3:0:1:1," +
        "28:60:4:0:1:1," +
        "28:61:4:0:1:1," +
        "28:62:5:0:1:1," +
        "28:63:6:0:1:1," +
        "51:63:9:130:0.75:1.25:3," +
        "50:53:0:0:1:1:3:20," +
        "28:64:6:0:1:1," +
        "28:65:6:0:1:1," +
        "51:66:0:40:0.75:1.25:4," +
        "50:57:0:0:1:1:4:20," +
        "29:68:0:0:1:1," +
        "29:69:0:0:1:1," +
        "47:69.5:6.5:0:2:2:0," +
        "28:70:2:0:1:1," +
        "28:71:3:0:1:1," +
        "26:72:6:0:1:1," +
        "26:73:5:0:1:1," +
        "28:73:6:0:1:1," +
        "26:73:7:0:1:1," +
        "26:74:5:0:1:1," +
        "28:74:6:0:1:1," +
        "26:74:7:0:1:1," +
        "42:74.5:3.5:0:2:2:0," +
        "10:76:5:0:1:1," +
        "26:77:4:0:1:1," +
        "27:78:4:0:1:1," +
        "42:77.5:7.5:0:2:2:0," +
        "29:79:4:0:1:1";
    #endregion
    #region stage004
    private string stage4 = "28:3:2:0:1:1," +
        "28:4:3:0:1:1," +
        "28:5:4:0:1:1," +
        "28:6:4:0:1:1," +
        "28:6:5:0:1:1," +
        "29:7:4:0:1:1," +
        "28:7:5:0:1:1," +
        "28:7:6:0:1:1," +
        "57:7:0:0:1:0.75," +
        "28:8:4:0:1:1," +
        "28:8:5:0:1:1," +
        "28:9:4:0:1:1," +
        "28:10:3:0:1:1," +
        "28:11:2:0:1:1," +
        "29:12:1:0:1:1," +
        "42:14.5:0.5:0:2:2:0," +
        "26:14:3:0:1:1," +
        "26:15:3:0:1:1," +
        "26:16:3:0:1:1," +
        "35:17:3:0:1:1," +
        "28:18:2:0:1:1," +
        "26:18:5:0:1:1," +
        "36:18:9:135:1:1.5:1," +
        "28:19:2:0:1:1," +
        "26:19:6:0:1:1," +
        "28:20:2:0:1:1," +
        "2:20:7:0:1:1," +
        "28:21:1:0:1:1," +
        "28:21:2:0:1:1," +
        "28:21:8:0:1:1," +
        "28:22:1:0:1:1," +
        "28:22:8:0:1:1," +
        "28:23:0:0:1:1," +
        "28:23:8:0:1:1," +
        "29:24:0:0:1:1," +
        "28:24:8:0:1:1," +
        "42:29.5:6.5:0:2:2:1," +
        "42:31.5:0.5:0:2:2:1," +
        "50:22:0:0:1:1:1:20," +
        "26:34:7:0:1:1," +
        "28:35:1:0:1:1," +
        "26:35:8:0:1:1," +
        "28:36:1:0:1:1," +
        "26:36:8:0:1:1," +
        "28:37:1:0:1:1," +
        "26:37:8:0:1:1," +
        "35:38:1:0:1:1," +
        "0:38:8:0:1:1," +
        "28:39:1:0:1:1," +
        "28:39:8:0:1:1," +
        "28:40:1:0:1:1," +
        "28:40:8:0:1:1," +
        "57:40:4.5:270:0.75:2," +
        "28:41:2:0:1:1," +
        "26:41:7:0:1:1," +
        "28:42:3:0:1:1," +
        "26:42:6:0:1:1," +
        "28:43:3:0:1:1," +
        "28:43:6:0:1:1," +
        "36:44:0:0:1:0.5," +
        "29:44:3:0:1:1," +
        "12:44:6:0:1:1," +
        "36:44:9:0:1:0.5," +
        "28:45:3:0:1:1," +
        "26:45:6:0:1:1," +
        "28:46:2:0:1:1," +
        "26:46:7:0:1:1," +
        "28:47:1:0:1:1," +
        "26:47:8:0:1:1," +
        "28:48:1:0:1:1," +
        "26:48:8:0:1:1," +
        "28:53:6:0:1:1," +
        "28:53:7:0:1:1," +
        "57:54:0:0:1:1," +
        "4:54:6:0:1:1," +
        "28:54:7:0:1:1," +
        "28:55:6:0:1:1," +
        "28:55:7:0:1:1," +
        "28:58:1:0:1:1," +
        "28:58:2:0:1:1," +
        "28:59:1:0:1:1," +
        "17:59:2:0:1:1," +
        "57:59:9:0:1:1," +
        "28:60:1:0:1:1," +
        "28:60:2:0:1:1," +
        "28:63:6:0:1:1," +
        "28:63:7:0:1:1," +
        "57:64:0:0:1:1," +
        "0:64:6:0:1:1," +
        "28:64:7:0:1:1," +
        "28:65:6:0:1:1," +
        "28:65:7:0:1:1," +
        "26:68:1:0:1:1," +
        "26:68:2:0:1:1," +
        "26:69:1:0:1:1," +
        "26:69:2:0:1:1," +
        "57:69:9:0:1:1," +
        "27:70:1:0:1:1," +
        "27:70:2:0:1:1," +
        "42:73.5:5.5:0:2:2:0," +
        "42:77.5:1.5:0:2:2:2," +
        "50:69:0:0:1:1:2:20";

    #endregion
    #region stage005
    private string stage5 = "28:5:4:0:1:1," +
        "28:5:5:0:1:1," +
        "28:6:4:0:1:1," +
        "28:6:5:0:1:1," +
        "28:7:4:0:1:1," +
        "28:7:5:0:1:1," +
        "28:7:5:0:1:1," +
        "57:8:0:0:1:1," +
        "28:8:4:0:1:1," +
        "28:8:5:0:1:1," +
        "28:11:6:0:1:1," +
        "28:11:7:0:1:1," +
        "28:12:6:0:1:1," +
        "28:12:7:0:1:1," +
        "28:13:6:0:1:1," +
        "28:13:7:0:1:1," +
        "57:14:0:0:1:1.25," +
        "28:14:6:0:1:1," +
        "28:14:7:0:1:1," +
        "28:17:6:0:1:1," +
        "28:17:7:0:1:1," +
        "28:18:6:0:1:1," +
        "28:18:7:0:1:1," +
        "28:19:6:0:1:1," +
        "28:19:7:0:1:1," +
        "57:20:0:0:1:1.5," +
        "28:20:6:0:1:1," +
        "28:20:7:0:1:1," +
        "42:23.5:6.5:0:2:2:0," +
        "28:26:6:0:1:1," +
        "28:26:7:0:1:1," +
        "42:27.5:3.5:0:2:2:0," +
        "28:27:6:0:1:1," +
        "35:27:7:0:1:1," +
        "28:28:6:0:1:1," +
        "28:28:7:0:1:1," +
        "42:30.5:6.5:0:2:2:0," +
        "26:30:8:0:1:1," +
        "0:30:9:0:1:1," +
        "26:31:8:0:1:1," +
        "28:31:9:0:1:1," +
        "26:32:8:0:1:1," +
        "28:32:9:0:1:1," +
        "26:33:8:0:1:1," +
        "28:33:9:0:1:1," +
        "26:34:3:0:1:1," +
        "8:34:4:0:1:1," +
        "26:35:2:0:1:1," +
        "28:35:3:0:1:1," +
        "26:35:4:0:1:1," +
        "26:36:1:0:1:1," +
        "35:36:2:0:1:1," +
        "26:36:3:0:1:1," +
        "26:37:0:0:1:1," +
        "28:37:1:0:1:1," +
        "26:37:2:0:1:1," +
        "28:38:0:0:1:1," +
        "26:38:1:0:1:1," +
        "17:39:0:0:1:1," +
        "36:41:9:135:1:1.25," +
        "35:44:7:0:1:1," +
        "28:45:7:0:1:1," +
        "28:46:7:0:1:1," +
        "28:47:6:0:1:1," +
        "28:48:6:0:1:1," +
        "28:48:5:0:1:1," +
        "28:49:5:0:1:1," +
        "35:54:2:0:1:1," +
        "15:55:1:0:1:1," +
        "28:56:0:0:1:1," +
        "28:57:0:0:1:1," +
        "36:57:9:180:1:1.25," +
        "28:58:0:0:1:1," +
        "28:59:0:0:1:1," +
        "28:60:3:0:1:1," +
        "26:60:4:0:1:1," +
        "28:60:5:0:1:1," +
        "28:61:3:0:1:1," +
        "11:61:4:0:1:1," +
        "28:61:5:0:1:1," +
        "28:62:3:0:1:1," +
        "26:62:4:0:1:1," +
        "28:62:5:0:1:1," +
        "26:65:0:0:1:1," +
        "26:65:1:0:1:1," +
        "26:65:7:0:1:1," +
        "26:65:8:0:1:1," +
        "28:66:0:0:1:1," +
        "28:66:1:0:1:1," +
        "28:66:7:0:1:1," +
        "28:66:8:0:1:1," +
        "28:67:0:0:1:1," +
        "28:67:1:0:1:1," +
        "28:67:7:0:1:1," +
        "28:67:8:0:1:1," +
        "28:68:0:0:1:1," +
        "28:68:1:0:1:1," +
        "28:68:7:0:1:1," +
        "28:68:8:0:1:1," +
        "28:69:0:0:1:1," +
        "28:69:1:0:1:1," +
        "28:69:7:0:1:1," +
        "28:69:8:0:1:1," +
        "0:70:0:0:1:1," +
        "28:70:1:0:1:1," +
        "42:66.5:3.5:0:2:2:0," +
        "42:70.5:5.5:0:2:2:0," +
        "28:72:4:0:1:1," +
        "13:73:4:0:1:1," +
        "28:74:4:0:1:1," +
        "28:75:5:0:1:1," +
        "26:76:5:0:1:1," +
        "28:76:6:0:1:1," +
        "36:77:0:0:1:1," +
        "26:77:5:0:1:1," +
        "4:77:6:0:1:1," +
        "42:48.5:2.5:0:2:2:1," +
        "42:52.5:0.5:0:2:2:1," +
        "50:39:0:0:1:1:1:20," +
        "42:52.5:5.5:0:2:2:0";
    #endregion
    #endregion
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
