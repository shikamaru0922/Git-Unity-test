using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    public KeyCode actionKey;
    private SoltHolder currentSoltHolder;

    private void Awake()
    {
        currentSoltHolder = GetComponent<SoltHolder>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(actionKey) && currentSoltHolder.itemUI.GetItem())
        {
            currentSoltHolder.UseItem();
        }
    }
}
