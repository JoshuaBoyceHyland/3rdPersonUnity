using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
  public List<Target> Targets = new List<Target>(); // will store all targets currently in range of the targets collider


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

}
