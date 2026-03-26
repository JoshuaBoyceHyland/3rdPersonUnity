using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ForceReciever : MonoBehaviour
{
    private float YAxisVelocity;
    private Vector3 impact;
    private Vector3 dampingVelocity;

        
    [SerializeField] CharacterController CharacterController;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float drag = 0.3f; 

    public Vector3 Movement => impact + Vector3.up * YAxisVelocity; // when accessed by states will return a vector3 with only a y value, which will be our gravity effect


    public void Update()
    {
        if(YAxisVelocity < 0f && CharacterController.isGrounded) // is on the ground and Y Vel is lower than 0
        {
            // if this was set to 0, character will always be constintely falling if there is a minor slope, this way is set to a small - float
            YAxisVelocity = Physics.gravity.y * Time.deltaTime; 
        
        }
        else
        {
            YAxisVelocity += Physics.gravity.y * Time.deltaTime; // gravity can be adjusted in engine 
        }

        impact = Vector3.SmoothDamp(impact ,Vector3.zero, ref dampingVelocity, drag ); // passing in the current value, the desired value, a reference to the damping velocity and the drag

        if( agent != null )
        {
            if( impact.sqrMagnitude < 0.2f * 0.2f)
            {
                impact = Vector3.zero;
                agent.enabled = true;
            }
            
        }
    }

    public void AddForce(Vector3 force)
    {
        impact += force;

        if( agent != null )
        {
            agent.enabled = false;
        }
    }
}
