using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PortalObject : MonoBehaviour, ClickableObject
{
    [SerializeField]
    private GameObject WorldObject;

    private bool IsClicked = false;


    private Vector3 mousePos = Vector3.zero;

    [SerializeField]
    private Vector3 RotationPerClick = new Vector3(45,0,0);
    private PortalExit Exit = null;

    
    public GameObject GetWorldObject()
    {
        return WorldObject;
    }

   

    private void Update()
    {
        if (IsClicked)
        {
            transform.position = MouseManager.MousePos2D;

            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.eulerAngles += (-1 * RotationPerClick);
                
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                transform.eulerAngles += (RotationPerClick);
            }
            if (Exit != null)
                Exit.UpdateParody(transform.eulerAngles);

        }

    }

    

    public void Click()
    {
        IsClicked = !IsClicked;
    }

    public void SetExit(PortalExit Exit)
    {
        this.Exit = Exit;
    }
   
}
