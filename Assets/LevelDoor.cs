using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{

    [SerializeField]
    private Light2D lightSource;

    [SerializeField]
    private bool EntranceDoor;

    public bool OpenState = false;
    public bool AlwaysOpen = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        int LevelIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(LevelIndex);
        if (!EntranceDoor)
        {
            OpenState = LevelManager.Instance.CompletedLevels[LevelIndex];
        }
        
        Synchronize();
    }

    public void ToggleDoor()
    {
        OpenState = !OpenState;
        Synchronize();
    }

    private void Synchronize()
    {
        if (OpenState)
            animator.Play("Open");
        else if (!AlwaysOpen)
            animator.Play("Close");
        Collision.SetActive(!OpenState);
    }

    void Update()
    {
        if (!EntranceDoor) {
            if (!OpenState)
            {
                lightSource.color = new Color(1f, 0, 0, 1f);
            }
            else
            {
                lightSource.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    public GameObject Collision;
}
