using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerMovement : MonoBehaviour, ButtonPressedChecker
{
    [Header("Character Movement Speed")]
    [SerializeField] private float initialSpeed;
    [SerializeField] private float finalSpeed;
    [SerializeField] private float speedIncreasingThreshold = 0.5f;

    [Space(20)]
    [Header("Character Jump Modifiers")]
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float fallingSpeed = 2.5f;
    
    [Space(20)]
    [Header("Layers to ignore for ground checking and player collider for the measurement")]

    [SerializeField] private LayerMask ignoreLayers;
    [SerializeField] private Collider collider; 
    private float tempSpeed;
    private bool isRightPressed = false;
    private bool isLeftPressed = false;
    private bool isJumpPressed = false;
    [SerializeField] private bool isHit;
    private bool isGrounded;
    private Rigidbody rb;
    private RaycastHit hit;
    
    void Start()
    {
       rb = GetComponent<Rigidbody>();   

       
       tempSpeed = initialSpeed; 
    }

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
    void Update() 
    {
        isGrounded = IsGrounded();
        Debug.Log(rb.velocity.y);
    }
    void FixedUpdate() 
    {
        Move();         
        Jump();
        Gravity();
    }

   public void IsRightPressed(bool isPressed) => isRightPressed = isPressed;

   public void IsLeftPressed(bool isPressed) => isLeftPressed = isPressed;

   void IsJumpPressed(bool isPressed) => isJumpPressed = isPressed;

    void Move()
    {
          if(isRightPressed)
          {
              rb.velocity = new Vector3((initialSpeed * Time.fixedDeltaTime), rb.velocity.y, 0f) ;

              initialSpeed += speedIncreasingThreshold;

              if(initialSpeed >= finalSpeed)
              {
                  initialSpeed = finalSpeed;
              }
          }

          if(isLeftPressed && isGrounded)
          {
              rb.velocity = new Vector3(-(initialSpeed * Time.fixedDeltaTime), rb.velocity.y, 0f);

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

    void Jump()
    {

        if(isJumpPressed && isGrounded)
        {
           rb.velocity = new Vector3(rb.velocity.x, jumpSpeed * Time.fixedDeltaTime, 0);
        }
    }

    bool IsGrounded()
    {
        float maxDistance = 1.2f;
        

        //isHit = Physics.BoxCast(collider.bounds.center, transform.localScale, Vector3.down, out hit, transform.rotation, maxDistance, ignoreLayers);
        //isHit = Physics.BoxCast(collider.bounds.center, transform.localScale, Vector3.down, out hit, Quaternion.identity, maxDistance, ignoreLayers);
        isHit = Physics.Raycast(collider.bounds.center, Vector3.down, out hit, maxDistance, ignoreLayers);
        Debug.DrawRay(collider.bounds.center, Vector2.down* maxDistance, Color.red);
        if(isHit)
          return true;

        else
           return false;
    }

    void OnDrawGizmos() 
    {
        if(isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.down * hit.distance);
           // Gizmos.DrawWireCube(transform.position + Vector3.down* hit.distance, transform.localScale);
            
        }

         else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, Vector3.down * hit.distance);
           // Gizmos.DrawWireCube(transform.position + Vector3.down * hit.distance, transform.localScale);

        }
        
    }

    void Gravity()
    {
      if(rb.velocity.y < 0)
      {
          rb.velocity += new Vector3(0,(Physics.gravity.y * ( fallingSpeed - 1) * Time.fixedDeltaTime), 0f );
      }
        
    }
}
