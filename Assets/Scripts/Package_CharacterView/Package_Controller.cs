using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package_Controller : MonoBehaviour
{
    Package_View view;
    public MyBagUI mybagUI;
    // Start is called before the first frame update
    void Awake()
    {
        view = GetComponent<Package_View>();
        InitItem();
    }
   
    //初始化动态生成
    void InitItem()
    {
        MyBagUI myBagUI = this.GetComponentInChildren<MyBagUI>();
        DataManager.Instance.all_geziItems.Clear();
        GameObject prefab = Resources.Load<GameObject>("Item/Bag_gezi");
        for(int i = 0; i < 10; i++)
        {
            GameObject clone = Instantiate(prefab);
            clone.transform.SetParent(view.contentParent);
            clone.name = i.ToString();
            clone.transform.localPosition = Vector3.zero;
            clone.transform.localScale = Vector3.one;
            GeziUI geziUI = clone.GetComponent<GeziUI>();
            myBagUI.MyBaglist.Add(geziUI);
            //mybagUI.MyRefreshUI();
            //格子里的数据refresh刷新显示
            //GeziItem geziItem = clone.GetComponent<GeziItem>();//获取格子身上的脚本
            //geziItem.geziData = DataManager.Instance.all_geziDatas[i];//取出集合数据,赋值给格子
            //geziItem.Refresh();//刷新格子面板
            //DataManager.Instance.all_geziItems.Add(geziItem);//格子添加到list里面
        }
    }
    
    // Update is called once per frame
    void Update()
    {
      
    }
}
