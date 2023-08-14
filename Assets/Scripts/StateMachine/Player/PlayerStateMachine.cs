using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    // field: makes it a field as it isn't already so then SerializeField can then make it 
    // field in the editor where we can give it a reference to the InputReader
    // the curly braces after the variable makes it a property that we can specify its access modifiers even more so
    // in this case it can be publicly got but only privately set by the Player state machine
    [field:SerializeField] public InputReader InputReader {get; private set;} 
    [field:SerializeField] public CharacterController Controller {get; private set;} 
    [field:SerializeField] public Animator Animator {get; private set;} 
    [field:SerializeField] public Targeter Targeter {get; private set;} 
    [field:SerializeField] public ForceReciever ForceReciever {get; private set;}
    [field:SerializeField] public float StandardMovementSpeed {get; private set;} 
    [field:SerializeField] public float TagretingMovementSpeed {get; private set;} 
    [field:SerializeField] public float RotationSmooth {get; private set;} 
    
    public Transform MainCameraTransform{get; private set;} 
    // Start is called before the first frame update
    void Start()
    {
        MainCameraTransform = Camera.main.transform; // setting mainCAmeraTransform reference at start
        SwitchState(new PlayerFreeLookState(this));
    }
}
