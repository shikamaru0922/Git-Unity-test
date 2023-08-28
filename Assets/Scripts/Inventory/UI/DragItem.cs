using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemUI))]
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    ItemUI currentItemUI;
    SoltHolder currentHolder;
    SoltHolder targetHolder;

    private void Awake()
    {
        currentItemUI = GetComponent<ItemUI>();
        currentHolder = GetComponentInParent<SoltHolder>();
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        InventoryManager.Instance.currentDrag = new InventoryManager.DragData();
        InventoryManager.Instance.currentDrag.origianalHolder = GetComponentInParent<SoltHolder>();
        InventoryManager.Instance.currentDrag.origianalParent = (RectTransform)transform.parent;
        
        //recored original data
        transform.SetParent(InventoryManager.Instance.dragCanvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //follow mouse position
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //drop item,exchange data
        //whether pointed to UI object
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (InventoryManager.Instance.CheckInActionUI(eventData.position) || InventoryManager.Instance.CheckEquipmentUI(eventData.position) ||
            InventoryManager.Instance.CheckInInventoryUI(eventData.position))
            {
                if (eventData.pointerEnter.gameObject.GetComponent<SoltHolder>())
                    targetHolder = eventData.pointerEnter.gameObject.GetComponent<SoltHolder>();
                else
                    targetHolder = eventData.pointerEnter.gameObject.GetComponentInParent<SoltHolder>();

                //IF the holder is orangional holder
            if(targetHolder != InventoryManager.Instance.currentDrag.origianalHolder)
                switch (targetHolder.solType)
                {
                    case SolType.BAG:
                        SwapItem();
                        break;
                    case SolType.WEAPON:
                        if (currentItemUI.Bag.items[currentItemUI.Index].itemData.ItemType == ItemType.Weapon)
                            SwapItem();
                        break;
                    case SolType.ARMOR:
                        if (currentItemUI.Bag.items[currentItemUI.Index].itemData.ItemType == ItemType.Armor)
                            SwapItem();
                        break;
                    case SolType.ACTION:
                        if(currentItemUI.Bag.items[currentItemUI.Index].itemData.ItemType == ItemType.Usable)
                            SwapItem();
                        break;
                }

                currentHolder.UpdateItem();
                targetHolder.UpdateItem();
            }
        }
        transform.SetParent(InventoryManager.Instance.currentDrag.origianalParent);

        RectTransform t = transform as RectTransform;

        t.offsetMax = -Vector2.one * 5;
        t.offsetMin = Vector2.one * 5;

    }

    public void SwapItem()
    {
        var targetItem = targetHolder.itemUI.Bag.items[targetHolder.itemUI.Index];//fist pick up thing
        var tempItem = currentHolder.itemUI.Bag.items[currentHolder.itemUI.Index];//was swaped thing which place will change to targetItem's Place
        bool isSameItem = tempItem.itemData == targetItem.itemData;
        
        if (isSameItem && targetItem.itemData.stackable)
        {
            targetItem.amount += tempItem.amount;
            tempItem.itemData = null;
            tempItem.amount = 0;
        }
        else
        {
            currentHolder.itemUI.Bag.items[currentHolder.itemUI.Index] = targetItem;
            targetHolder.itemUI.Bag.items[targetHolder.itemUI.Index] = tempItem;
        }
    }
}
                
