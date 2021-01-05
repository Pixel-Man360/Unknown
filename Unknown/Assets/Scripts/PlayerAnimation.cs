using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour, ButtonPressedChecker
{

   
    [Header("Animation Speed")]
    [SerializeField] float acceleration= 0.1f;
    [SerializeField] float deceleration= 0.5f;
    [SerializeField] float jumpAnimTransitionTime = 0.8f;
    private Animator animator;
    private bool isRightPressed;
    private bool isLeftPressed;
    
    private bool isJumpPressed;
    private float movingSpeed = 0f;

    private PlayerMovement playerMovement;
    private Rigidbody rigidbody;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable() 
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

    void Update()
    {
      
      MovementAnimation();
      JumpUpAnimation();
      JumpDownAnimation();
      
    }

    public void IsRightPressed(bool isPressed) => isRightPressed = isPressed;

    public void IsLeftPressed(bool isPressed) => isLeftPressed = isPressed;

    void IsJumpPressed(bool isPressed) => isJumpPressed = isPressed;
    void MovementAnimation()
    { 
       
      
       if(playerMovement.IsGrounded())
         {

           animator.Play("Movement");
           if((isRightPressed || isLeftPressed) && movingSpeed < 1.0f)
           { 
            movingSpeed += Time.deltaTime * acceleration;
            animator.SetFloat("Velocity", movingSpeed );
           }

          else if(!isRightPressed && !isLeftPressed && movingSpeed > 0.0f)
          {
            movingSpeed -= Time.deltaTime * deceleration;
            animator.SetFloat("Velocity", movingSpeed); 
          }

          
          if((isRightPressed || isLeftPressed) && movingSpeed > 1.0f)
           { 
             movingSpeed = 1;
           }

          else if(!isRightPressed && !isLeftPressed && movingSpeed < 0.0f)
           {
            movingSpeed = 0;
            
           }
        

         }

          
    }

    void JumpUpAnimation()
    {
      if(isJumpPressed && playerMovement.IsGrounded() && animator.GetCurrentAnimatorStateInfo(0).IsName("Jump Down") == false)
      {
        animator.Play("Jump Up");
        StartCoroutine("ResetJumpAnimation");
      }

    }

    void JumpDownAnimation()
    {
      if(playerMovement.IsGrounded() == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Jump Up") == false)
      {
        animator.Play("Jump Down");
      }
    }

    IEnumerator ResetJumpAnimation()
    {
      yield return new WaitForSeconds(jumpAnimTransitionTime);
      animator.Play("Jump Down");
    } 


   
      
    

}
