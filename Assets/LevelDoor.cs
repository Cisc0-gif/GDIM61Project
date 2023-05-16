using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoor : MonoBehaviour
{
    public bool OpenState = false;
    public bool AlwaysOpen = false;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
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
    public GameObject Collision;
}
