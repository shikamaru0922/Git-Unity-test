using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceLevelLoader : MonoBehaviour
{
    public Animator transition;
    public float waitingTime = 1;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) 
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        //get current scene's index + 1 for next scene level's index
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex) 
    {
        //play loading animation
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(waitingTime);
        //load scene
        SceneManager.LoadScene(levelIndex);
    }
}
