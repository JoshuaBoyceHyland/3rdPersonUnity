using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;

    private Collider[] allColliders;
    private Rigidbody[] allRigidBodies;

    // Start is called before the first frame update
    private void Start()
    {
        allColliders = GetComponentsInChildren<Collider>(true);
        allRigidBodies = GetComponentsInChildren<Rigidbody>(true);

        ToggleRagdoll(false);
    }

    public void ToggleRagdoll( bool isRagDoll)
    {
        foreach( Collider collider in allColliders )
        {
            if( collider.gameObject.CompareTag( "Ragdoll"))
            {
                collider.enabled = isRagDoll;
            }
            
        }

        foreach( Rigidbody rigidBody in allRigidBodies )
        {
            if( rigidBody.gameObject.CompareTag( "Ragdoll"))
            {
                rigidBody.isKinematic = !isRagDoll;
                rigidBody.useGravity = isRagDoll;
            }
            
        }

        controller.enabled = !isRagDoll;
        animator.enabled = !isRagDoll;
    }

}
