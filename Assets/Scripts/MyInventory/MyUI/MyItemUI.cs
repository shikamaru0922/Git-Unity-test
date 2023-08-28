using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyItemUI : MonoBehaviour
{
    public Image icon = null;
    public Text amount = null;

    public MyInventoryData_SO MyBag { get; set; }
    public int Myindex { get; set; } = -1;
    public void MySetUpItemSlotUI(MyItemData_SO item, int itemAmount)//将资源文件里的数据给每一个ui对象，参数是背包资源文件的每一个item资源文件和数量
    {
        if (item != null)
        {
            icon.sprite = item.itemIcon;
            amount.text = itemAmount.ToString();
            icon.gameObject.SetActive(true);
        }
        else
        {
            icon.gameObject.SetActive(false);
        }
    }

    public MyItemData_SO MyGetItem()
    {
        return MyBag.myitems[Myindex].myitemData;
    }
}
