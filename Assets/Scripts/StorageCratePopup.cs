using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StorageCratePopup : MonoBehaviour
{
    [SerializeField]
    private float distThreshold;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject textParent;

    private GameObject playerPos;
    private Vector3 cratePos;
    private Light2D lightSource;
    public AudioSource PopUpSound;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player");
		textParent.SetActive(false);
        if (GetComponent<Light2D>() != null)
		{
            lightSource = GetComponent<Light2D>();
            lightSource.enabled = false;
		}

	}

	// Update is called once per frame
	void Update()
    {
        cratePos = gameObject.transform.position;

        float deltaPos = Vector3.Distance(cratePos, playerPos.transform.position);

        if (deltaPos < distThreshold)
		{
            animator.SetFloat("Distance", 1);
        } else
		{
            animator.SetFloat("Distance", -1);
		}

        /*
        AnimatorClipInfo[] animClip = animator.GetCurrentAnimatorClipInfo(0); //clip weight = % of clip played
        int currentFrame = (int)(animClip[0].weight * (animClip[0].clip.length * animClip[0].clip.frameRate)); //% of clip played * (length of clip * frame rate/sample rate)) OR (% * (t * r))

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime == 1f)
		{
            textParent.SetActive(true);
        }
        else
        {
            textParent.SetActive(false);
        }

        if (currentFrame == 0)
		{
            textParent.SetActive(true);
        }
        else
        {
            textParent.SetActive(false);
        }
        */

        //Debug.Log(deltaPos);
        
    }

    public void OnAnimStart()
	{
        if (animator.GetFloat("Distance") == 1)
        {
            PopUpSound.Play();
            lightSource.enabled = true;
        }
        else
        {
            PopUpSound.Play();
            lightSource.enabled = false;
        }
    }

    public void OnAnimEnd()
	{
        if (animator.GetFloat("Distance") == 1)
		{
            textParent.SetActive(true);
            gameObject.transform.parent.gameObject.GetComponent<PrismStorage>().UpdateVisuals();
            lightSource.enabled = true;
        } else
		{
            textParent.SetActive(false);
            lightSource.enabled = false;
        }
        
    }

}