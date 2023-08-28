using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackBuffSpwan : MonoBehaviour
{
    float coolDown = 0;
    float coolDownMax;
    public GameObject attackVFX;
    public GameObject attack;
    int playerLayer;
    public Image coolDownSlider;
    bool isTaken;

    public GameObject[] bosses;

    // Start is called before the first frame update
    void Start()
    {
        coolDownMax = 12;
        playerLayer = LayerMask.NameToLayer("Player");
        attack = Instantiate(attackVFX, transform.position, transform.rotation);
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
            DamageUpPlayer(bosses,5);
            Destroy(attack);

            if (coolDown <= 0 && isTaken)
            {
                coolDown = coolDownMax;
                isTaken = false;
                StartCoroutine(BuffReSpwan(coolDownMax));
            }

        }
    }

    void DamageUpPlayer(GameObject[] other, int damage)
    {
        int i = Random.Range(0, other.Length);
        Debug.Log(i);
        if (other[i]!= null) 
        GameManager.Instance.EnemyTakeDamage(other[i], damage);

    }

    IEnumerator BuffReSpwan(float time)
    {
        yield return new WaitForSeconds(time);
        attack = Instantiate(attackVFX, transform.position, transform.rotation);
    }
}
