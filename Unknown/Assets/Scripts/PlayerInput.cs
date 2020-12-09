using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event Action<bool> OnRightKeyPressed;
    public static event Action<bool> OnRightKeyNotPressed;

    public static event Action<bool> OnLeftKeyPressed;
    public static event Action<bool> OnLeftKeyNotPressed;

    public static event Action OnJumpKeyPressed;


    void Update()
    {
        if((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && OnRightKeyPressed != null)
           OnRightKeyPressed(true);

        if((Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) && OnRightKeyNotPressed != null)
           OnRightKeyNotPressed(false);

        if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && OnLeftKeyPressed != null)
           OnLeftKeyPressed(true);

        if((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) && OnLeftKeyNotPressed != null)
           OnLeftKeyNotPressed(false);

        if(Input.GetKeyDown(KeyCode.Space) && OnJumpKeyPressed != null)
           OnJumpKeyPressed();
    }
}
