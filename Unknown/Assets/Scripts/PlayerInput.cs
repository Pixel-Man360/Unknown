using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event Action<bool> OnRightKeyPressed;
    public static event Action<bool> OnLeftKeyPressed;
    public static event Action<bool> OnJumpKeyPressed;
    //public static event Action<bool> OnJumpKeyNotPressed;


    void Update()
    {
        if((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && OnRightKeyPressed != null)
           OnRightKeyPressed(true);

        if((Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) && OnRightKeyPressed != null)
           OnRightKeyPressed(false);

        if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && OnLeftKeyPressed != null)
           OnLeftKeyPressed(true);

        if((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) && OnLeftKeyPressed != null)
           OnLeftKeyPressed(false);


         bool jump = Input.GetKey(KeyCode.Space);
        if(OnJumpKeyPressed != null)
           OnJumpKeyPressed(jump);

      //  if(Input.GetKeyUp(KeyCode.Space) && OnJumpKeyNotPressed != null)
         // OnJumpKeyNotPressed(false);

        
    }
}
