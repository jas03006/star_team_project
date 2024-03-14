using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage005 : MonoBehaviour
{
    [SerializeField] public List<string> StageList = new List<string>();
    [SerializeField] public int thisStage;          //스테이지 1~5 넘어올때 받아야함
    #region stage
    #region stage001
    private string stage1 = "36:8:2:0:1:2.5," +
        "35:10:5:0:1:1," +
        "28:11:0:0:1:1," +
        "28:11:1:0:1:1," +
        "28:11:5:0:1:1," +
        "7:12:0:0:1:1," +
        "28:12:1:0:1:1," +
        "28:12:5:0:1:1," +
        "28:13:5:0:1:1," +
        "28:14:5:0:1:1," +
        "38:15:0:35:1:1.25," +
        "28:15:5:0:1:1," +
        "36:15:9:-45:1:0.75," +
        "28:16:4:0:1:1," +
        "28:16:6:0:1:1," +
        "26:17:3:0:1:1," +
        "28:17:7:0:1:1," +
        "28:18:2:0:1:1," +
        "26:18:8:0:1:1," +
        "28:19:2:0:1:1," +
        "26:19:9:0:1:1," +
        "28:20:2:0:1:1," +
        "26:20:9:0:1:1," +
        "44:19.5:6.5:0:2:2:0," +
        "26:21:3:0:1:1," +
        "28:21:9:0:1:1," +
        "28:22:4:0:1:1," +
        "26:22:8:0:1:1," +
        "28:23:3:0:1:1," +
        "28:23:8:0:1:1," +
        "28:24:3:0:1:1," +
        "28:24:7:0:1:1," +
        "44:23.5:0.5:0:2:2:0," +
        "26:25:2:0:1:1," +
        "26:25:7:0:1:1," +
        "28:26:1:0:1:1," +
        "28:26:7:0:1:1," +
        "26:27:1:0:1:1," +
        "26:27:7:0:1:1," +
        "44:26.5:4.5:0:2:2:0," +
        "28:28:1:0:1:1," +
        "28:28:7:0:1:1," +
        "28:29:6:0:1:1," +
        "44:30.5:8.5:0:2:2:0," +
        "35:32:0:0:1:1," +
        "28:33:0:0:1:1," +
        "28:34:0:0:1:1," +
        "36:34:2:0:1:0.5," +
        "28:34:6:0:1:1," +
        "28:34:7:0:1:1," +
        "28:35:0:0:1:1," +
        "26:35:6:0:1:1," +
        "26:35:7:0:1:1," +
        "0:36:0:0:1:1," +
        "28:36:6:0:1:1," +
        "28:36:7:0:1:1," +
        "26:37:1:0:1:1," +
        "26:37:6:0:1:1," +
        "26:37:7:0:1:1," +
        "28:38:2:0:1:1," +
        "26:39:2:0:1:1," +
        "28:40:3:0:1:1," +
        "28:40:9:0:1:1," +
        "26:41:4:0:1:1," +
        "36:41:6:0:1:0.5," +
        "28:41:9:0:1:1," +
        "28:42:4:0:1:1," +
        "28:42:9:0:1:1," +
        "26:43:4:0:1:1," +
        "15:43:9:0:1:1," +
        "28:44:4:0:1:1," +
        "57:45:0:35:1:1," +
        "35:49:1:0:1:1," +
        "35:49:8:0:1:1," +
        "28:50:1:0:1:1," +
        "28:50:8:0:1:1," +
        "26:51:1:0:1:1," +
        "36:51:4:0:1:0.5," +
        "26:51:8:0:1:1," +
        "28:52:1:0:1:1," +
        "28:52:8:0:1:1," +
        "26:53:1:0:1:1," +
        "26:53:8:0:1:1," +
        "28:54:1:0:1:1," +
        "38:54:4:0:1:0.5," +
        "28:54:8:0:1:1," +
        "26:55:1:0:1:1," +
        "26:55:8:0:1:1," +
        "28:56:1:0:1:1," +
        "28:56:8:0:1:1," +
        "26:57:1:0:1:1," +
        "36:57:4:0:1:0.5," +
        "26:57:8:0:1:1," +
        "28:58:1:0:1:1," +
        "28:58:8:0:1:1," +
        "26:59:1:0:1:1," +
        "26:59:8:0:1:1," +
        "28:60:1:0:1:1," +
        "41:60:4:0:1:0.5," +
        "28:60:8:0:1:1," +
        "26:61:1:0:1:1," +
        "26:61:8:0:1:1," +
        "28:62:1:0:1:1," +
        "28:62:8:0:1:1," +
        "26:63:1:0:1:1," +
        "36:63:4:0:1:0.5," +
        "26:63:8:0:1:1," +
        "28:64:1:0:1:1," +
        "28:64:8:0:1:1," +
        "26:65:1:0:1:1," +
        "26:65:8:0:1:1," +
        "28:66:1:0:1:1," +
        "37:66:4:0:1:0.5," +
        "28:66:8:0:1:1," +
        "26:67:1:0:1:1," +
        "26:67:8:0:1:1," +
        "15:68:1:0:1:1," +
        "28:68:8:0:1:1," +
        "28:69:5:0:1:1," +
        "28:69:6:0:1:1," +
        "28:70:5:0:1:1," +
        "28:70:6:0:1:1," +
        "28:73:1:0:1:1," +
        "28:73:2:0:1:1," +
        "28:74:1:0:1:1," +
        "28:74:2:0:1:1," +
        "24:75:3:0:1:1," +
        "57:78:9:135:1:1.5," +
        "36:79:0:0:1:1," +
        "28:79:5:0:1:1," +
        "28:79:6:0:1:1," +
        "28:80:5:0:1:1," +
        "28:80:6:0:1:1";
    #endregion
    #region stage002
    private string stage2 = "38:3:9:220:1:1.25," +
        "28:4:2:0:1:1," +
        "28:5:3:0:1:1," +
        "28:6:4:0:1:1," +
        "28:7:5:0:1:1," +
        "36:8:0:0:1:1," +
        "28:8:5:0:1:1," +
        "28:9:5:0:1:1," +
        "28:10:5:0:1:1," +
        "4:11:5:0:1:1," +
        "28:12:5:0:1:1," +
        "36:13:0:0:1:1," +
        "28:13:5:0:1:1," +
        "28:14:5:0:1:1," +
        "28:15:4:0:1:1," +
        "28:16:3:0:1:1," +
        "28:17:2:0:1:1," +
        "26:18:1:0:1:1," +
        "38:18:9:135:1:1," +
        "28:19:5:0:1:1," +
        "35:20:0:0:1:1," +
        "28:20:5:0:1:1," +
        "26:21:0:0:1:1," +
        "28:21:5:0:1:1," +
        "28:22:0:0:1:1," +
        "28:22:5:0:1:1," +
        "28:23:0:0:1:1," +
        "42:23:2:0:3:3:0," +
        "28:23:5:0:1:1," +
        "28:24:0:0:1:1," +
        "28:24:5:0:1:1," +
        "28:25:5:0:1:1," +
        "26:26:2:0:1:1," +
        "26:26:4:0:1:1," +
        "28:27:2:0:1:1," +
        "28:27:9:0:1:1," +
        "28:28:3:0:1:1," +
        "42:28:7:0:3:3:0," +
        "28:28:9:0:1:1," +
        "28:29:4:0:1:1," +
        "23:29:9:0:1:1," +
        "28:30:4:0:1:1," +
        "42:31:1:0:3:3:0," +
        "28:31:4:0:1:1," +
        "28:32:4:0:1:1," +
        "28:32:6:0:1:1," +
        "28:32:7:0:1:1," +
        "28:33:4:0:1:1," +
        "28:33:6:0:1:1," +
        "28:33:7:0:1:1," +
        "28:34:4:0:1:1," +
        "26:34:6:0:1:1," +
        "26:34:7:0:1:1," +
        "28:37:2:0:1:1," +
        "28:37:3:0:1:1," +
        "28:38:2:0:1:1," +
        "2:38:3:0:1:1," +
        "38:42:0:0:1:1," +
        "26:42:6:0:1:1," +
        "26:42:7:0:1:1," +
        "28:43:6:0:1:1," +
        "28:43:7:0:1:1," +
        "28:44:6:0:1:1," +
        "8:44:7:0:1:1," +
        "28:45:6:0:1:1," +
        "28:45:7:0:1:1," +
        "26:46:5:0:1:1," +
        "57:47:0:0:1:0.5," +
        "28:47:5:0:1:1," +
        "28:48:4:0:1:1," +
        "28:49:4:0:1:1," +
        "36:49:9:220:1:1.5," +
        "36:50:0:0:1:0.75," +
        "28:50:3:0:1:0.75," +
        "28:53:0:0:1:1," +
        "28:53:1:0:1:1," +
        "28:54:0:0:1:1," +
        "19:54:1:0:1:1," +
        "35:55:3:0:1:1," +
        "26:56:4:0:1:1," +
        "26:56:5:0:1:1," +
        "28:57:4:0:1:1," +
        "28:57:5:0:1:1," +
        "28:58:4:0:1:1," +
        "28:58:5:0:1:1," +
        "38:59:0:35:1:1," +
        "28:60:6:0:1:1," +
        "28:60:7:0:1:1," +
        "28:61:6:0:1:1," +
        "28:61:7:0:1:1," +
        "36:64:2:0:1:2.5," +
        "35:66:2:0:1:1," +
        "26:66:7:0:1:1," +
        "26:66:8:0:1:1," +
        "28:67:1:0:1:1," +
        "4:67:8:0:1:1," +
        "28:68:0:0:1:1," +
        "28:69:0:0:1:1," +
        "28:70:0:0:1:1," +
        "28:71:0:0:1:1," +
        "42:70.5:1.5:0:2:2:0," +
        "28:72:0:0:1:1," +
        "28:73:0:0:1:1," +
        "28:74:1:0:1:1," +
        "28:74:8:0:1:1," +
        "28:75:2:0:1:1," +
        "28:75:7:0:1:1," +
        "28:75:8:0:1:1," +
        "42:74.5:4.5:0:2:2:0," +
        "3:76:2:0:1:1," +
        "28:76:6:0:1:1," +
        "28:76:7:0:1:1," +
        "26:77:5:0:1:1," +
        "28:77:6:0:1:1," +
        "26:78:5:0:1:1," +
        "42:79.5:8.5:0:2:2:0," +
        "42:38:7:0:3:3:1," +
        "50:30:0:0:1:1:1:20," +
        "56:70:9:135:1:1.5:2," +
        "50:64:0:0:1:1:2:20";
    #endregion
    #region stage003
    private string stage3 = "28:3:1:0:1:1," +
        "28:4:2:0:1:1," +
        "44:5:1:0:1:1:0," +
        "28:5:3:0:1:1," +
        "28:6:2:0:1:1," +
        "26:6:4:0:1:1," +
        "26:7:2:0:1:1," +
        "28:7:5:0:1:1," +
        "28:8:2:0:1:1," +
        "42:8:4:0:1:1:0," +
        "28:8:6:0:1:1," +
        "26:9:2:0:1:1," +
        "26:9:6:0:1:1," +
        "28:10:3:0:1:1," +
        "28:10:5:0:1:1," +
        "45:11:1:0:1:1:0" +
        "28:11:3:0:1:1," +
        "28:11:5:0:1:1," +
        "44:11:7:0:1:1:0" +
        "26:12:3:0:1:1," +
        "28:12:5:0:1:1," +
        "28:13:2:0:1:1," +
        "28:13:6:0:1:1," +
        "28:14:2:0:1:1," +
        "44:14:4:0:1:1:0," +
        "11:14:6:0:1:1," +
        "46:16:0:0:1:1:0," +
        "35:18:1:0:1:1," +
        "28:19:0:0:1:1," +
        "28:19:1:0:1:1," +
        "28:20:0:0:1:1," +
        "28:20:1:0:1:1," +
        "26:22:5:0:1:1," +
        "26:22:6:0:1:1," +
        "28:23:5:0:1:1," +
        "28:23:6:0:1:1," +
        "28:24:5:0:1:1," +
        "28:24:6:0:1:1," +
        "26:26:5:0:1:1," +
        "26:26:6:0:1:1," +
        "28:27:5:0:1:1," +
        "28:27:6:0:1:1," +
        "39:28:0:0:1:1," +
        "28:28:5:0:1:1," +
        "28:28:6:0:1:1," +
        "28:29:5:0:1:1," +
        "28:29:6:0:1:1," +
        "28:32:4:0:1:1," +
        "28:33:4:0:1:1," +
        "26:34:4:0:1:1," +
        "28:34:5:0:1:1," +
        "57:35:0:0:1:1," +
        "28:35:4:0:1:1," +
        "26:35:5:0:1:1," +
        "57:35:9:0:1:1," +
        "26:36:4:0:1:1," +
        "28:36:5:0:1:1," +
        "28:37:4:0:1:1," +
        "26:40:4:0:1:1," +
        "26:40:5:0:1:1," +
        "14:41:4:0:1:1," +
        "28:41:5:0:1:1," +
        "36:42:0:135:1:2.5," +
        "28:42:4:0:1:1," +
        "28:42:5:0:1:1," +
        "38:43:9:135:1:1," +
        "26:44:7:0:1:1," +
        "26:44:8:0:1:1," +
        "28:45:7:0:1:1," +
        "28:45:8:0:1:1," +
        "28:46:7:0:1:1," +
        "28:46:8:0:1:1," +
        "57:52:0:0:1:1.5," +
        "35:52:7:0:1:1," +
        "21:53:6:0:1:1," +
        "28:54:7:0:1:1," +
        "28:55:6:0:1:1," +
        "57:55:9:0:1:0.5," +
        "28:56:7:0:1:1," +
        "28:57:6:0:1:1," +
        "28:58:7:0:1:1," +
        "47:59:2:0:3:3:0," +
        "28:59:8:0:1:1," +
        "28:60:9:0:1:1," +
        "28:61:9:0:1:1," +
        "28:62:3:0:1:1," +
        "28:62:4:0:1:1," +
        "28:62:9:0:1:1," +
        "4:63:3:0:1:1," +
        "28:63:4:0:1:1," +
        "46:63:7:0:3:3:0," +
        "28:63:9:0:1:1," +
        "28:64:3:0:1:1," +
        "28:64:4:0:1:1," +
        "28:64:9:0:1:1," +
        "28:65:9:0:1:1," +
        "28:66:8:0:1:1," +
        "28:67:7:0:1:1," +
        "36:68:0:35:1:1," +
        "28:68:6:0:1:1," +
        "26:70:6:0:1:1," +
        "26:70:7:0:1:1," +
        "26:71:1:0:1:1," +
        "28:71:6:0:1:1," +
        "28:71:7:0:1:1," +
        "28:72:1:0:1:1," +
        "28:73:1:0:1:1," +
        "37:73:9:0:1:1.75," +
        "28:74:1:0:1:1," +
        "11:75:6:0:1:1," +
        "28:75:7:0:1:1," +
        "26:76:1:0:1:1," +
        "28:76:6:0:1:1," +
        "28:76:7:0:1:1," +
        "28:77:1:0:1:1," +
        "28:78:1:0:1:1," +
        "37:78:9:0:1:1.75," +
        "28:79:1:0:1:1," +
        "29:80:1:0:1:1," +
        "24:80:6:0:1:1," +
        "28:80:7:0:1:1," +
        "42:21:3:0:1:1:1," +
        "51:22:9:135:1:1.5:2," +
        "42:24:1:0:1:1:3," +
        "50:14:0:0:1:1:1:20," +
        "50:17:0:0:1:1:2:20," +
        "50:19:0:0:1:1:3:20";
    #endregion
    #region stage004
    private string stage4 = "28:3:0:0:1:1," +
        "28:4:0:0:1:1," +
        "47:4:2:0:3:3:0," +
        "28:5:0:0:1:1," +
        "28:7:0:0:1:1," +
        "28:7:5:0:1:1," +
        "28:7:6:0:1:1," +
        "28:8:0:0:1:1," +
        "42:8:2:0:3:3:0," +
        "28:8:5:0:1:1," +
        "28:8:6:0:1:1," +
        "28:9:0:0:1:1," +
        "28:9:5:0:1:1," +
        "28:9:6:0:1:1," +
        "28:11:0:0:1:1," +
        "28:11:5:0:1:1," +
        "28:11:6:0:1:1," +
        "28:12:0:0:1:1," +
        "42:12:2:0:3:3:0," +
        "28:12:5:0:1:1," +
        "28:12:6:0:1:1," +
        "28:13:0:0:1:1," +
        "28:13:5:0:1:1," +
        "28:13:6:0:1:1," +
        "35:17:3:0:1:1," +
        "28:19:4:0:1:1," +
        "28:19:5:0:1:1," +
        "28:20:4:0:1:1," +
        "18:20:5:0:1:1," +
        "28:21:4:0:1:1," +
        "28:21:5:0:1:1," +
        "36:22:0:35:1:1," +
        "28:23:3:0:1:1," +
        "26:24:2:0:1:1," +
        "28:25:1:0:1:1," +
        "26:26:1:0:1:1," +
        "40:27:9:0:1:1.75," +
        "28:29:1:0:1:1," +
        "28:30:0:0:1:1," +
        "26:30:1:0:1:1," +
        "28:30:2:0:1:1," +
        "28:31:0:0:1:1," +
        "26:31:1:0:1:1," +
        "28:31:2:0:1:1," +
        "28:32:1:0:1:1," +
        "45:35:4:0:3:3:0," +
        "35:36:1:0:1:1," +
        "28:37:1:0:1:1," +
        "26:38:2:0:1:1," +
        "28:39:3:0:1:1," +
        "26:40:4:0:1:1," +
        "45:41:1:0:3:3:0," +
        "28:41:4:0:1:1," +
        "45:41:7:0:3:3:0," +
        "26:42:4:0:1:1," +
        "28:43:4:0:1:1," +
        "28:44:5:0:1:1," +
        "28:45:1:0:1:1," +
        "28:45:6:0:1:1," +
        "28:46:1:0:1:1," +
        "28:46:6:0:1:1," +
        "28:47:0:0:1:1," +
        "28:47:7:0:1:1," +
        "28:48:0:0:1:1," +
        "28:48:7:0:1:1," +
        "28:49:8:0:1:1," +
        "28:50:8:0:1:1," +
        "35:51:2:0:1:1," +
        "28:51:9:0:1:1," +
        "28:52:3:0:1:1," +
        "28:52:4:0:1:1," +
        "28:52:9:0:1:1," +
        "57:53:0:0:1:0.5," +
        "28:53:3:0:1:1," +
        "28:53:4:0:1:1," +
        "26:53:9:0:1:1," +
        "0:54:3:0:1:1," +
        "26:54:9:0:1:1," +
        "28:60:5:0:1:1," +
        "37:61:0:35:1:1.75," +
        "28:61:4:0:1:1," +
        "28:62:3:0:1:1," +
        "28:64:1:0:1:1," +
        "28:64:2:0:1:1," +
        "28:64:8:0:1:1," +
        "28:64:9:0:1:1," +
        "47:65:5:0:3:3:0," +
        "26:67:1:0:1:1," +
        "26:67:2:0:1:1," +
        "26:67:8:0:1:1," +
        "26:67:9:0:1:1," +
        "28:69:1:0:1:1," +
        "28:69:2:0:1:1," +
        "28:69:8:0:1:1," +
        "28:69:9:0:1:1," +
        "42:70:5:0:3:3:0," +
        "28:72:1:0:1:1," +
        "28:72:2:0:1:1," +
        "28:72:8:0:1:1," +
        "28:72:9:0:1:1," +
        "28:74:1:0:1:1," +
        "28:74:2:0:1:1," +
        "28:74:8:0:1:1," +
        "28:74:9:0:1:1," +
        "45:75:5:0:3:3:0," +
        "26:77:1:0:1:1," +
        "26:77:2:0:1:1," +
        "26:77:8:0:1:1," +
        "26:77:9:0:1:1," +
        "28:79:1:0:1:1," +
        "28:79:2:0:1:1," +
        "3:79:8:0:1:1," +
        "28:79:9:0:1:1," +
        "28:80:1:0:1:1," +
        "28:80:2:0:1:1," +
        "42:80:5:0:3:3:0," +
        "28:80:8:0:1:1," +
        "28:80:9:0:1:1," +
        "51:19:9:135:1:1.5:1," +
        "42:48.5:3.5:0:2:2:2," +
        "42:53.5:6.5:0:2:2:3," +
        "50:12:0:0:1:1:1:20," +
        "50:43:0:0:1:1:2:20," +
        "50:48:0:0:1:1:3:20";

    #endregion
    #region stage005
    private string stage5 = "28:3:1:0:1:1," +
    "28:4:2:0:1:1," +
    "28:5:3:0:1:1," +
    "28:6:4:0:1:1," +
    "29:7:5:0:1:1," +
    "28:8:6:0:1:1," +
    "28:9:7:0:1:1," +
    "28:10:7:0:1:1," +
    "28:11:6:0:1:1," +
    "28:12:5:0:1:1," +
    "28:13:4:0:1:1," +
    "28:14:3:0:1:1," +
    "26:15:1:0:1:1," +
    "28:15:2:0:1:1," +
    "26:16:1:0:1:1," +
    "28:16:2:0:1:1," +
    "26:17:1:0:1:1," +
    "28:17:2:0:1:1," +
    "28:18:3:0:1:1," +
    "28:19:4:0:1:1," +
    "28:20:5:0:1:1," +
    "28:21:6:0:1:1," +
    "29:21:7:0:1:1," +
    "28:22:6:0:1:1," +
    "0:22:7:0:1:1," +
    "28:23:5:0:1:1," +
    "28:24:4:0:1:1," +
    "28:25:3:0:1:1," +
    "28:26:3:0:1:1," +
    "28:27:4:0:1:1," +
    "28:28:5:0:1:1," +
    "36:28:9:135:1:1.25," +
    "28:29:6:0:1:1," +
    "36:30:0:0:1:1," +
    "28:30:5:0:1:1," +
    "29:30:6:0:1:1," +
    "28:30:7:0:1:1," +
    "28:31:5:0:1:1," +
    "13:31:6:0:1:1," +
    "28:31:7:0:1:1," +
    "28:32:6:0:1:1," +
    "28:33:5:0:1:1," +
    "28:34:4:0:1:1," +
    "42:35:1:0:3:3:0," +
    "26:35:3:0:1:1," +
    "28:36:4:0:1:1," +
    "26:37:3:0:1:1," +
    "42:37:6:0:3:3:0," +
    "28:38:4:0:1:1," +
    "26:39:3:0:1:1," +
    "28:40:4:0:1:1," +
    "35:40:5:0:1:1," +
    "28:41:5:0:1:1," +
    "28:42:5:0:1:1," +
    "42:43:2:0:3:3:0," +
    "28:43:5:0:1:1," +
    "42:43:8:0:3:3:0," +
    "28:44:5:0:1:1," +
    "28:45:5:0:1:1," +
    "28:46:4:0:1:1," +
    "35:47:3:0:1:1," +
    "28:48:2:0:1:1," +
    "28:49:1:0:1:1," +
    "28:50:1:0:1:1," +
    "28:51:1:0:1:1," +
    "6:52:1:0:1:1," +
    "28:53:1:0:1:1," +
    "28:54:1:0:1:1," +
    "28:55:2:0:1:1," +
    "28:56:3:0:1:1," +
    "28:57:4:0:1:1," +
    "28:58:5:0:1:1," +
    "28:59:6:0:1:1," +
    "28:60:7:0:1:1," +
    "29:61:8:0:1:1," +
    "17:62:7:0:1:1," +
    "28:63:6:0:1:1," +
    "28:64:5:0:1:1," +
    "26:65:4:0:1:1," +
    "28:66:5:0:1:1," +
    "28:67:6:0:1:1," +
    "28:68:7:0:1:1," +
    "57:69:0:0:1:1.25," +
    "28:69:7:0:1:1," +
    "28:70:6:0:1:1," +
    "26:71:5:0:1:1," +
    "26:72:5:0:1:1," +
    "26:73:5:0:1:1," +
    "28:74:6:0:1:1," +
    "28:75:6:0:1:1," +
    "28:76:6:0:1:1," +
    "24:77:6:0:1:1," +
    "28:78:6:0:1:1," +
    "28:79:6:0:1:1," +
    "28:80:5:0:1:1," +
    "28:81:5:0:1:1," +
    "58:8:0:0:1:1.5:1," +
    "58:16:9:0:1:1.5:2," +
    "44:22.5:1.5:0:2:2:3," +
    "51:52:9:0:1:2:4," +
    "51:61:0:0:1:2:5," +
    "58:77:0:0:1:2:6," +
    "47:71.5:7.5:0:2:2:6," +
    "50:5:0:0:1:1:1:20," +
    "50:12:0:0:1:1:2:20," +
    "50:17:0:0:1:1:3:20," +
    "50:12:0:0:1:1:4:20," +
    "50:47:0:0:1:1:5:20," +
    "50:74:0:0:1:1:6:20";
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