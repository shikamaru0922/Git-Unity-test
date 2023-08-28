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
    public void MySetUpItemSlotUI(MyItemData_SO item, int itemAmount)//����Դ�ļ�������ݸ�ÿһ��ui���󣬲����Ǳ�����Դ�ļ���ÿһ��item��Դ�ļ�������
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
