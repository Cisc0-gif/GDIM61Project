using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private Light2D lightSource;
    private SpriteRenderer spriteRenderer;
    private float hit;
    public Sprite[] sprites;

    private bool IsPickedUp = false;

    void Start()
    {
        if (GetComponent<Light2D>() != null) {
            lightSource = GetComponent<Light2D>();
            lightSource.enabled = false;
        }

        if (GetComponent<SpriteRenderer>() != null)
		{
            spriteRenderer = GetComponent<SpriteRenderer>();
		}
	}

    public void Collide(Laser other)
    {
        hit += 10f;
        /*
        if (!other.HasBeenSplit && !other.HasBeenMerged)
		{
            hit += 2.5f;
		} else
		{
            hit += 1;
		}
        */
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
                } else if (hit > 5)
				{
                    hit = 5f;
				}
                else
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
    bool AllowSpriteSwitch(Color current,Color target)
    {
        return Vector3.Distance(new Vector3(current.r, current.g, current.b) * 100, new Vector3(target.r, target.g, target.b) * 100) <20;
    }
    Color FetchTargetColor(int spriteIndex)
    {
        switch (spriteIndex)
        {
            case 1:
                return Color.white;

            case 2:
                return Color.red;

            case 3:
                return Color.green;

            case 4:
                return Color.blue;
        }
        return Color.black;
        //Fail Color is Black so we will notice.
    }
    void ChangeSprite(int spriteIndex)
	{
        if (!wall)
        {
            Color target=FetchTargetColor(spriteIndex);
            if(spriteRenderer.sprite==sprites[0] || AllowSpriteSwitch(lightSource.color,target))
                spriteRenderer.sprite = sprites[spriteIndex];
            lightSource.color = Color.Lerp(lightSource.color, target, 0.25f);

        }
        
	}

    

}
