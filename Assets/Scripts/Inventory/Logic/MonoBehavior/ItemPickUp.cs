using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData_SO itemData;
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            //TODO: add item to backpack
            //InventoryManager.Instance.inventoryData.AddItem(itemData, itemData.itemAmount);
            //Equip weapon
            //GameManager.Instance.playerStats.EquipWeapon(itemData);
            

            //数据传出



            Destroy(gameObject);

            


            //Destroy(gameObject);
        }
    }
}
