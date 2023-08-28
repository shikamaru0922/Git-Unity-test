using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NaviBossTest : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent meshAgent;
    public GameObject player;
    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        meshAgent.SetDestination(player.transform.position);
    }
}
