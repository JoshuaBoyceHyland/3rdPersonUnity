using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
  
  private Camera MainCamera;

  public List<Target> Targets = new List<Target>(); // will store all targets currently in range of the targets collider
  public Target CurrentTarget {get; private set;}

  [SerializeField] private CinemachineTargetGroup cineTargetGroup; // gives us a reference to the targeting cameras, targeting groups 


  private void Start()
  {
    MainCamera = Camera.main; 
  }

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

    Debug.Log("Selecting target");
    if(Targets.Count != 0) // checking if there is any avaiable targets
    {

      Target closestTarget = null; 
      float closestDistance = Mathf.Infinity; // easier to compare

      foreach(Target target in Targets)
      {
        Vector3 WindowRelativePos = MainCamera.WorldToViewportPoint(target.transform.position); // gets the position of the target relative to the cameras view port

        Debug.Log(WindowRelativePos);
        // 0 and 1 are the bounds of the cameras width and height, if the returned value is not within this range then it is not visible to within the screen
        if(WindowRelativePos.x < 0 
        || WindowRelativePos.x > 1
        || WindowRelativePos.y < 0
        || WindowRelativePos.y > 1
        || WindowRelativePos.z < 0) // checking z prevents a bug where if camera is exactly opposite the target it will flip and select it
        {
          continue; 
        }

        Vector2 ClosestToCenter = new Vector2(WindowRelativePos.x,WindowRelativePos.y) - new Vector2(0.5f, 0.5f); 


        if(ClosestToCenter.sqrMagnitude < closestDistance)
        {
          closestTarget = target;
          closestDistance = ClosestToCenter.sqrMagnitude; // update for next loop
        }
      }

      if(closestTarget != null )
      {
        CurrentTarget = closestTarget;
        cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f); // adds target to target group list
        return true; // then letting the free state know there are available targets
      }
      
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
