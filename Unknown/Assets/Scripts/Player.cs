using System.Collections;
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
    private bool isDead = false;

    
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

        if(!isDead)
        {
          Move();
          Jump();
          Rotation();
          AnimateRunning();
          AnimateJump();
          AnimateFalling();
        }

        Gravity();
        
    }

    #endregion


    #region Movement Controller
    void Move()
    {
        if(isGrounded)
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
        
    }

    void Jump()
    {
        if(isJumpPressed && isGrounded)
        {
            playerMovement.Jump();
            if(playerAnimation.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump Down") == false)
            {
               playerAnimation.JumpUpAnimation();
            }
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
      if(isGrounded)
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
        
    }


    #endregion
 

   #region Movement Animation Controller
    
    void AnimateRunning()
    {
        
       if(playerMovement.IsGrounded())
         {

           playerAnimation.Idle();

           if((isRightPressed || isLeftPressed) && playerAnimation.movingSpeed < 1.0f)
           { 
             playerAnimation.MovementButtonPressed();
           }

          else if(!isRightPressed && !isLeftPressed && playerAnimation.movingSpeed > 0.0f)
          {
            playerAnimation.MovementButtonNotPressed();
          }

          
          if((isRightPressed || isLeftPressed) && playerAnimation.movingSpeed > 1.0f)
           { 
             playerAnimation.ResetMovingAnimationSpeed(1);
           }

          else if(!isRightPressed && !isLeftPressed && playerAnimation.movingSpeed < 0.0f)
           {
            playerAnimation.ResetMovingAnimationSpeed(0);
           }

         }
    }


    void AnimateJump()
    {
        if(isJumpPressed && isGrounded && playerAnimation.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump Down") == false)
        {
            playerAnimation.JumpUpAnimation();
        }
    }

    void AnimateFalling()
    {
        if(playerMovement.IsGrounded() == false && playerAnimation.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump Up") == false)
        {
          playerAnimation.FallingAnimation();
        }
    }
   #endregion
}
