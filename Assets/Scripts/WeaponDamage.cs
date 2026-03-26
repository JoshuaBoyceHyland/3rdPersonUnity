using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    private int damage = new int();
    private float knockback = new float();
    [SerializeField] private Collider player; // neccesary to make sure you are not colliding with yourself
    [SerializeField] private List<Collider> alreadyCollidedWith;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other == player){return;}

        // if it is something we have already damaged in the attack we don't want to damage it constantly
        if(alreadyCollidedWith.Contains(other)){return;}  
        
        alreadyCollidedWith.Add(other);

        if(other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }

        if( other.TryGetComponent<ForceReciever>(out ForceReciever ForceReciever )) 
        {
            Vector3 dir = ( other.transform.position - player.transform.position).normalized ;
            ForceReciever.AddForce( dir * knockback);
        }
    
    
    }

    // this is called by the engine when the script is enabled by the event we set up in the animations
    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    public void SetAttack(int attackDamage, float attackKnockback)
    {
        damage = attackDamage;
        knockback = attackKnockback;
    }
}
