using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBuffSpwan : MonoBehaviour
{
    float coolDown = 0;
    public float coolDownMax = 20;
    public GameObject shieldVFX;
    public GameObject shieldBuff;
    public Transform player;
    public GameObject shield;
    public GameObject shieldSpwan;
    int playerLayer;
    public Image coolDownSlider;
    bool isTaken;

    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        player = GetComponent<Transform>();
        //shield = GetComponent<GameObject>();
        
        shieldBuff = Instantiate(shieldVFX, transform.position, transform.rotation);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            isTaken = true;
            Destroy(shieldBuff);

            if (coolDown <= 0 && isTaken)
            {
                ShieldAdd(other);
                coolDown = coolDownMax;
                isTaken = false;
                StartCoroutine(BuffReSpwan(coolDownMax));
                //StartCoroutine(ShieldDestroy(12));
            }

        }
    }

    void ShieldAdd(Collider player)
    {if (shieldSpwan != null) 
        {
            shieldSpwan.SetActive(true);
            return;
        }
        Vector3 pos = player.transform.position;
        pos.y += 1f;
        shieldSpwan = Instantiate(shield, pos, Quaternion.identity, player.transform);
        //player.GetComponent<Collider>().enabled = false;
        player.enabled = false;
        StartCoroutine(PlayerColliderActive(player, 10));
        AudioManagement.BuffGainingAudio();
    }


    //IEnumerator ShieldDestroy(float time) 
    //{
    //    yield return new WaitForSeconds(time);
    //    shieldSpwan.SetActive(false);
    //}

    IEnumerator BuffReSpwan(float time)
    {
        yield return new WaitForSeconds(time);
        shieldBuff = Instantiate(shieldVFX, transform.position, transform.rotation);
        AudioManagement.BuffSpwaningAudio();
    }

    IEnumerator PlayerColliderActive(Collider obj,float time) 
    {
        yield return new WaitForSeconds(time);
        obj.enabled = true;
    }


}
