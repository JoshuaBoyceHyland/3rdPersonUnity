using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
  public List<Target> Targets = new List<Target>(); // will store all targets currently in range of the targets collider
  public Target CurrentTarget {get; private set;}

  private void OnTriggerEnter(Collider Other)
  {
    // checks if other has a Target component, if it does it will put it into the InrAgneObject 
    if(Other.TryGetComponent<Target>(out Target InRangeTarget))
    {
      Targets.Add(InRangeTarget);
    }
    
  }

  private void OnTriggerExit(Collider Other)
  {
    if(Other.TryGetComponent<Target>(out Target ObjectLeavingRange))
    {
      Targets.Remove(ObjectLeavingRange);
    }
  }

  public bool SelectTarget()
  {
    if(Targets.Count != 0) // checking if there is any avaiable targets
    {
      CurrentTarget = Targets[0]; // if there are we are selecting the first 
      return true; // then letting the free state know there are available targets
    }

    return false; 
  }


    public void Cancel()
    {
      CurrentTarget = null; // 
    }

}
