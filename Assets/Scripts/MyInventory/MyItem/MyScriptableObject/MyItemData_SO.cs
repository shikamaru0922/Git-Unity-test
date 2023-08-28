using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyNew Item", menuName = "MyInventory/MyItem Data")]
public class MyItemData_SO : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int itemAmount=1;
    public ShootObject shootObject;
    [TextArea]
    public string description = "";
    
    public string Setdescription()
    {
        string str=description;
        Debug.Log("Dic");
        switch (shootObject.objtype)
        {
            case ShootObject.Objtype.bullet:
                str = "Spend��" + shootObject.spend + "\n" + "Recover��" + shootObject.recover + "\n" + "FireGap��" + shootObject.fireGap + "\n" + "Energy��" + shootObject.energy + "\n";
                break;
            case ShootObject.Objtype.magic:
                str = "Spend��" + shootObject.spend + "\n" + "Recover��" + shootObject.recover + "\n" + "FireGap��" + shootObject.fireGap + "\n" + "Energy��" + shootObject.energy + "\n";
                break;
            case ShootObject.Objtype.trigger:
                str = "Spend��" + shootObject.spend + "\n" + "Recover��" + shootObject.recover + "\n" + "FireGap��" + shootObject.fireGap + "\n" + "Energy��" + shootObject.energy + "\n";
                break;
            case ShootObject.Objtype.modified:
                str = "Spend��" + shootObject.spend + "\n" + "Recover��" + shootObject.recover + "\n" + "FireGap��" + shootObject.fireGap + "\n" + "Energy��" + shootObject.energy + "\n";
                break;
        }
        str = str + "\n" + description;
        return str;
    }
}
