using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireButton : MonoBehaviour
{
    public GameObject Forcefeld;
    private ParticleSystem system;
    private Collider2D fcollider;
    public static bool ButtonState;
    public bool InitState = false;
    void Start()
    {
        system= Forcefeld.GetComponent<ParticleSystem>();
        fcollider= Forcefeld.GetComponent<Collider2D>();
        ButtonState = InitState;
    }
    public void ToggleButton()
    {
        if (!ButtonState)
        {
            ButtonState = true;
        }
        else if (!deToggling)
        {
            ButtonState = false;
            deToggling = true;
            StartCoroutine(DeToggle());
        }
    }
    bool canClick;
    bool deToggling;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            canClick = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            canClick = false;
        }
        
    }
    public IEnumerator DeToggle()
    {
        while(Camera.main.GetComponentInChildren<Laser>() != null)
            yield return new WaitForSeconds(0.25f);
        deToggling = false;
    }
    void Update()
    {
        if (!deToggling)
        {
            fcollider.enabled = (ButtonState);
            if (!(ButtonState) && system.isPlaying)
            {
                system.Stop();
            }
            if ((ButtonState) && system.isStopped)
            {
                system.Play();
            }

            if (canClick && Input.GetKeyDown(KeyCode.Space))
                ToggleButton();
        }
    }
}
