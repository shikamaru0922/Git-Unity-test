using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelerateBuffSpwan : MonoBehaviour
{
    float coolDown = 0;
    public float coolDownMax = 15;
    public GameObject speedVFX;
    public GameObject speed;
    int playerLayer;
    public Image coolDownSlider;
    bool isTaken;
    public float speedBoost = 3;

    public float speedUpTime = 3;

    CharacterMove characterMove;
    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");

        speed = Instantiate(speedVFX, transform.position, transform.rotation);
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
            AudioManagement.BuffSpwaningAudio();
            Destroy(speed);

            if (coolDown <= 0 && isTaken)
            {
                coolDown = coolDownMax;
                isTaken = false;
                StartCoroutine(SpeedUp(other.gameObject));
                StartCoroutine(BuffReSpwan(coolDownMax));
                characterMove = other.gameObject.GetComponent<CharacterMove>();
                //characterMove.moveSpeed += speedBoost;

            }

        }
    }

    void SpeedUpPlayer(float speed)
    {

    }

    IEnumerator SpeedUp(GameObject obj) 
    {
        obj.GetComponent<CharacterMove>().moveSpeed += speedBoost;
        yield return new WaitForSeconds(speedUpTime);
        obj.GetComponent<CharacterMove>().moveSpeed -= speedBoost;
    }


    IEnumerator BuffReSpwan(float time)
    {
        yield return new WaitForSeconds(time);
        speed = Instantiate(speedVFX, transform.position, transform.rotation);
    }
}
