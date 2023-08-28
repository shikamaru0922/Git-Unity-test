using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UIType
{
    MainView, LoginView, SettingView, CharacterView
}
public class UIManager
{
    private static UIManager _instance;
    public static UIManager Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
                UIViewInfoList InfoList = Resources.Load<UIViewInfoList>("UIViewInfo_Config");
                foreach(UIViewInfo item in InfoList.List)
                {
                    _instance.allPaths.Add(item.uiView, item.uiPath);
                }
            }
            return _instance;
        }
    }
    
    public Dictionary<UIType, string> allPaths = new Dictionary<UIType, string>();//键是枚举UI类型，值是路径
    public Dictionary<UIType, GameObject> allViews = new Dictionary<UIType, GameObject>();//键是枚举UI类型，值是对象Canvas。

    public void OpenView(UIType uiType)
    {
        string path = _instance.allPaths[uiType];
        if (_instance.allViews.ContainsKey(uiType))
        {
            allViews[uiType].SetActive(true);
            allViews[uiType].GetComponent<Canvas>().sortingOrder = GetMaxSortingOrder() + 1;
        }
        else
        {
            GameObject prefab = Resources.Load<GameObject>(path);//获取预制体
            GameObject clonePop = GameObject.Instantiate(prefab);//克隆对象
            clonePop.transform.SetParent(GameObject.Find("UICanvasParent").transform);//设置父物体
            clonePop.GetComponent<RectTransform>().anchorMin = Vector2.zero;//动态更新需要设置锚点。
            clonePop.GetComponent<RectTransform>().anchorMax = Vector2.one;
            clonePop.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            clonePop.transform.localPosition = Vector3.zero;
            clonePop.transform.localScale = Vector3.one;
            clonePop.GetComponent<Canvas>().sortingOrder = GetMaxSortingOrder() + 1;
            allViews.Add(uiType, clonePop);
        }
    }
    public void CloseView(UIType uiType)
    {
        if (allViews.ContainsKey(uiType))
        {
            allViews[uiType].SetActive(false);
        }
    }
    public GameObject GetWindow(UIType uiType)
    {
        if (allViews.ContainsKey(uiType))
        {
            return allViews[uiType];
        }
        else
            return null;
    }
    public void CloseAllViews()
    {
        foreach(var key in allViews.Keys)
        {
            CloseView(key);
        }
    }
    private int GetMaxSortingOrder()
    {
        int max = 0;
        foreach (var item in allViews)
        {
            if (item.Value.gameObject.activeInHierarchy)
            {
                int order = item.Value.gameObject.GetComponent<Canvas>().sortingOrder;
                if (max < order)
                    max = order;
            }
        }
        return max; 
    }
}
