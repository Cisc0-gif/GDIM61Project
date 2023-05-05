using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float DeSpawnDistance = 100;
    bool FinishedLaser;
    Vector2 lastHit=Vector2.positiveInfinity;
    TrailRenderer rend;
    [HideInInspector]
    public bool HasBeenSplit;
    [HideInInspector]
    public string MyColor="White";

    private enum deflectColor { Clear, White, Red, Green, Blue };
    private int currentColor;

    private void Start()
    {
        rend= GetComponent<TrailRenderer>();
    }

    public void SetColor(Gradient g, string colorName)
    {
        MyColor = colorName;
        GetComponent<TrailRenderer>().colorGradient = g;
        switch (MyColor)
        {
            case "Red":
                currentColor = 2;
                break;

            case "Green":
                currentColor = 3;
                break;

            case "Blue":
                currentColor = 4;
                break;
        }
    }

    public void DestroyMe()
    {
        if (!FinishedLaser)
        {
            FinishedLaser = true;
            if (GetComponent<Rigidbody2D>() != null)
                GetComponent<Rigidbody2D>().simulated = false;
            gameObject.name = "Destroying: " + gameObject.name;
            transform.parent = Camera.main != null ? Camera.main.transform : null;
            //When the parent is changed to a temporary location or null, the emitter will allow new lasers to spawn while this one does its destroy animation.
            StartCoroutine(DelayedDestroy());
        }
    }
    public IEnumerator DelayedDestroy()
    {
        while (rend.widthMultiplier >= 0)
        {
           rend.widthMultiplier -= 0.01f;
           yield return new WaitForFixedUpdate();
        }
        Destroy(this.gameObject);
    }

    void Update()
    {
        
        if (!FinishedLaser)
        {
            if (!HasBeenSplit)
            {
                currentColor = 1;
            }
            float max_laser_distance = 5;
            //Will move max_laser_distance units unless it encounters an Object layer collider between that distance.           

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up,max_laser_distance, (LayerMask.GetMask("Objects")));
            RaycastHit2D wallhit = Physics2D.Raycast(transform.position, transform.up, max_laser_distance, (LayerMask.GetMask("Walls"))); //Ferenc
            
            //Check collision with object first
            if (hit.collider != null)
            {

                float distance = Vector2.Distance(hit.point, transform.position);
                if (Vector2.Distance(hit.collider.transform.position, lastHit) > 0.05f)
                {

                    rend.AddPosition(transform.position);
                    rend.AddPosition(hit.point);
                    hit.collider.SendMessage("Collide", this, SendMessageOptions.DontRequireReceiver);
                    hit.collider.SendMessage("ChangeSprite", currentColor, SendMessageOptions.DontRequireReceiver);
                    //The Collide(laser) functon is Duck Typed to all Laser interacting scripts, and will only call here.
                    lastHit = hit.collider.transform.position;
                    transform.position = hit.collider.transform.position;
                }
                else
                {
                    transform.position += transform.up * 0.5f;
                }
            }
            else if (wallhit.collider != null) //if no object collided w/ check collision with wall instead (Ferenc)
            {
                rend.AddPosition(transform.position);
                rend.AddPosition(wallhit.point);
                wallhit.collider.SendMessage("Collide", this, SendMessageOptions.DontRequireReceiver);
                wallhit.collider.SendMessage("ChangeSprite", currentColor, SendMessageOptions.DontRequireReceiver);
                //The Collide(laser) functon is Duck Typed to all Laser interacting scripts, and will only call here.
                lastHit = wallhit.collider.transform.position;
                transform.position = wallhit.collider.transform.position;
            }
            else
            {
                transform.position += transform.up * max_laser_distance;
            }

            if (Vector2.Distance(Camera.main.transform.position, transform.position) > DeSpawnDistance)
            {
                DestroyMe();
            }
        }
    }
}
