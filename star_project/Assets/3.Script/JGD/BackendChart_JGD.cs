using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BackEnd;
public class BackendChart_JGD : MonoBehaviour
{
    private static BackendChart_JGD instance= null;

    public static BackendChart_JGD Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BackendChart_JGD();
            }
            return instance;
        }
    }

    public void ChartGet(string chartId)
    {
        //차트 정보 가져오기
    }
}