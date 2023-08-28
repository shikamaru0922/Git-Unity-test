using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBagUI : MonoBehaviour
{
    public List<GeziUI> MyBaglist;//MyBaglist存放每个格子对象的geziui脚本对象。
    public void MyRefreshUI()//
    {
        for(int i = 0; i < MyBaglist.Count; i++)
        {
            MyBaglist[i].myitemUI.Myindex = i;//将geziui的脚本对象的序号对应每一个item资源文件的序号
            MyBaglist[i].MyUpdateItem();
        }
    }
}
