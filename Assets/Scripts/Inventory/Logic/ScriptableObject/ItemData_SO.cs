using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Usable, Weapon, Armor }
[CreateAssetMenu (fileName = "New Item" , menuName = "Inventory/Item Data")]
public class ItemData_SO : ScriptableObject
{
    public ItemType ItemType;
    public string itemName;
    public Sprite itemIcon;
    public int itemAmount;

    [TextArea]
    public string description = "";

    public bool stackable ;//�Ƿ�ɶѵ���Ҳ�����Ƿ����������ӡ����ٲ���

    [Header("Usable Item")]
    public UseableItemData_SO useableData;

    [Header("Weapon")]
    public GameObject weaponPrefab;
    public AttackData_SO weaponData;
    public AnimatorOverrideController weaponAnimator;
   
}
