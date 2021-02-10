using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{

   
    [Header("Animation Speed")]
    [SerializeField] internal float acceleration= 0.1f;
    [SerializeField] internal float deceleration= 0.5f;
    [SerializeField] float jumpAnimTransitionTime = 0.8f;

    internal Animator animator;
    private bool isRightPressed;
    private bool isLeftPressed;
    
    private bool isJumpPressed;
    internal float movingSpeed = 0f;

    private PlayerMovement playerMovement;
    private Rigidbody rigidBody;
    private Player playerController;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        rigidBody = GetComponent<Rigidbody>();
        playerController = GetComponent<Player>();
    }

    internal void Idle()
    {
      animator.Play("Movement");
    }
    internal void MovementButtonPressed()
    { 
       movingSpeed += Time.deltaTime * acceleration;
       animator.SetFloat("Velocity", movingSpeed );  
    }

    internal void MovementButtonNotPressed()
    {
       movingSpeed -= Time.deltaTime * deceleration;
       animator.SetFloat("Velocity", movingSpeed); 
    }

    internal void ResetMovingAnimationSpeed(int speed)
    {
      movingSpeed = speed;
    }

    internal void JumpUpAnimation()
    {
        animator.Play("Jump Up");
        StartCoroutine("ResetJumpAnimation");
    }

    internal void FallingAnimation()
    {
        animator.Play("Jump Down");
    }

    IEnumerator ResetJumpAnimation()
    {
      yield return new WaitForSeconds(jumpAnimTransitionTime);
      animator.Play("Jump Down");
    } 


   
      
    

}
