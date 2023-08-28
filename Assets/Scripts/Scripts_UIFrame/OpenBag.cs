using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBag : MonoBehaviour
{
    public GameObject mybag;
    public bool isopen = false;
    // Start is called before the first frame update
    private void Awake()
    {
        mybag.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OpenMyBag();
    }
    void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            isopen = !isopen;
            if (isopen)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
            mybag.SetActive(isopen);
            
        }
    }
}
