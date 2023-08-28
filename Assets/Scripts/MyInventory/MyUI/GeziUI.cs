using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum SoltType { BAG, EQUIPMENT}
public class GeziUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public SoltType soltType;
    public MyItemUI myitemUI;
    public void MyUpdateItem()
    {
        switch (soltType) 
        {
            case SoltType.BAG:
                myitemUI.MyBag = MyInventoryManager.Instance.myInventoryData;//将唯一的那个背包资源文件对应
                break;
            case SoltType.EQUIPMENT:
                myitemUI.MyBag = MyInventoryManager.Instance.myEquipmentData;
                break;
        }
        var myitem = myitemUI.MyBag.myitems[myitemUI.Myindex];
        myitemUI.MySetUpItemSlotUI(myitem.myitemData, myitem.myamount);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (myitemUI.MyGetItem())
        {
            MyInventoryManager.Instance.MyItemToolTip.SetUpToolTip(myitemUI.MyGetItem());
            MyInventoryManager.Instance.MyItemToolTip.gameObject.SetActive(true);
           
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MyInventoryManager.Instance.MyItemToolTip.gameObject.SetActive(false);
    }

    //public static implicit operator GeziUI(GameObject v)
    //{
    //    throw new NotImplementedException();
    //}
}
