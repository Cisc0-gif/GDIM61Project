using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    [SerializeField]
    private Material Pattern;

    [SerializeField]
    private PortalEnterance PortalEnterance;
    [SerializeField]
    private PortalExit PortalExit;
    void Start()
    {
        PortalEnterance.GetComponent<SpriteRenderer>().material = Pattern;
        PortalExit.GetComponent<SpriteRenderer>().material = Pattern;
        PortalEnterance.SetExit(PortalExit);
        PortalExit.SetEnterance(PortalEnterance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
