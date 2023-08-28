using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MyDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    MyItemUI myCurrentItemUI;
    GeziUI myCurrentGeziUI;
    GeziUI myTargetGeziUI;
    private void Awake()
    {
        myCurrentItemUI = GetComponent<MyItemUI>();
        myCurrentGeziUI = GetComponentInParent<GeziUI>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //��¼ԭʼ��Ϣ
        MyInventoryManager.Instance.currentDrag = new MyInventoryManager.DragData();
        MyInventoryManager.Instance.currentDrag.originalGeziUI = GetComponentInParent<GeziUI>();
        MyInventoryManager.Instance.currentDrag.originalParent =(RectTransform)transform.parent;
        transform.SetParent(MyInventoryManager.Instance.dragCanvas.transform,true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //������Ʒ�ƶ�
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //������Ʒ����������
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (MyInventoryManager.Instance.CheckInventoryUI(eventData.position)||MyInventoryManager.Instance.CheckEquipmentUI(eventData.position))
            {
                if (eventData.pointerEnter.gameObject.GetComponent<GeziUI>())
                    myTargetGeziUI = eventData.pointerEnter.gameObject.GetComponent<GeziUI>();
                else
                    myTargetGeziUI = eventData.pointerEnter.gameObject.GetComponentInParent<GeziUI>();
            }
            switch (myTargetGeziUI.soltType) 
            {
                case SoltType.BAG:
                    SwapItem();
                    break;
                case SoltType.EQUIPMENT:
                    SwapItem();
                    break;
            }
            myCurrentGeziUI.MyUpdateItem();
            myTargetGeziUI.MyUpdateItem();
        }
        else
        {
            transform.position = MyInventoryManager.Instance.currentDrag.originalParent.position;
        }
        transform.SetParent(MyInventoryManager.Instance.currentDrag.originalParent);
        transform.localPosition = Vector3.zero;
    }
    public void SwapItem()
    {
        var TargetItem = myTargetGeziUI.myitemUI.MyBag.myitems[myTargetGeziUI.myitemUI.Myindex];
        var TempItem = myCurrentGeziUI.myitemUI.MyBag.myitems[myCurrentGeziUI.myitemUI.Myindex];

        //bool SameItem = TargetItem.myitemData == TempItem.myitemData;
        //if (SameItem)//����
        //{
        //    TargetItem.myamount += TempItem.myamount;
        //    TempItem.myitemData = null;
        //    TempItem.myamount = 0;
        //}
        //����
        {
            myCurrentGeziUI.myitemUI.MyBag.myitems[myCurrentGeziUI.myitemUI.Myindex] = TargetItem;
            myTargetGeziUI.myitemUI.MyBag.myitems[myTargetGeziUI.myitemUI.Myindex] = TempItem;
        }
        Getdata.Instance.Getmydata();
    }
}
