using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    //private RaycastHit2D hitInfo;
    private WallCheck check;
    [SerializeField]
    private LayerMask HeldObjLayer;
    private LayerMask HeldObjStored;
    public AudioSource placeObject;

    private void Start()
	{
        Physics2D.queriesStartInColliders = false; //start ray outside of BoxCollider
        check = GetComponentInChildren<WallCheck>();
	}

    void Update()
	{
        Vector3 dir = Vector3.zero;
        float xRD = 0f;
        float yRD = 0f;

        #region Animation Flips
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
        #endregion



        grabPoint.position = new Vector3(gameObject.transform.position.x + xRD, gameObject.transform.position.y + yRD, gameObject.transform.position.z); //position grabPoint around player
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, dir, rayDistance, LayerMask.GetMask("Objects") + LayerMask.GetMask("Crates")); //send raycast out to find objects to pick up
        
        if (hitInfo.collider != null||grabbedObject!=null)
		{
            //Debug.Log(hitInfo.collider);
            if (Keyboard.current.spaceKey.wasPressedThisFrame && grabbedObject == null) //if nothing grabbed
            {
                if (hitInfo.collider.GetComponent<PrismStorage>() != null)
                {
                    grabbedObject = hitInfo.collider.GetComponent<PrismStorage>().SpawnPrism();
                    if (grabbedObject != null)
                    {

                        grabbedObject.transform.position = grabPoint.position;
                        grabbedObject.transform.SetParent(grabPoint);
                        if (grabbedObject.GetComponent<Rigidbody2D>() != null)
                        {
                            HeldObjStored = grabbedObject.layer;
                            grabbedObject.layer = HeldObjLayer;
                            grabbedObject.GetComponent<Rigidbody2D>().simulated = false;
                        }
                    }
                }
                else if (hitInfo.collider.gameObject.GetComponent<ObjectBolter>() == null)
                {
                    grabbedObject = hitInfo.collider.gameObject; //set grabbed to whatever Ray Cast hit
                    grabbedObject.transform.position = grabPoint.position;
                    grabbedObject.transform.SetParent(grabPoint);
                    if (grabbedObject.GetComponent<Rigidbody2D>() != null)
                    {
                        HeldObjStored = grabbedObject.layer;
                        grabbedObject.layer = HeldObjLayer;
                        grabbedObject.GetComponent<Rigidbody2D>().simulated = false;
                    }
                }
                 
            }
            else if (Keyboard.current.spaceKey.wasPressedThisFrame && grabbedObject != null) //else if something grabbed
            {
                placeObject.Play();
                CratePutBack(hitInfo);
            }
		}
        check.HeldObject = grabbedObject;
        Debug.DrawRay(rayPoint.position, dir * rayDistance);

    }

    private void CratePutBack(RaycastHit2D hit)
    {

        if (hit.collider == null)
        {
            //Failsafe cancel to eliminate any overlap issues
            foreach (PrismType prism in GameObject.FindObjectsOfType<PrismType>())
            {
                if (prism.gameObject != grabbedObject.gameObject&&Vector3.Distance(prism.transform.position, (Vector3Int.RoundToInt(grabbedObject.transform.position)))<0.25f)
                    return;
            }
            grabbedObject.transform.position = Vector3Int.RoundToInt(grabbedObject.transform.position);
            grabbedObject.transform.SetParent(null); //release object

            if (grabbedObject.GetComponent<Rigidbody2D>() != null)
            {
                grabbedObject.GetComponent<Rigidbody2D>().simulated = true;
                grabbedObject.layer = HeldObjStored;
            }
            grabbedObject = null;

            return;

        }

        PrismStorage storage = hit.collider.GetComponent<PrismStorage>();
        Prism objType = grabbedObject.GetComponent<PrismType>().GetPrism();

        if(storage != null && storage.CheckType(objType))
        {
            storage.PlaceObject(grabbedObject);
            Destroy(grabbedObject);
            grabbedObject = null;
        }
    }
    
    private void CheckForWall()
    {
        
    }

}
