using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Animator animator;

    private Vector2 m_Movement;

    //Use for input

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnChange;
        
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnChange;
    }

    private void OnChange(GameState newState)
    {
        enabled = newState == GameState.GAMEPLAY;
        animator.enabled = newState == GameState.GAMEPLAY;
    }

    void Update()
    {
        m_Movement.x = Input.GetAxisRaw("Horizontal"); //return val between -1 and 1
        m_Movement.y = Input.GetAxisRaw("Vertical");

        
        if (m_Movement != Vector2.zero && Time.timeScale > 0) {
            animator.SetFloat("Horizontal", m_Movement.x);
            animator.SetFloat("Vertical", m_Movement.y);
        }

        animator.SetFloat("Speed", m_Movement.sqrMagnitude); //set speed to square root of length of vector (optimization)
    }

    //Use for movement calc
    void FixedUpdate()
	{
        rb.MovePosition(rb.position + m_Movement.normalized * moveSpeed * Time.fixedDeltaTime); //"Time.fixedDeltaTime"
	}
    

    public Vector2 GetMovement()
    {
        return m_Movement;
    }

    public void StopHorrizontalMovement()
    {
        m_Movement.x = 0;
    }
    public void StopVertialMovement()
    {
        m_Movement.y = 0;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
