using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public GameObject Laser;

    private void Update()
    {
        //Will only emit a laser after the last one has either been modified (and stopped being a child) or is destroyed.
        if (transform.childCount == 0)
        {
            Instantiate(Laser,transform.position+(transform.up/1.7f),transform.rotation,transform);
        }
    }
}
