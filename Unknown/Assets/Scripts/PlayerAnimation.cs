﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour, ButtonPressedChecker
{

    [SerializeField] float acceleration= 0.1f;
    [SerializeField] float deceleration= 0.5f;
    
    private Animator animator;
    private bool isRightPressed;
    private bool isLeftPressed;
    
    private float movingSpeed = 0f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() 
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

    void Update()
    {
        MovementAnimation();
    }

    public void IsRightPressed(bool isPressed) => isRightPressed = isPressed;

    public void IsLeftPressed(bool isPressed) => isLeftPressed = isPressed;

    
    void MovementAnimation()
    {
        
        
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