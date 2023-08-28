using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magiclist", menuName = "Magiclist/SO/MagiclistSO")]

public class MagicList_SO : ScriptableObject
{
    public List<MyItemData_SO> Magiclist;
}

