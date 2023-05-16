using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public enum TargetType {Only_White,Only_RGB,Only_R,Only_G,Only_B};
    //TargetType controls what colors will toggle the target.
    [HideInInspector]
    public bool Activated;
    private float TimeTillDeactivate;
    public Sprite Pressed;
    private Sprite Unpressed;
    public TargetType targetType;

    //After TimeOutDelay seconds of no input, the target will toggle off.
    float TimeOutDelay=2;
    private void Start()
    {
        Unpressed = GetComponent<SpriteRenderer>().sprite;
    }
    public void Activate()
    {
        Activated = true;
        TimeTillDeactivate = Time.timeSinceLevelLoad + TimeOutDelay;
    }
    public void DeActivate()
    {
        Activated = false;
    }
    public void Collide(Laser other)
    {
        if (targetType==TargetType.Only_White&&!other.HasBeenSplit)
        {
            Activate();
        }
        else if (targetType == TargetType.Only_RGB && other.HasBeenSplit)
        {
            Activate();
        }
        else if (targetType == TargetType.Only_R && other.MyColor=="Red")
        {
            Activate();
        }
        else if (targetType == TargetType.Only_G && other.MyColor == "Green")
        {
            Activate();
        }
        else if (targetType == TargetType.Only_B && other.MyColor == "Blue")
        {
            Activate();
        }
        other.DestroyMe();
    }
    private void FixedUpdate()
    {
        //Target Timeout
        if(Activated&&Time.timeSinceLevelLoad>TimeTillDeactivate)
        {
            DeActivate();
        }

        //On/Off visibility controlled here
        if (Activated)
            this.GetComponent<SpriteRenderer>().sprite = Pressed;
        else
            this.GetComponent<SpriteRenderer>().sprite = Unpressed;
    }
}
