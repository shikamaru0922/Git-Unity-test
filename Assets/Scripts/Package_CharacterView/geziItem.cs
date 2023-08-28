using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GeziData 
{
    public bool hasItem;//是否有物品
    public string name;//物品名字
    public int id;//物品编号
    public string iconPath;//物品Icon
    public int num;//物品数量
    public string description;
}

public class GeziItem : MonoBehaviour
{
    public GeziData geziData;
    public Image icon;
    public Text num;
    public Text description;
    
    public void Refresh()//当数据发生改变，调用此方法
    {
        if (geziData == null || geziData.hasItem == false)
        {
            num.gameObject.SetActive(false);//隐藏
            icon.gameObject.SetActive(false);
            return;
        }
        num.gameObject.SetActive(true);//显示
        icon.gameObject.SetActive(true);
        num.text = geziData.num.ToString();//数据对应,geziData里的num数据给text。
        icon.sprite = Resources.Load<GameObject>(geziData.iconPath).GetComponent<SpriteRenderer>().sprite;//图标对应，根据gezidata里的路径得到图片。
        description.text = geziData.description;
    }
}

