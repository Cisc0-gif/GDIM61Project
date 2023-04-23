using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D),typeof(Rigidbody2D))]
public class LaserCollider : MonoBehaviour
{
    public enum EnterReaction {Deflect,Destroy}
    //EnterReaction toggles wether or not the collider will deflect or just destroy the incoming laser.
    public EnterReaction enterReaction;
    public float DeflectionAngle = 45;
    public void Collide(Laser other)
    {
        if (enterReaction == EnterReaction.Deflect)
        {
            other.transform.eulerAngles += new Vector3(0, 0, DeflectionAngle);
            other.transform.position += other.transform.up;
        }
        else if (enterReaction == EnterReaction.Destroy)
        {
            other.DestroyMe();
        }
    }
}
