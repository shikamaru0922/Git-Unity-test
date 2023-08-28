using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjFactory 
{
    public string path="Bullets/";
    public GameObject prefab;
    public abstract GameObject CreatObj(string name);
    
}
