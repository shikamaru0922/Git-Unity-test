using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapControl : MonoBehaviour
{
    public Transform[] rows;
    public Transform[][] trap;
    public float traptime = 1;

    private void Awake()
    {
        TimeLinePlayControl.Instance.TimeLinePlayControlAction += Open;
    }

    void Start()
    {
        
        trap = new Transform[13][];
        for (int i = 0; i < 13; i++) 
        {
            trap[i] = new Transform[rows[i].childCount];
            for (int j = 0; j < trap[i].Length; j++) 
            {
                trap[i][j] = rows[i].GetChild(j);
            }
        }

        StartCoroutine(TrapControlExplosion(traptime));
    }



    IEnumerator TrapControlExplosion(float time) 
    {
        for (int i = 0; i < 13; i++) 
        {
            for (int j = 0; j < trap[i].Length; j++) 
            {
                trap[i][j].GetComponent<ExplosionTrap>().PlayExplosion();
            }
            yield return new WaitForSeconds(time);
            
        }       
    }

    public void Open()
    {
        this.enabled = true;
    }
}
