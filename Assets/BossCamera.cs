using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossCamera : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    public Transform camera;
    void Awake()
    {
        camera = GetComponent<Transform>();
        offset = camera.position - player.position;
        if (SceneManager.GetActiveScene().name == "SIFI_Boss_NewUpdate")
            TimeLinePlayControl.Instance.TimeLinePlayControlAction += Open;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        camera.position = player.position + offset;
        // Debug.Log(player.position);

    }
    public void Open()
    {
        this.enabled = true;
    }
}
