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
   
    //��ʼ����̬����
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
            //�����������refreshˢ����ʾ
            //GeziItem geziItem = clone.GetComponent<GeziItem>();//��ȡ�������ϵĽű�
            //geziItem.geziData = DataManager.Instance.all_geziDatas[i];//ȡ����������,��ֵ������
            //geziItem.Refresh();//ˢ�¸������
            //DataManager.Instance.all_geziItems.Add(geziItem);//������ӵ�list����
        }
    }
    
    // Update is called once per frame
    void Update()
    {
      
    }
}
