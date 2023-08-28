using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainController : MonoBehaviour
{
    MainView mainView;
   public GameObject slider;
    public GameObject slider_mp;
    // Start is called before the first frame update
    void Awake()
    {
        mainView = GetComponent<MainView>();
    }

    // Update is called once per frame
    void OnEnable()
    {
        mainView.btn.onClick.AddListener(LoadLevelScene);
    }

    private void OnDisable()
    {
        mainView.btn.onClick.RemoveListener(LoadLevelScene);
    }

    void LoadLevelScene()
    {
        //UIManager.Instance.CloseView(UIType.MainView);
        //UIManager.Instance.OpenView(UIType.SettingView);
        gameObject.SetActive(false);
        //SceneManager.LoadScene(1);
        GameManager.Instance.nextSceneName = "level1_SiFi";
        SceneManager.LoadScene("Loading");
        Invoke("slideractive", 0.5f);//ÇÐ»»³¡¾°ºó¼¤»îslider.
    }
    void slideractive()
    {
        slider.SetActive(true);
        slider_mp.gameObject.SetActive(true);
    }
}
