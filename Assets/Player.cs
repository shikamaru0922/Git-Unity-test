using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 movement;
    private Rigidbody playerRigbody;
    RaycastHit hit;
    public LayerMask layerMask;
    public Transform floor;
    private Plane plane;
    private float distance;
    private Animator animator;
    private int isWalkingId;
    // Start is called before the first frame update
    void Start()
    {
        playerRigbody = GetComponent<Rigidbody>();
        plane = new Plane(floor.up, floor.position);
        //animator = GetComponent<Animator>();
        //isWalkingId = Animator.StringToHash
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h, v);
        Rotate();
        //IsWalking(h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * moveSpeed * Time.fixedDeltaTime;
        playerRigbody.MovePosition(playerRigbody.position + movement);
    }

    void Rotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            Vector3 hitpoint = ray.GetPoint(distance);
            Vector3 dir = hitpoint - transform.position;
            dir.y = 0;
            Quaternion targetDir = Quaternion.LookRotation(dir);
            playerRigbody.MoveRotation(targetDir);
        }
    }
    //void IsWalking(float h, float v)
    //{
    //    bool walking = (h != 0 || v != 0);
    //    animator.SetBool("IsWalking", walking);
    //}
}
