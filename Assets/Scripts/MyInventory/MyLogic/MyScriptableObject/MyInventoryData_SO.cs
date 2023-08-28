using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyNew Inventory", menuName = "MyInventory/MyInventory Data")]
public class MyInventoryData_SO :ScriptableObject
{
    [SerializeField]
    public List<MyInventoryItem> myitems = new List<MyInventoryItem>();

    public void MyAddItem(MyItemData_SO newItemData, int amount)
    {
        for (int i = 0; i < myitems.Count; i++)
        {
            if (myitems[i].myitemData == null)
            {
                myitems[i].myitemData = newItemData;
                myitems[i].myamount = amount;
                break;
            }
        }
    }
    [System.Serializable]
    public class MyInventoryItem
    {
        public MyItemData_SO myitemData;
        public int myamount;
    }
}
