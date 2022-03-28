using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D playerBody;
    Animator playerAnimator;
    public float jumpFactor = .2f;
    public float deceleration = 1f;
    private bool jumpWasPressed;
    public float maxSpeed = 2f;
    
    

    // Acquires body and animator components, freezes player.
    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    
    // Checks for jump input, flags physics for jump in FixedUpdate, and starts animation.
    private void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetButton("Jump"))
        {
            jumpWasPressed = true;
        }
        playerAnimator.SetBool("Flapping", jumpWasPressed);
        
    }

    // Manages player jump either at the start of or in the middle of play.
    void FixedUpdate()
    {
        switch (GameManager.Instance.getCurrentState())
        {
            case GameManager.State.PLAY:
                if (jumpWasPressed)
                {
                    float newUpVelocity = playerBody.velocity.y + jumpFactor;
                    if (newUpVelocity <= maxSpeed)
                    {
                        playerBody.velocity += Vector2.up * jumpFactor;
                    }
                    jumpWasPressed = false;
                }
                else
                {
                    playerBody.velocity += Vector2.down * deceleration;
                }
                break;
            case GameManager.State.INIT:
                // Giving the player an extra boost on their first jump.
                if (jumpWasPressed)
                {
                    GameManager.Instance.SwitchState(GameManager.State.PLAY);
                    playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                    playerBody.velocity += Vector2.up * 1.5f * jumpFactor;
                    jumpWasPressed = false;
                }
                break;
        }
        
    }
}
