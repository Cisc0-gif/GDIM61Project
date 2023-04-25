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

    [SerializeField]
    private SpriteRenderer Renderer;
    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        
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
        ParodiedObject = Instantiate(Parody, transform.position - new Vector3(0,0,1), Parody.transform.rotation, transform);
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

    public void UpdateTexture(Material mat)
    {
        Renderer.material = mat;
    }
}
