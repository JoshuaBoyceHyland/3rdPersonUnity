using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
  
  public List<Target> Targets = new List<Target>(); // will store all targets currently in range of the targets collider
  public Target CurrentTarget {get; private set;}

  [SerializeField] private CinemachineTargetGroup cineTargetGroup; // gives us a reference to the targeting cameras, targeting groups 

  private void OnTriggerEnter(Collider Other)
  {
    // checks if other has a Target component, if it does it will put it into the InrAgneObject 
    if(Other.TryGetComponent<Target>(out Target InRangeTarget))
    {
      Targets.Add(InRangeTarget); // adds the ned in range target to our list of targets
      InRangeTarget.OnDestroyed+=RemoveTarget; // subsccribes us to the this targets destroyed event 
    }
    
  }

  private void OnTriggerExit(Collider Other)
  {
    if(Other.TryGetComponent<Target>(out Target TargetLeavingRange))
    {
      //Targets.Remove(TargetLeavingRange);
      RemoveTarget(TargetLeavingRange); 
    }
  }

  public bool SelectTarget()
  {
    if(Targets.Count != 0) // checking if there is any avaiable targets
    {
      CurrentTarget = Targets[0]; // if there are we are selecting the first 
      cineTargetGroup.AddMember(Targets[0].transform, 1f, 2f); // adds target to target group list
      return true; // then letting the free state know there are available targets
    }

    return false; 
  }


  public void Cancel()
  {
    // simple safe gaurd
    if(CurrentTarget != null)
    {
      cineTargetGroup.RemoveMember(CurrentTarget.transform);
      CurrentTarget = null;
    }
  }

  private void RemoveTarget(Target target)
  {
    // checking to see if this is our currently focused on target
    if(CurrentTarget == target)
    {
      cineTargetGroup.RemoveMember(CurrentTarget.transform); // if so we remove it from out tagreted group
      CurrentTarget = null; // make it null
    }
    target.OnDestroyed-=RemoveTarget; // either way we unsubscribe from it
    Targets.Remove(target); // then remove it from out list 
  }
}
