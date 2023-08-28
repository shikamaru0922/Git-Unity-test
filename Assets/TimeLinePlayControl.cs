using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimeLinePlayControl : MonoBehaviour
{
    public static TimeLinePlayControl Instance;
    private PlayableDirector pd;
    public Action TimeLinePlayControlAction;

    private void Awake()
    {
        Instance = this;
        TimeLinePlayControlAction = new Action(() => { });
    }
    // Start is called before the first frame update
    void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.stopped += OnStopped;
        Debug.Log(TimeLinePlayControlAction.Method.Name);
    }

    private void OnStopped(PlayableDirector obj)
    {
        TimeLinePlayControlAction.Invoke();
        TimeLinePlayControlAction -= TimeLinePlayControlAction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
