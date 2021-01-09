﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(PlayerAnimation ))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerRotation))]
public class Player : MonoBehaviour, ButtonPressedChecker
{
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerRotation playerRotation;

    private bool isRightPressed = false;
    private bool isLeftPressed = false;
    private bool isJumpPressed = false;
    private bool isGrounded = false;
    

    
    #region Assignment or calling methods
    

    void OnEnable() 
    {
        PlayerInput.OnRightKeyPressed += IsRightPressed;
        
        PlayerInput.OnLeftKeyPressed += IsLeftPressed;
        
        PlayerInput.OnJumpKeyPressed += IsJumpPressed;
     

    }
    void OnDisable() 
    {
         PlayerInput.OnRightKeyPressed -= IsRightPressed;

         PlayerInput.OnLeftKeyPressed -= IsLeftPressed;

         PlayerInput.OnJumpKeyPressed -= IsJumpPressed;
       
    }


    public void IsRightPressed(bool isPressed) => isRightPressed = isPressed;

    public void IsLeftPressed(bool isPressed) => isLeftPressed = isPressed;

    public void IsJumpPressed(bool isPressed) => isJumpPressed = isPressed;


    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMovement  = GetComponent<PlayerMovement>();
        playerRotation  = GetComponent<PlayerRotation>();

    }

    void FixedUpdate() 
    {
        isGrounded = playerMovement.IsGrounded();


        Move();
        Jump();
        Gravity();
        Rotation();
    }

    #endregion

    
    

    #region Movement Logic
    void Move()
    {
        if(isRightPressed)
        {
            playerMovement.MoveRight();
        }

        else if(isLeftPressed)
        {
            playerMovement.MoveLeft();
        }

        else if(!isLeftPressed && !isRightPressed)
        {
            playerMovement.Stop();
        }
    }

    void Jump()
    {
        if(isJumpPressed && isGrounded)
        {
            playerMovement.Jump();
        }
    }

    void Gravity()
    {
        if(playerMovement.GetYVelocity() < 0)
        {
            playerMovement.Gravity();
        }
    }

    void Rotation()
    {
        if(isRightPressed)
        {
            playerRotation.RotateRight();
        }

        else if(isLeftPressed)
        {
            playerRotation.RotateLeft();
        }
    }


    #endregion
 
 

    ///////////// Movement Animation ///////////////
   
}
