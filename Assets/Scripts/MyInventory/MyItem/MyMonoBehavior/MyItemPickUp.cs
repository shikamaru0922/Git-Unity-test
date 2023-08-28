using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItemPickUp : MonoBehaviour
{
    public MyItemData_SO myitemData;
    public MagicList_SO magicListSO;
    private List<MyItemData_SO> magicList;

    //private List<ShootObject> shootObjectsList;

    private void Start()
    {
        magicList = magicListSO.Magiclist;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            //Debug.Log("picked up");
            

            if (magicList.Count != 0)
            {
                int i = Random.Range(0, magicList.Count);
                //Debug.Log(i);
                myitemData = magicList[i];

            }

            MyInventoryManager.Instance.myInventoryData.MyAddItem(myitemData, myitemData.itemAmount);
            MyInventoryManager.Instance.myInventoryUI.MyRefreshUI();
            Destroy(gameObject);
        }
    }
}
