using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputReader : MonoBehaviour, Controls.IPlayerActions    // inherits from Monobehaviours, uses controls as an interface 
{
    
    


// needs to be public so statemachine can then be notified of a jump to change 
    public event Action JumpEvent; 
    public event Action DodgeEvent;
    public event Action TargetingToggleEvent; 
    public Vector2 MovementValue{get; private set;}

    private Controls controls; 



    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this); // subscribes this class to the event, will trigger the then interfaced methods 
        controls.Enable();
    }

    private void OnDestroy()
    {
        controls.Disable();
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            JumpEvent?.Invoke(); // if nobody is subscribed this will create an error, thus the need to check if null
        }
        
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            DodgeEvent?.Invoke(); // if nobody is subscribed this will create an error, thus the need to check if null
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnTargetLockToggle(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            TargetingToggleEvent?.Invoke();
        }
    }
}
