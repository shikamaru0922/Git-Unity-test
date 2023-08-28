using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIRootDontDestroy : MonoBehaviour
{
    public static UIRootDontDestroy uiRootDontDestroy;//当前脚本所在对象

    public GameObject failed;
    public GameObject success;
    public GameObject EndWindow;
    public Slider slider_mp;
    // Start is called before the first frame update
    void Awake()
    {
        if (uiRootDontDestroy != null)
        {
            Destroy(gameObject);
            return;
        }
        uiRootDontDestroy = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetFailedUI()
    {
        failed.SetActive(true);
        Invoke("SetEndWindow", 1f);

    }

    public void SetSuccessUI()
    {
        success.SetActive(true);
        Invoke("SetEndWindow", 1f);

    }


    public void SetEndWindow()
    {
        failed.SetActive(false);
        success.SetActive(false);
        EndWindow.SetActive(true);

    }

    public void SetSuccessUIFalse()
    {
        success.SetActive(false);
        EndWindow.SetActive(false);

    }

}
