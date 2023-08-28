using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethod 
{
    private const float dotThreshold = 0.5f;
    public static bool IsFacingTarget(this Transform transfrom,Transform target)
    {
        var vectorToTarget = target.position - transfrom.position;
        vectorToTarget.Normalize();
        float dot = Vector3.Dot(transfrom.forward, vectorToTarget);

        return dot >= dotThreshold;
    }
}
