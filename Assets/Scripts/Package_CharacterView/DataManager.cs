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
    public List<GeziItem> all_geziItems = new List<GeziItem>();//������и��ӣ�����Ҫ��gameobject��ÿ�����󶼻������ű���
    public List<GeziData> all_geziDatas = new List<GeziData>();//������и�������
}
