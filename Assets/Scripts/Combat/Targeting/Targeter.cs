using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
  public List<Target> Targets = new List<Target>(); // will store all targets currently in range of the targets collider


  private void OnTriggerEnter(Collider Other)
  {
    Target InRangeObject = Other.GetComponent<Target>(); // gets the Target component if there is any 
    Debug.Log("target enter");
    if(InRangeObject != null) // if this variable is null means that this is not a releveant object to out targeter
    {
        Targets.Add(InRangeObject); // adds object to list of targets
    }
  }

  private void OnTriggerExit(Collider Other)
  {
    Target ObjectLeavingRange = Other.GetComponent<Target>(); // gets the Target component if there is any
    Debug.Log("target exit");
    if(ObjectLeavingRange != null) // if this variable is null means that this is not a releveant object to out targeter
    {
        Targets.Remove(ObjectLeavingRange); // adds object to list of targets
    }

    /*
    could also be done 

    if(ObjectLeavingRange == null){return} 
    
     Targets.Remove(ObjectLeavingRange);
    */
  }

}
