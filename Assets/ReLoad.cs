using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReLoad : MonoBehaviour
{
    public GameObject EndWidowUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevelScene()
    {
        //UIManager.Instance.CloseView(UIType.MainView);
        //UIManager.Instance.OpenView(UIType.SettingView);
        //gameObject.SetActive(false);
        //SceneManager.LoadScene(1);
        EndWidowUI.SetActive(false);
        GameManager.Instance.nextSceneName = "level1_SiFi";
        SceneManager.LoadScene("Loading");
        GameManager.Instance.EnemyCount = 0;
        GameManager.Instance.EnemyRound = 0;
        UIRootDontDestroy.uiRootDontDestroy.SetSuccessUIFalse();
        Invoke("slideractive", 0.5f);//ÇÐ»»³¡¾°ºó¼¤»îslider.
        
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
