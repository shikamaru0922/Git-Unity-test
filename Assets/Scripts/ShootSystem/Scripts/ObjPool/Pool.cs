using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool //对象池
{
    public string prefabPath;//注意：把要动态加载的资源对象放在 Resources文件夹下
    public ObjFactory bulletObjFc = new BulletObjFc();
    public int maxCount = 99999999;//对象池的最大容量是10个
    int count = 10;
    List<GameObject> usingList = new List<GameObject>();//正在使用的列表
    List<GameObject> freeList = new List<GameObject>();//空闲的列表

    public GameObject GetObj()//从对象池中获得一个对象
    {
        GameObject obj = null;
        if (freeList.Count > 0)//空闲列表中有对象，直接取出来使用，添加到usingList正在使用的集合列表中
        {
            obj = freeList[0];//顺序取出空闲的对象
            usingList.Add(obj);
            freeList.RemoveAt(0);
        }
        else
        {
            if (usingList.Count >= maxCount)
            {
                return null;
            }

            //while (count > 0)
            //{
                
            //    count--;
            //}
            //count = 10;
            obj = bulletObjFc.CreatObj(prefabPath);
            
            usingList.Add(obj);
            //没有空闲可以使用的对象，就克隆一个对象出来

        }
        InitObj(obj);
        return obj;
    }

    public void ReleaseObj(GameObject obj)//对象没有在使用，则放回空闲列表中
    {
        ReLoad(obj);
        usingList.Remove(obj);
        freeList.Add(obj);
    }
    void ReLoad(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
            return;
        rb.Sleep();
        rb.rotation = Quaternion.identity;
        rb.transform.rotation = Quaternion.identity;
    }
    void InitObj(GameObject obj)
    {
        obj.GetComponent<BulletEventController>().Init();
    }
    public void ClearPool()
    {
        usingList.Clear();
        freeList.Clear();
    }
}
