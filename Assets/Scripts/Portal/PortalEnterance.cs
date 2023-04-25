using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PortalEnterance : MonoBehaviour
{
    [SerializeField]
    private PortalExit Exit;

    private PortalObject PlacedObject;

    [SerializeField]
    private SpriteRenderer Renderer;

    [SerializeField]
    private RandomPattern Frequency;

    [SerializeField]
    private RandomPattern Strength;

    //public Action<Vector3> RotateObject;
    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Renderer.material.SetVector("_Frequency", Frequency.getRandom());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetExit(PortalExit Exit)
    {
        this.Exit = Exit;
    }

    public void PlaceObject(PortalObject obj)
    {
        PlacedObject = obj;
        PlacedObject.SetExit(Exit);
        Exit.ParodyObject(PlacedObject.GetWorldObject());
    }

    public void RemoveObject()
    {
        PlacedObject.SetExit(null);
        PlacedObject = null;
        Exit.RemoveObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered");
        if (collision.GetComponent<PortalObject>() != null)
        {
            Debug.Log("Portal Obj Entered");
            collision.transform.position -= new Vector3(0f, 0f, transform.position.z + 1);
            PlaceObject(collision.GetComponent<PortalObject>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.GetComponent<PortalObject>() != null)
        {
            RemoveObject();
        }
    }

    public void SwitchMaterial(Color color)
    {
        Renderer.material.SetColor("_BGColor", color);
        Exit.UpdateTexture(Renderer.material);
    }
}


[System.Serializable]
public class RandomPattern {

    [SerializeField]
    private Vector2 MinValue;
    [SerializeField]
    private Vector2 MaxValue;

    public RandomPattern (Vector2 MinValue, Vector2 MaxValue) { 
        this.MinValue = MinValue;
        this.MaxValue = MaxValue;
    }

    public Vector2 getRandom()
    {
        return new Vector2(Random.Range(MinValue.x, MaxValue.x), Random.Range(MinValue.y, MaxValue.y));
    }
}