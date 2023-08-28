using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager 
{
    private static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataManager();
            }
            return _instance;
        }
    }
    public List<GeziItem> all_geziItems = new List<GeziItem>();//存放所有格子，不需要是gameobject，每个对象都会带这个脚本的
    public List<GeziData> all_geziDatas = new List<GeziData>();//存放所有格子数据
}
