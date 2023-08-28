using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Renderer _renderer;
    [SerializeField] AnimationCurve _DisplacementCurve;
    [SerializeField] float _DisplacementMagnitude;
    [SerializeField] float _LerpSpeed;
    [SerializeField] float _DisolveSpeed;
    bool _shieldOn;
    Coroutine _disolveCoroutine;
    public float shieldCapacity = 100;
    public float shieldDisableTime = 10;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.SetFloat("_Disolve", 1);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                HitShield(hit.point);
            }
        }

        //if (Input.GetKeyDown(KeyCode.Space)) 
        //{
        //    ShieldTakeDamage(shieldCapacity, 20);
        //}

    }

    public void HitShield(Vector3 hitPos)
    {
        _renderer.material.SetVector("_HitPos", hitPos);      
        StartCoroutine(Coroutine_HitDisplacement());
    }

    public void OpenCloseShield()
    {
        
        _disolveCoroutine = StartCoroutine(Coroutine_DisolveShield(0));
    }


    void ShieldTakeDamage(float capacity, float damage)
    {
        capacity -= damage;
        Debug.Log(capacity);
        if (capacity <= 0)
        {
            _disolveCoroutine = StartCoroutine(Coroutine_DisolveShield(1));
        }
    }

    private void OnEnable()
    {
        OpenCloseShield();
        StartCoroutine(ShieldDestroy(shieldDisableTime));
    }


    IEnumerator Coroutine_HitDisplacement()
    {
        float lerp = 0;
        while (lerp < 1)
        {
            _renderer.material.SetFloat("_DisplacementStrength", _DisplacementCurve.Evaluate(lerp) * _DisplacementMagnitude);
            lerp += Time.deltaTime*_LerpSpeed;
            yield return null;
            //Debug.Log("1");
        }
    }

    IEnumerator Coroutine_DisolveShield(float target)
    {
        float start = _renderer.material.GetFloat("_Disolve");
        float lerp = 0;
        while (lerp < 1)
        {
            _renderer.material.SetFloat("_Disolve", Mathf.Lerp(start,target,lerp));
            lerp += Time.deltaTime * _DisolveSpeed;
            yield return null;
            //Debug.Log("2");
        }
        if (target == 1) 
        {
            gameObject.SetActive(false);
        }

    }

    IEnumerator ShieldDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(Coroutine_DisolveShield(1));
    }
}
