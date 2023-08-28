using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerUI : MonoBehaviour
{
    public SoltHolder[] soltHolders;
    public void RefreshUI()
    {
        for(int  i = 0; i<soltHolders.Length; i++)
        {
            soltHolders[i].itemUI.Index = i;
            soltHolders[i].UpdateItem();
        }
    }
}
