using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTrans : MonoBehaviour
{
    //公开一个滑动条
    public Slider LoadingSlider;

    //公开载入文本（代码放置载入文本身上）
    public Text LoadText;

    //公开字符串用于填写第三个场景的名称
    private string SceneName;
    private float TargetVaule;
    private AsyncOperation async = null;

    void Start()
    {
        LoadText = GetComponent<Text>();
        LoadingSlider = FindObjectOfType<Slider>();
        Invoke("StartLoad", 0.1f);
        SceneName = GameManager.Instance.nextSceneName;
        PoolManager.Instance.ClearPoolItemForLoadingScene();
    }

    private void StartLoad()
    {
        StartCoroutine(AsyncLoading());
    }

    IEnumerator AsyncLoading()
    {
        //异步加载场景
        async = SceneManager.LoadSceneAsync(SceneName);
        //阻止当加载完成自动切换
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress < 0.9f)
            {
                TargetVaule = async.progress;
            }
            else
            {
                TargetVaule = 1.0f;
            }

            LoadingSlider.value = TargetVaule;

            LoadText.text = (int)(LoadingSlider.value * 100) + "%";

            if (TargetVaule >= 0.9)
            {
                async.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}