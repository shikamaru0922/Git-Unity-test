using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Follow", menuName = "Command/FixUpdateCommand/Follow")]
public class Follow : CommandObj
{
    Vector3 finalForward;
    public Transform target;
    float rotateSpeed = 300f;
    public LayerMask layerMask;
    Collider[] collider1;
    public override void Execute(BulletEventController eventController, BulletData bulletData, Collision collision = null, Collider collider = null)
    {
        
        collider1= Physics.OverlapSphere(eventController.transform.position, 5, layerMask);
       
        if (collider1.Length<=0)
        {
            
            bulletData.rb.velocity = eventController.transform.forward * Mathf.Max(bulletData.v_speed, 3f);
            return;
        }
        target = collider1[0].transform;
        bulletData.rb.velocity = eventController.transform.forward * Mathf.Max(bulletData.v_speed, 3f);

        finalForward = (target.position - eventController. transform.position);
        finalForward.y = 0;
        if (finalForward.sqrMagnitude > 0.1f)
        {
            finalForward = finalForward.normalized;

            float speed = rotateSpeed * Time.fixedDeltaTime;
            if (finalForward != eventController.transform.forward)
            {
                float angleOffset = Vector3.Angle(eventController.transform.forward, finalForward);
                //Debug.Log(angleOffset);
                if (angleOffset > rotateSpeed)
                {
                    angleOffset = rotateSpeed;
                }
                //将自身forward朝向慢慢转向最终朝向
                eventController.transform.forward = Vector3.Slerp(eventController.transform.forward, finalForward, speed / angleOffset);

            }

            //rb.velocity = transform.forward * Mathf.Lerp(5,15, Mathf.Abs(180 - Vector3.Angle(transform.forward, finalForward)) /180f);
            //rb.velocity = transform.forward * v_speed;
            //Debug.Log(rb.velocity.magnitude);
            //Debug.Log(Mathf.Abs(180 - Vector3.Angle(transform.forward, finalForward))/180f);
        }
        //transform.LookAt(target, transform.forward);
        // rb.AddRelativeForce (Vector3.forward*2);
    }
}
