using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager //对象池管理类
{
    //单例模式实现
    private static PoolManager _instance;
    public static PoolManager Instance
    {
        get 
        {
            if (_instance == null)
            {
                _instance = new PoolManager();
            }
            return _instance;
        }
    }

    //所有的对象池的集合，key:是物体的加载路径，value:是某一个对象池类
    Dictionary<string, Pool> allPools = new Dictionary<string, Pool>();

    //获取对象的接口
    public GameObject GetPoolGameObject(string prefabPath,int max=999)//根据路径，从对象池中查找对象
    {
        Pool pool = null;
        if (allPools.ContainsKey(prefabPath))//从所有的池子列表中去查找，获取到某一个已经存在的对象池
        {
            pool = allPools[prefabPath];
        }
        else//还没有加入到列表中，则是第一次Add添加到集合中
        {
            pool = new Pool();
            pool.prefabPath = prefabPath;
            allPools.Add(prefabPath,pool);
        }
        pool.maxCount = max;
        GameObject obj = pool.GetObj();

        if (obj != null)//因为只有10个对象，再去查找第11个对象不存在，为null,所以添加代码保护，不为空，才需要显示激活
        {
            //obj.SetActive(true);//使用时激活显示就可以 
        }
        return obj;
    }

    //回收对象接口
    public void ReleasePoolGameObject(string prefabPath,GameObject obj)
    {
        Pool pool = allPools[prefabPath];//获取到:要释放的对象所在的对象池
        pool.ReleaseObj(obj);
        obj.SetActive(false) ;
        //调用封装的单独的对象池类中的方法ReleaseObj，释放某一个对象（也就是标识为：空闲状态）
    }
    public void ClearPoolItemForLoadingScene()
    {
        foreach(Pool i in allPools.Values)
        {
            i.ClearPool();
        }
    }
}
