using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    #region Singleton
    public static MouseManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;   
        }
    }
    #endregion

    public static Vector2 MousePos2D = Vector2.zero;

    private ClickableObject Selected;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos2D = new Vector2(mousePos.x, mousePos.y);
        if (Input.GetMouseButtonDown(0)) {

            if (Selected != null)
            {
                Selected.Click();
                Selected = null;
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(MousePos2D, Vector2.zero);
                
                if (hit.collider.GetComponent<ClickableObject>() != null)
                {
                    Selected = hit.collider.GetComponent<ClickableObject>();
                    Selected.Click();
                }
            }
        }

    }
}

public interface ClickableObject
{
    public void Click();
}
