using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    public GameObject attack1VFX;
    public GameObject attack1VFXPos;
    
    public GameObject attack2VFX;
    public GameObject taughtingVFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAttack1VFX() 
    {
        Quaternion quaternion =  GetComponentInParent<Transform>().rotation;
        Instantiate<GameObject>(attack1VFX,attack1VFXPos.transform.position, quaternion);
    }

    public void PlayAttack2VFX()
    {
        Instantiate<GameObject>(attack2VFX);
    }
    public void PlayTaughtingVFX()
    {
        Instantiate<GameObject>(taughtingVFX);
    }


}
