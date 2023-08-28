using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BulletsSO", menuName = "ShootObject/SO/BulletsSO")]

public class BulletsSO : ScriptableObject
{
    public List<ShootObject> shootObjects;
}
