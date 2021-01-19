using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour 
{
    [SerializeField] private float rotationSpeed;
    

    public void RotateRight()
    {   
       transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,90,0), rotationSpeed * Time.deltaTime);
    }

    public void RotateLeft()
    {
       transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,-90,0), rotationSpeed * Time.deltaTime);
    }
}
