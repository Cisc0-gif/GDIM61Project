using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireButton : MonoBehaviour
{

    [SerializeField]
    private CameraFollow followCam;

    public GameObject Forcefeld;
    private ParticleSystem system;
    private Collider2D fcollider;
    public static bool ButtonState;
    public bool InitState = false;
    public AudioSource buttonPress;
    public AudioSource forceField;
    public AudioSource forceFieldOpen;

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
            buttonPress.Play();
            forceFieldOpen.Play();
            ButtonState = true;
            forceField.Play();
        }
        else if (!deToggling)
        {
            buttonPress.Play();
            forceFieldOpen.Play();
            ButtonState = false;
            deToggling = true;
            StartCoroutine(DeToggle());
            forceField.Stop();
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
                followCam.UncenterCam();
                system.Stop();
            }
            if ((ButtonState) && system.isStopped)
            {
                followCam.CenterCam();
                system.Play();
            }

            if (canClick && Input.GetKeyDown(KeyCode.Space))
                
                ToggleButton();
        }
    }
}
