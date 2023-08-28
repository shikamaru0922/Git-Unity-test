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
    
    public Dictionary<UIType, string> allPaths = new Dictionary<UIType, string>();//����ö��UI���ͣ�ֵ��·��
    public Dictionary<UIType, GameObject> allViews = new Dictionary<UIType, GameObject>();//����ö��UI���ͣ�ֵ�Ƕ���Canvas��

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
            GameObject prefab = Resources.Load<GameObject>(path);//��ȡԤ����
            GameObject clonePop = GameObject.Instantiate(prefab);//��¡����
            clonePop.transform.SetParent(GameObject.Find("UICanvasParent").transform);//���ø�����
            clonePop.GetComponent<RectTransform>().anchorMin = Vector2.zero;//��̬������Ҫ����ê�㡣
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
