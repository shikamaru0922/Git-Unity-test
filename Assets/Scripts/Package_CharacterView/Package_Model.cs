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
        //DataManager.Instance.all_geziDatas.Clear();//����պ�ֵ��
        //for(int i = 0; i < 10; i++)
        //{
        //    GeziData geziData = new GeziData();//������ѧ��
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
        //        geziData.iconPath = "Icon/Misc_" + index;//�������ѧ����һϵ������
        //    }
        //    DataManager.Instance.all_geziDatas.Add(geziData);//��ѧ������List
        //}
    }
}
