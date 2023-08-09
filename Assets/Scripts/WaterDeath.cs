using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDeath : MonoBehaviour
{
    public List<GameObject> cubeList = new List<GameObject>();

    [HideInInspector]
    public BoxCollider coll = null;

    public void OnAction()
    {
        if (!coll)
            return;

        coll.enabled = false;

        foreach(var cube in cubeList) 
        { 
            Rigidbody rg = cube.GetComponent<Rigidbody>();
            rg.AddForce(Vector3.up * 200f, ForceMode.Force);
        }
    }
}
