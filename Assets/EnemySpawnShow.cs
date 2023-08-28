using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnShow : MonoBehaviour
{
    public GameObject[] objs;

    public void ShowEnemySpawns()
    {
        foreach (var obj in objs)
        {
            obj.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ZhangWei.Tags.Player))
        {
            //Debug.Log("Player In");
            ShowEnemySpawns();
            this.transform.parent.GetComponent<Room>().DoorController(1);
            GameManager.Instance.Nowroom = this.transform.parent.GetComponent<Room>();
            this.gameObject.SetActive(false);
        }
    }
}
