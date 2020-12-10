using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour, ButtonPressedChecker  
{
    [SerializeField][Range(0.1f, 5f)] private float rotationSpeed;

    private bool isRightPressed = false;
    private bool isLeftPressed = false;
    
    void OnEnable() 
    {
        PlayerInput.OnRightKeyPressed += IsRightPressed;

        PlayerInput.OnLeftKeyPressed += IsLeftPressed;
    }

    void OnDisable() 
    {
         PlayerInput.OnRightKeyPressed -= IsRightPressed;

         PlayerInput.OnLeftKeyPressed -= IsLeftPressed;
    }

    void Update()
    {
      Rotate();
    }

    public void IsRightPressed(bool isPressed) => isRightPressed = isPressed;

    public void IsLeftPressed(bool isPressed) => isLeftPressed = isPressed;

    void Rotate()
    {

          if(isRightPressed)
          {
              transform.rotation =  transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,90,0), rotationSpeed * Time.deltaTime);
          }
          if(isLeftPressed)
          {
              transform.rotation =  transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,-90,0), rotationSpeed * Time.deltaTime);
          }
    }
}
