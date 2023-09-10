using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyStateMachine : StateMachine
{
  [field:SerializeField] public Animator Animator {get; private set;}
  [field:SerializeField] public NavMeshAgent NavAgent {get; private set;} 
  [field:SerializeField] public ForceReciever ForceReciever {get; private set;}
  [field:SerializeField] public CharacterController Controller {get; private set;} 
  [field:SerializeField] public float PlayerDetectionRange {get; private set;}
  [field:SerializeField] public int MovementSpeed {get; private set;} 
  public GameObject Player {get; private set;}

  private void Start()
  {
    // We dont want these constantly updated as we want more control over position and rotation
    NavAgent.updatePosition = false; 
    NavAgent.updateRotation = false; 
    Player = GameObject.FindGameObjectWithTag("Player"); // will find our player by tag, not best efficiently but not too bad as we only run once
    SwitchState( new EnemyIdleState (this));
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, PlayerDetectionRange); // this will visualise our player detection range in the editor when selected
  }
}
