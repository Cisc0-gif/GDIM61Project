using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Collider2D),typeof(Rigidbody2D))]
public class LaserCollider : MonoBehaviour
{
    public enum EnterReaction {Deflect,Destroy}
    //EnterReaction toggles wether or not the collider will deflect or just destroy the incoming laser.
    public EnterReaction enterReaction;
    public float DeflectionAngle = 45;
    public bool wall;
    public bool alwaysLit;

    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;

    private float hit;
    private Light2D lightSource;

    void Start()
    {
        if (GetComponent<Light2D>() != null) {
            lightSource = GetComponent<Light2D>();
            lightSource.enabled = false;
        }
	}

    public void Collide(Laser other)
    {
        if (!other.HasBeenSplit)
		{
            hit += 2.5f;
		} else
		{
            hit += 1;
		}
        //hit += 1; //add hit value to keep light enabled
        EnableLight();
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

    void Update()
    {
        if (!wall)
        {
            if (alwaysLit)
            {
                EnableLight();
            }
            else
            {
                if (hit < 0.5f) //value is added and reduced to stay above .5 when lit, else light turns off
                {
                    DisableLight();
                    ChangeSprite(0);
                    hit = 0;
                } else
				{
                    hit -= 0.5f;
				}
            }
        }

    }

	public void EnableLight()
    {
        if (!wall) {
            lightSource.enabled = true;
        }
	}

    public void DisableLight()
	{
        if (!wall)
        {
            lightSource.enabled = false;
        }
	}

    void ChangeSprite(int spriteIndex)
	{
        if (!wall)
        {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
        
	}

}
