using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReciever : MonoBehaviour
{
    private float YAxisVelocity;

    public Vector3 Movement => Vector3.up * YAxisVelocity; // when accessed by states will return a vector3 with only a y value, which will be our gravity effect

    [SerializeField] CharacterController CharacterController;
    public void Update()
    {
        if(YAxisVelocity < 0f && CharacterController.isGrounded) // is on the ground and Y Vel is lower than 0
        {
            // if this was set to 0, character will always be constintely falling if there is a minor slope, this way is set to a small - float
            YAxisVelocity = Physics.gravity.y * Time.deltaTime; 
            Debug.Log(Physics.gravity.y * Time.deltaTime);
        }
        else
        {
            YAxisVelocity += Physics.gravity.y * Time.deltaTime; // gravity can be adjusted in engine 
        }
    }
}
