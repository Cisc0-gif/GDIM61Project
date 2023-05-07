using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LaserMerger : MonoBehaviour
{
    public GameObject LaserPrefab;
    //The output lazer emerges at an average rotation for its inputs, to that end, three seperate input rotations are stored
    //In addition the ttd_color variables are seperate timeouts to seperately reset the ColorHits variables when one color stops getting powered after a long time of being powered.


    public bool RedHits;
    public bool GreenHits;
    public bool BlueHits;
    private Quaternion redrot;
    private Quaternion bluerot;
    private Quaternion greenrot;
    private float ttd_red;
    private float ttd_blue;
    private float ttd_green;

    private Light2D lightSource;
    private float hit;

    //After TimeOutDelay seconds of no input, the given color will toggle off.
    float TimeOutDelay = 2;

    void Start()
	{
        if (GetComponent<Light2D>() != null)
		{
            lightSource = GetComponent<Light2D>();
		}
        lightSource.enabled = false;
	}

    public void Collide(Laser other)
    {
        //Marks which colors have hit the merger, destroys them afterwards.
        if (other.HasBeenSplit)
        {
            if (other.MyColor == "Red")
            {
                RedHits=true;
                ttd_red = Time.timeSinceLevelLoad + TimeOutDelay;
                redrot = other.transform.rotation;
            }
            else if (other.MyColor == "Blue")
            {
                BlueHits=true;
                ttd_blue = Time.timeSinceLevelLoad + TimeOutDelay;
                bluerot = other.transform.rotation;
            }
            else if (other.MyColor == "Green")
            {
                GreenHits=true;
                ttd_green = Time.timeSinceLevelLoad + TimeOutDelay;
                greenrot = other.transform.rotation;
            }
            other.MyColor = "";

            

        }
        other.DestroyMe();
    }
    public float dist;
    
    private void Update()
    {
        //After the timeout, reset the toggle for any given color.
        if (Time.timeSinceLevelLoad > ttd_red)
            RedHits = false;
        if (Time.timeSinceLevelLoad > ttd_green)
            GreenHits = false;
        if (Time.timeSinceLevelLoad > ttd_blue)
            BlueHits = false;
        //Takes a confirmed Red, Green and Blue hit within Timeout Seconds to create a new White Laser output.
        if (RedHits&&GreenHits&&BlueHits)
        {
            hit += 1.05f;
            RedHits=false;
            GreenHits=false;
            BlueHits=false;

            GameObject output = Instantiate(LaserPrefab, this.transform.position, Quaternion.Euler((redrot.eulerAngles+bluerot.eulerAngles+greenrot.eulerAngles)/3), this.transform);
            output.GetComponent<TrailRenderer>().AddPosition(this.transform.position);
            output.transform.position += (output.transform.up *dist);
            output.GetComponent<Laser>().HasBeenMerged = true;
        }

        if (hit > 0.5f)
        {
            lightSource.enabled = true;
        } else
		{
            lightSource.enabled = false;
		}

        if (hit < 0)
		{
            hit = 0;
		} else
		{
            hit -= 0.5f;
		}

    }
}
