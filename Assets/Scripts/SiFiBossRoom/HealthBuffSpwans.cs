using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBuffSpwans : MonoBehaviour
{
    float coolDown = 0;
    public float coolDownMax = 20;
    public GameObject healthVFX;
    public GameObject health;
    int playerLayer;
    public Image coolDownSlider;
    bool isTaken;
    public int addHealth = 20;
    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");

        health = Instantiate(healthVFX, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDown >= 0)
        {
            coolDown -= Time.deltaTime;
            //Debug.Log("1111");
                
        }
        coolDownSlider.fillAmount = coolDown / coolDownMax;

    }

    private void FixedUpdate()
    {
        
        
    }
   
    /// <summary>
    /// 玩家进入获取buff+销毁+生成
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            isTaken = true;
            AudioManagement.BuffSpwaningAudio();
            Destroy(health);
            
            if (coolDown <= 0 && isTaken) 
            {
                coolDown = coolDownMax;
                isTaken = false;
                StartCoroutine(BuffReSpwan(coolDownMax));
                other.gameObject.GetComponent<CharactrStats>()?.ApplyHealth(addHealth);
            }
        }
    }


    /// <summary>
    /// buff再生成
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator BuffReSpwan(float time) 
    {
        yield return new WaitForSeconds(time);
        health = Instantiate(healthVFX, transform.position, transform.rotation);
    }
}
