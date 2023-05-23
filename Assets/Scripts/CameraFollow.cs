using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float followSize;

    [SerializeField]
    private bool centered;

    [SerializeField]
    private float centerSize;

    [SerializeField]
    private float trackRate;

    [SerializeField]
    private Vector2 centerOffset;

    private Camera OrthoCam;

    // Start is called before the first frame update
    void Start()
    {
        OrthoCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!centered)
        {
            if (target != null)
            {
                if (OrthoCam.orthographicSize > followSize)
				{
                    OrthoCam.orthographicSize -= .1f;
				}
                float xTo = target.transform.position.x;
                float yTo = target.transform.position.y;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + (xTo - gameObject.transform.position.x) / trackRate, gameObject.transform.position.y + (yTo - gameObject.transform.position.y) / trackRate, transform.position.z);
            }
        }
        else
		{
            if (OrthoCam.orthographicSize < centerSize)
            {
                OrthoCam.orthographicSize += .1f;
            }
            //gameObject.transform.position = new Vector3(0, 0, -10);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + ((0 + centerOffset.x) - gameObject.transform.position.x) / trackRate, gameObject.transform.position.y + ((0 + centerOffset.y) - gameObject.transform.position.y) / trackRate, -10);
        }
    }

    public void CenterCam()
	{
        centered = true;
	}

    public void UncenterCam()
	{
        centered = false;
	}
}
