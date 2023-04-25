using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEnterance : MonoBehaviour
{
    [SerializeField]
    private PortalExit Exit;

    private PortalObject PlacedObject;

    [SerializeField]
    private SpriteRenderer Renderer;


    //public Action<Vector3> RotateObject;
    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetExit(PortalExit Exit)
    {
        this.Exit = Exit;
    }

    public void PlaceObject(PortalObject obj)
    {
        PlacedObject = obj;
        PlacedObject.SetExit(Exit);
        Exit.ParodyObject(PlacedObject.GetWorldObject());
    }

    public void RemoveObject()
    {
        PlacedObject.SetExit(null);
        PlacedObject = null;
        Exit.RemoveObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered");
        if (collision.GetComponent<PortalObject>() != null)
        {
            Debug.Log("Portal Obj Entered");
            collision.transform.position -= new Vector3(0f, 0f, transform.position.z + 1);
            PlaceObject(collision.GetComponent<PortalObject>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.GetComponent<PortalObject>() != null)
        {
            RemoveObject();
        }
    }

    public void SwitchMaterial(Color color)
    {
        Renderer.material.SetColor("_BGColor", color);
        Exit.UpdateTexture(Renderer.material);
    }
}
