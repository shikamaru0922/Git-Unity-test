using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBagUI : MonoBehaviour
{
    public List<GeziUI> MyBaglist;//MyBaglist���ÿ�����Ӷ����geziui�ű�����
    public void MyRefreshUI()//
    {
        for(int i = 0; i < MyBaglist.Count; i++)
        {
            MyBaglist[i].myitemUI.Myindex = i;//��geziui�Ľű��������Ŷ�Ӧÿһ��item��Դ�ļ������
            MyBaglist[i].MyUpdateItem();
        }
    }
}
