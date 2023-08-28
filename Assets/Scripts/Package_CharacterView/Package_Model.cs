using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package_Model : MonoBehaviour
{
    private void Awake()
    {
        InitData();
    }
    void InitData()
    {
        //DataManager.Instance.all_geziDatas.Clear();//先清空后赋值。
        //for(int i = 0; i < 10; i++)
        //{
        //    GeziData geziData = new GeziData();//就像是学生
        //    int index = Random.Range(0, 6);
        //    if (i > 1 && i < 6)
        //    {
        //        geziData.hasItem = false;
        //    }
        //    else
        //    {
        //        geziData.hasItem = true;
        //        geziData.num = Random.Range(1, 11);
        //        geziData.id = index;
        //        geziData.iconPath = "Icon/Misc_" + index;//这里加入学生的一系列数据
        //    }
        //    DataManager.Instance.all_geziDatas.Add(geziData);//将学生加入List
        //}
    }
}
