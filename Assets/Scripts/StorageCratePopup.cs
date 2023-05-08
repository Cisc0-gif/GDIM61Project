using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageCratePopup : MonoBehaviour
{
    [SerializeField]
    private float distThreshold;

    [SerializeField]
    private Animator animator;

    private GameObject playerPos;
    private Vector3 cratePos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player");
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

        Debug.Log(deltaPos);
        
    }
}