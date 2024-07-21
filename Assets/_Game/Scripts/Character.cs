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

    [SerializeField] WeaponSODatas weaponSODatas;
    WeaponBase curWeapon;
    WeaponType curWeaponType;
    [SerializeField] Transform weaponHolder;

    [SerializeField] Transform attackPoint;

    private void Start()
    {
        OnInit();
    }

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

    public void ChangeWeapon(WeaponType weaponType)
    {
        curWeaponType = weaponType;
        if(curWeapon != null)
        {
            Destroy(curWeapon.gameObject);
        }

        curWeapon = Instantiate(weaponSODatas.GetPrefab(weaponType), weaponHolder);        
    }

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
        ChangeAnim(Constain.ANIM_ATTACK);
        this.transform.LookAt(Target.transform);
        StartCoroutine(ThrowWeapon(curWeaponType));
    } 
    
    IEnumerator ThrowWeapon(WeaponType weaponType)
    {
        yield return new WaitForSeconds(.4f);
        weaponSODatas.GetBulletPrefab(weaponType);
    } 

        
}
