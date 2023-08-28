using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInventoryManager : Singleton<MyInventoryManager>
{
    public class DragData 
    {
        public GeziUI originalGeziUI;
        public RectTransform originalParent;
    }

    [Header("MyInventory Data")]
    public MyInventoryData_SO myInventoryData;//������������Դ�ļ�������item��Դ�ļ�������
    public MyInventoryData_SO myEquipmentData;

    [Header("MyBagS")]
    public MyBagUI myInventoryUI;//����MyBagUI����ű�ʵ��
    public MyBagUI myEquipmentUI;

    [Header("Drag Canvas")]
    public Canvas dragCanvas;

    public DragData currentDrag;

    [Header("ToolTip")]
    public MyItemToolTip MyItemToolTip;

    //[Header("GetData")]
    //public Getdata getdata;
    private void Start()
    {
        myInventoryUI.MyRefreshUI();
        myEquipmentUI.MyRefreshUI();
    }
    
    //��鱳��
    public bool CheckInventoryUI(Vector3 position)
    {
        for(int i = 0; i < myInventoryUI.MyBaglist.Count; i++)
        {
            RectTransform t = myInventoryUI.MyBaglist[i].transform as RectTransform;
            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckEquipmentUI(Vector3 position)
    {
        for (int i = 0; i < myEquipmentUI.MyBaglist.Count; i++)
        {
            RectTransform t = myEquipmentUI.MyBaglist[i].transform as RectTransform;
            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
            {
                return true;
            }
        }
        return false;
    }
}
