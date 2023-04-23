using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalExit : MonoBehaviour
{
    [SerializeField]
    private PortalEnterance Enterance;

    [SerializeField]
    private GameObject ParodiedObject;

   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnterance (PortalEnterance Enterance)
    {
        this.Enterance = Enterance;
    }

    public void ParodyObject(GameObject Parody)
    {
        ParodiedObject = Instantiate(Parody, transform.position, Parody.transform.rotation, transform);
    }

    public void RemoveObject()
    {
        Destroy(ParodiedObject);
        ParodiedObject = null;
    }

    public void UpdateParody (Vector3 Rotation)
    {
        ParodiedObject.transform.eulerAngles = Rotation;
    }
}
