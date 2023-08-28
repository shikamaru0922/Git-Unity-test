using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMove : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 movement;

    private Plane plane;
    public Transform floor;
    public float distance;

    private Animator anim;

    private Rigidbody playerRigidbody;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        floor = GameObject.Find("BasePlacePoint").transform;
        plane = new Plane(floor.up, floor.position);
        if (SceneManager.GetActiveScene().name == "SIFI_Boss_NewUpdate")
            TimeLinePlayControl.Instance.TimeLinePlayControlAction += Open;
    }
    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Mathf.Abs(transform.rotation.eulerAngles.y - 180f) > 90)
        {
            anim.SetFloat("Horizontal", h);
            anim.SetFloat("Vertical", v);
        }
        else
        {
            anim.SetFloat("Horizontal", -h);
            anim.SetFloat("Vertical", -v);
        }
        
        
        Move(h, v);
        Rotate();
    }

    private void Update()
    {
      
    }

    private void Move(float h, float v)
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * moveSpeed * Time.fixedDeltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + movement);
    }


    public void Rotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            Vector3 dir = hitPoint - transform.position;
            dir.y = 0;
            Quaternion targetDir = Quaternion.LookRotation(dir);
            playerRigidbody.MoveRotation(targetDir);
        }

    }

    public void Open() 
    {
        this.enabled = true;
    }
}
