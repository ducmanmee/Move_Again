using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private string currentAnim;
    public Animator animCharacter;

    bool isDead;
    public bool IsDead() => isDead;

    private Character target;
    public Character Target => target;
    public CharacterSight sightCharacter; 


    public virtual void OnInit() 
    { 

    }
    public virtual void OnDespawn()
    {

    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animCharacter.ResetTrigger(currentAnim);
            currentAnim = animName;
            animCharacter.SetTrigger(currentAnim);
        }
    }

    public void SetTargetCharacter(Character C)
    {
        this.target = C;
    }    

    public virtual void Move()
    {

    }

    /*public void ChangeWeapon(WeaponType weaponType)
    {

    }*/ 
        
    public virtual void OnHit(int damage)
    {

    }    

    public virtual void OnDeath()
    {

    }

    public virtual void SetSize(float size)
    {

    } 
        
    public virtual void UpSize(float increment)
    {

    }  
    
    public virtual void Attack()
    {
        
    } 
    
    public virtual void ThrowWeapon()
    {

    } 

        
}
