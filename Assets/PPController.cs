using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PPController : MonoBehaviour
{
    private Volume volume;
    private Vignette vignette;

    

    [SerializeField]
    private Color hurtColor;
    [SerializeField]
    private float hurtSpeed;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("in PP ");
        //Debug.Log(GameManager.Instance.isBack == false);
        if (GameManager.Instance.isDamaging  && GameManager.Instance.isBack == false)
        {
            
            vignette.intensity.value += hurtSpeed * Time.deltaTime;
           
            vignette.color.value = hurtColor;

            if (vignette.intensity.value >= 0.85f)
            {
                //Debug.Log(vignette.intensity.value);
                GameManager.Instance.isDamaging = false;
                GameManager.Instance.isBack = true;
            }


            if (GameManager.Instance.isBack) 
            {
                //Debug.Log("im back");
                vignette.intensity.value -= 3.5f*hurtSpeed * Time.deltaTime;
                if (vignette.intensity.value <= 0.75f) 
                {
                    vignette.intensity.value = 0.0f;
                    //Debug.Log("back finish");
                    GameManager.Instance.isBack = false;
                }
            }


        }
    }
}
