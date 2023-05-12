using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class LaserSplitter : MonoBehaviour
{
    public GameObject LaserPrefab;
    public float Output1_DeflectionAngle = 45;
    public float Output2_DeflectionAngle = -45;
    public float Output3_DeflectionAngle = 0;
    public Gradient Red;
    public Gradient Blue;
    public Gradient Green;

    private Light2D lightSource;
    private SpriteRenderer spriteRenderer;
    public float hit;
    public Sprite[] sprites;

    void Start()
    {
        if (GetComponent<Light2D>() != null)
        {
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
        EnableLight();
        if (!other.HasBeenSplit)
        {
            //Split the beam into its three sub parts, Red Blue and Green.
            other.HasBeenSplit = true;
            GameObject output1 = Instantiate(LaserPrefab, this.transform.position, other.transform.rotation, this.transform);
            output1.transform.eulerAngles += new Vector3(0, 0, Output1_DeflectionAngle);
            GameObject output2 = Instantiate(LaserPrefab, this.transform.position, other.transform.rotation, this.transform);
            output2.transform.eulerAngles += new Vector3(0, 0, Output2_DeflectionAngle);
            GameObject output3 = Instantiate(LaserPrefab, this.transform.position, other.transform.rotation, this.transform);
            output3.transform.eulerAngles += new Vector3(0, 0, Output3_DeflectionAngle);

            output1.GetComponent<Laser>().HasBeenSplit = true;
            output2.GetComponent<Laser>().HasBeenSplit = true;
            output3.GetComponent<Laser>().HasBeenSplit = true;
            output1.GetComponent<Laser>().SetColor(Red, "Red");
            output2.GetComponent<Laser>().SetColor(Blue, "Blue");
            output3.GetComponent<Laser>().SetColor(Green, "Green");
            other.DestroyMe();
        }
    }

    void Update()
	{
        if (hit < 0.5f) //value is added and reduced to stay above .5 when lit, else light turns off
        {
            DisableLight();
            ChangeSprite(0);
            hit = 0;
        }
        else if (hit > 5)
        {
            hit = 5f;
        }
        else
        {
            hit -= 0.5f;
        }
    }

    public void ChangeSprite(int spriteIndex)
	{
        if (spriteIndex > 0)
		{
            spriteIndex = 1;
		}
        spriteRenderer.sprite = sprites[spriteIndex];
	}

    public void EnableLight()
	{
        lightSource.enabled = true;
    }

    public void DisableLight()
	{
        lightSource.enabled = false;
    }
}
