using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private string currentAnim;
    public Animator animCharacter;

    bool isDead;
    
    public bool isMoving;


    private Character target;
    public Character Target => target;
    public CharacterSight sightCharacter;

    [SerializeField] WeaponSODatas weaponSODatas;
    WeaponBase curWeapon;
    WeaponType curWeaponType;
    [SerializeField] Transform weaponHolder;

    [SerializeField] Transform attackPoint;

    public float delayAttackTimer;
    float attackRange = 5.5f;
    public float AttackRange() => attackRange;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit() 
    {
        isDead = false;
        sightCharacter.ClearCInRange();
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
        isDead = true;
        ChangeAnim(Constain.ANIM_DEAD);
    }

    public virtual void SetSize(float size)
    {

    } 
        
    public virtual void UpSize(float increment)
    {

    }  
    
    public virtual void Attack()
    {
        if(target.isDead)
        {
            sightCharacter.RemoveCInRange(target);
            return;
        }    
        transform.LookAt(target.transform);
        ChangeAnim(Constain.ANIM_ATTACK);
        StartCoroutine(ThrowWeapon(curWeaponType));
    }

    IEnumerator ThrowWeapon(WeaponType weaponType)
    {
        yield return Cache.GetWFS(.4f);
        if (!isMoving && !isDead)
        {
            if(Target != null)
            {
                transform.LookAt(target.transform);
                BulletBase B = SimplePool.Spawn<BulletBase>((PoolType)DataManager.ins.dt.idWeapon, attackPoint.position, attackPoint.rotation);
                B.SetOwner(this);
            }    
        }
        yield return new WaitForSeconds(.4f);
    } 

    public void RemoveCharacterInRange(Character C)
    {
        sightCharacter.RemoveCInRange(C);
    }

    public bool IsDead
    {
        get { return isDead; }
        private set { isDead = value; }
    }
}
