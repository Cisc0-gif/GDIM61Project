using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [SerializeField]
    public GameObject HeldObject;
    [SerializeField]
    private BoxCollider2D col;


  

    private PlayerMovement movement;
   

    private void LateUpdate()
    {
        col.enabled = HeldObject != null;
        if(HeldObject != null)
        {
            col.size = HeldObject.GetComponent<Collider2D>().bounds.size;
        }
        

    }
  
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log($"X: {transform.localPosition.x}, Y: {transform.localPosition.y}");
        Vector2 absPos = new Vector2(Mathf.Abs(transform.localPosition.x), Mathf.Abs(transform.localPosition.y));
        if(absPos.x == absPos.y)
        {
            Debug.Log("Horizontal Collision");
        }
        else if (transform.localPosition.y == 0)
        {
            Debug.Log("X only Collsion");
        }
        else if (transform.localPosition.x == 0)
        {
            Debug.Log("Y only Collsiion");
        }
    }*/
}
