using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabObjects : MonoBehaviour
{
    [SerializeField]
    private Transform grabPoint;

    [SerializeField]
    private Transform rayPoint;

    [SerializeField]
    private float rayDistance;

    private GameObject grabbedObject;
    private int layerIndex;
    private Vector3 dir;
    //private RaycastHit2D hitInfo;

    private void Start()
	{
        layerIndex = LayerMask.NameToLayer("Objects");
        Physics2D.queriesStartInColliders = false; //start ray outside of BoxCollider
	}

    void Update()
	{
        Vector3 dir = Vector3.zero;
        float xRD = 0f;
        float yRD = 0f;

        //Horizontal
        if (gameObject.GetComponent<Animator>().GetFloat("Horizontal") == 1) //if gameObject not flipped (moving Left!)
		{
            dir = transform.right;
            xRD = rayDistance;
        } 
        else if (gameObject.GetComponent<Animator>().GetFloat("Horizontal") == -1)
        {
            dir = -transform.right;
            xRD = -rayDistance;
        }

        //Vertical
        if (gameObject.GetComponent<Animator>().GetFloat("Vertical") == 1) //if gameObject not flipped (moving Left!)
        {
            dir = Vector3.up;
            yRD = rayDistance;
        }
        else if (gameObject.GetComponent<Animator>().GetFloat("Vertical") == -1)
        {
            dir = -Vector3.up;
            yRD = -rayDistance - 0.25f;
        }

        grabPoint.position = new Vector3(gameObject.transform.position.x + xRD, gameObject.transform.position.y + yRD, gameObject.transform.position.z); //position grabPoint around player
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, dir, rayDistance); //send raycast out
        
        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
		{
            //Debug.Log(hitInfo.collider);
            if (Keyboard.current.spaceKey.wasPressedThisFrame && grabbedObject == null) //if nothing grabbed
			{
                grabbedObject = hitInfo.collider.gameObject; //set grabbed to whatever Ray Cast hit
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(grabPoint);
			} 
            else if (Keyboard.current.spaceKey.wasPressedThisFrame && grabbedObject != null) //else if something grabbed
			{
                grabbedObject.transform.SetParent(null); //release object
                grabbedObject = null;
			}
		}

        Debug.DrawRay(rayPoint.position, dir * rayDistance);
	}

}
