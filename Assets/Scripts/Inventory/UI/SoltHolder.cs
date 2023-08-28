using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SolType { BAG,WEAPON,ARMOR,ACTION}
public class SoltHolder : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public SolType solType;
    public ItemUI itemUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount % 2 == 0)
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        if(itemUI.GetItem() != null)
        if(itemUI.GetItem().ItemType == ItemType.Usable  && itemUI.Bag.items[itemUI.Index].amount >0)
        {
            GameManager.Instance.playerStats.ApplyHealth(itemUI.GetItem().useableData.healthPoint);

            itemUI.Bag.items[itemUI.Index].amount -= 1;

                //check quest item update progress
                //QuestManager.Instance.UpDateQuestProgress(itemUI.GetItem().itemName, -1);
        }
        UpdateItem();
    }

    public void UpdateItem()
    {
        switch(solType)
        {
            case SolType.BAG:
                itemUI.Bag = InventoryManager.Instance.inventoryData;
                break;
            case SolType.WEAPON:
                itemUI.Bag = InventoryManager.Instance.equipmentData;
                //equipment my weapon change weapon
                if (itemUI.Bag.items[itemUI.Index].itemData != null)
                {
                    GameManager.Instance.playerStats.ChangeWeapon(itemUI.Bag.items[itemUI.Index].itemData);
                }
                else
                {
                    GameManager.Instance.playerStats.UnEquipWeapon();
                }
                break;
            case SolType.ARMOR:
                itemUI.Bag = InventoryManager.Instance.equipmentData;
                break;
            case SolType.ACTION:
                itemUI.Bag = InventoryManager.Instance.actionData;
                break;
        }

        var item = itemUI.Bag.items[itemUI.Index];
        itemUI.SetupItemUI(item.itemData, item.amount);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemUI.GetItem())
        {
            InventoryManager.Instance.tooltip.SetupTooltip(itemUI.GetItem());
            InventoryManager.Instance.tooltip.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.Instance.tooltip.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        InventoryManager.Instance.tooltip.gameObject.SetActive(false);
    }
}
