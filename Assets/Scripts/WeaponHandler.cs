using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
  [SerializeField] private GameObject Weapon;

  public void EnableWeapon()
  {
    Weapon.SetActive(true);
  }

  public void DisableWeapon()
  {
    Weapon.SetActive(false);
  }
}
