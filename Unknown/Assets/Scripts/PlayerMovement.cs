using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Character Movement Speed")]
    [SerializeField] private float initialSpeed;
    [SerializeField] private float finalSpeed;
    [SerializeField] private float speedIncreasingThreshold = 0.5f;

    [Space(20)]

    [Header("Character Jump Modifiers")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float fallingSpeed = 2.5f;
    
    [Space(20)]

    [Header("Collider for the measurement of ground check")]
    [SerializeField] private Collider collider3d; 

    [Space(20)]
    [Header("Layers for ground checking")]

    [SerializeField] private LayerMask layerMask;

    private float tempSpeed;
    private bool isHit;
    private bool isGrounded;
    private Rigidbody rb;
    private RaycastHit hit;

    private Player playerController;
    
    void Start()
    {
       rb = GetComponent<Rigidbody>();   
       collider3d = GetComponent<Collider>();
       playerController = GetComponent<Player>();
       
       tempSpeed = initialSpeed; 
    }
   

    public void MoveLeft()
    {
   
       rb.velocity = new Vector3(-(initialSpeed * Time.fixedDeltaTime), rb.velocity.y, 0f);

       initialSpeed += speedIncreasingThreshold;
              
       if(initialSpeed >= finalSpeed)
       {
         initialSpeed = finalSpeed;
       }

    }

    public void MoveRight()
    {
       
        rb.velocity = new Vector3((initialSpeed * Time.fixedDeltaTime), rb.velocity.y, 0f) ;

        initialSpeed += speedIncreasingThreshold;

        if(initialSpeed >= finalSpeed)
        {
            initialSpeed = finalSpeed;
        }
         
    }

    public void Stop()
    {
       initialSpeed = tempSpeed;
    } 

    public void Jump()
    {

       rb.velocity = new Vector3(rb.velocity.x, jumpHeight * Time.fixedDeltaTime, 0);
  
    }

    public bool IsGrounded()
    {
        float maxDistance = 1f;
        
        isHit = Physics.Raycast(collider3d.bounds.center, Vector3.down, out hit, maxDistance,layerMask);

        Debug.DrawRay(collider3d.bounds.center, Vector2.down* maxDistance, Color.red);
        if(isHit)
          return true;

        else
           return false;
    }

   /* void OnDrawGizmos() 
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
        
    } */

    public void Gravity()
    {
        Debug.Log(rb.velocity.y);
        
        if(rb.velocity.y < 0)
        {
            rb.velocity += new Vector3(0,(Physics.gravity.y * ( fallingSpeed - 1) * Time.fixedDeltaTime), 0f );  
        }
       
    }

    
}
