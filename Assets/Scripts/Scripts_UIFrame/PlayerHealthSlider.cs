using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSlider : MonoBehaviour
{
    Slider playerHealthSlider;
    GameObject player;
    CharactrStats charactrStats;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerHealthSlider = GetComponent<Slider>();
        charactrStats = player.GetComponent<CharactrStats>();
        playerHealthSlider.value =  (float)charactrStats.CurrentHealth / charactrStats.MaxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        playerHealthSlider.value = (float)charactrStats.CurrentHealth / charactrStats.MaxHealth;
        if (player == null) 
        {
            player = GameObject.FindWithTag("Player");
            charactrStats = player.GetComponent<CharactrStats>();
            playerHealthSlider.value = (float)charactrStats.CurrentHealth / charactrStats.MaxHealth;
        }
    }
}
