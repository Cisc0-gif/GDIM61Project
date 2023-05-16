using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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

    private Light2D lightSource;
    //After TimeOutDelay seconds of no input, the target will toggle off.
    float TimeOutDelay=2;
    private void Start()
    {
        lightSource= GetComponent<Light2D>();
        Unpressed = GetComponent<SpriteRenderer>().sprite;
    }
    public void Activate()
    {
        if(!Activated)
        {
            foreach(LevelDoor door in GameObject.FindObjectsOfType<LevelDoor>())
            {
                door.ToggleDoor();
            }
        }
        Activated = true;
        TimeTillDeactivate = Time.timeSinceLevelLoad + TimeOutDelay;
    }
    public void DeActivate()
    {
        if (Activated)
        {
            foreach (LevelDoor door in GameObject.FindObjectsOfType<LevelDoor>())
            {
                door.ToggleDoor();
            }
        }
      
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
        if(Activated)
        {
            if(Time.timeSinceLevelLoad > TimeTillDeactivate)
                DeActivate();

            lightSource.intensity = Mathf.Min(1, lightSource.intensity+0.25f);
        }
        else
            lightSource.intensity=Mathf.Max(0,lightSource.intensity/2f);


        //On/Off visibility controlled here
        if (Activated)
            this.GetComponent<SpriteRenderer>().sprite = Pressed;
        else
            this.GetComponent<SpriteRenderer>().sprite = Unpressed;
    }
}
