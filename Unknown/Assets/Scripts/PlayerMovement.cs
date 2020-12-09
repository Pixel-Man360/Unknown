using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, ButtonPressedChecker
{
    [SerializeField] private float initialSpeed;
    [SerializeField] private float finalSpeed;
    [SerializeField] private float speedIncreasingThreshold = 0.5f;
    private float tempSpeed;
    private bool isRightPressed = false;
    private bool isLeftPressed = false;
    private bool isJumpPressed = false;

    private Rigidbody rb;

    void Start()
    {
       rb = GetComponent<Rigidbody>();   
       tempSpeed = initialSpeed; 
    }

    void OnEnable() 
    {
        PlayerInput.OnRightKeyPressed += IsRightPressed;
        PlayerInput.OnRightKeyNotPressed += IsRightPressed;

        PlayerInput.OnLeftKeyPressed += IsLeftPressed;
        PlayerInput.OnLeftKeyNotPressed += IsLeftPressed;
    }
    void OnDisable() 
    {
         PlayerInput.OnRightKeyPressed -= IsRightPressed;
         PlayerInput.OnRightKeyNotPressed -= IsRightPressed;

         PlayerInput.OnLeftKeyPressed -= IsLeftPressed;
         PlayerInput.OnLeftKeyNotPressed -= IsLeftPressed;
    }
    void FixedUpdate() 
    {
        
        Move();         
    }

   public void IsRightPressed(bool isPressed) => isRightPressed = isPressed;

    public void IsLeftPressed(bool isPressed) => isLeftPressed = isPressed;

    void Move()
    {
          if(isRightPressed)
          {
              rb.velocity = Vector3.right * initialSpeed * Time.fixedDeltaTime;

              initialSpeed += speedIncreasingThreshold;

              if(initialSpeed >= finalSpeed)
              {
                  initialSpeed = finalSpeed;
              }
          }

          if(isLeftPressed)
          {
              rb.velocity = Vector3.left * initialSpeed * Time.fixedDeltaTime;

               initialSpeed += speedIncreasingThreshold;
              
              if(initialSpeed >= finalSpeed)
              {
                  initialSpeed = finalSpeed;
              }
          }

          else if(!isLeftPressed && !isRightPressed)
          {
              initialSpeed = tempSpeed;
          }
    }
}
