using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestChangeScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            UIManager.Instance.CloseAllViews();
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            UIManager.Instance.CloseAllViews();
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            UIManager.Instance.CloseAllViews();
            SceneManager.LoadScene(2);
        }
    }
}
