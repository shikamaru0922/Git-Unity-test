using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplosionTrap : MonoBehaviour
{
    public GameObject explosionVFX;
    GameObject explosion;
    Renderer mat;
    int playerLayer;
    public Color color;
    public float colorValue = 0.25f;
    public float padColorChangeTime = 0.2f;
    void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        explosion = GetComponent<GameObject>();
        mat = gameObject.GetComponent<Renderer>();
    }

    private void Start()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;//没爆炸不启动collider
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void PlayExplosion()
    {
        StartCoroutine(TrapPadColorChange(colorValue));

    }



    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.layer == playerLayer)
        {
            player.GetComponent<CharactrStats>().characterData.currentHealth -= 10;
        }
        gameObject.GetComponent<BoxCollider>().enabled = false;//扣完血关闭collider
    }

    IEnumerator TrapPadColorChange(float target) 
    {
        float current = mat.material.color.a;
        float lerp = 0;
        while (lerp < 1) 
        {
            color.a = Mathf.Lerp(current, target, lerp);
            lerp += Time.deltaTime * padColorChangeTime;
            mat.material.SetColor("_BaseColor", color);
            yield return null;
        }
        explosion = Instantiate(explosionVFX, gameObject.transform.position, gameObject.transform.rotation);
        gameObject.GetComponent<BoxCollider>().enabled = true;//爆炸时启动collider
        Destroy(explosion, 3f);
        color.a = 0;
        mat.material.SetColor("_BaseColor", color);
    }
}
