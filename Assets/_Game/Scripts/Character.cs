using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private string currentAnim;
    public Animator animCharacter;
    Transform characterTF;
    bool isDead;
    
    public bool isMoving;

    int score;
    public TargetIndicator targetIndicator;


    private Character target;
    
    public CharacterSight sightCharacter;

    //Weapon
    [SerializeField] WeaponSODatas weaponSODatas;
    WeaponBase curWeapon;
    WeaponType curWeaponType;
    [SerializeField] Transform weaponHolder;


    //Hat
    [SerializeField] HatSODatas hatSODatas;
    HatBase curHat;
    HatType curHatType;
    [SerializeField] Transform hatHolder;

    //Pant
    [SerializeField] PantSODatas pantSODatas;
    [SerializeField] SkinnedMeshRenderer pantMaterialCharacter;
    PantType curPantType;

    //Shield
    [SerializeField] ShieldSODatas shieldSODatas;
    ShieldBase curShield;
    ShieldType curShieldType;
    [SerializeField] Transform shieldHolder;


    [SerializeField] Transform attackPoint;

    public float delayAttackTimer;
    float attackRange = 6.5f;

    string nameCharacter;

    public virtual void OnInit() 
    {
        isDead = false;
        characterTF = transform;
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

    public void ChangeHat(HatType hatType)
    {
        curHatType = hatType;
        if(curHat != null)
        {
            Destroy(curHat.gameObject);
        }
        if (curHatType == HatType.None) return;
        curHat = Instantiate(hatSODatas.GetPrefab(hatType), hatHolder);
    }  
    
    public void ChangePant(PantType pantType)
    {
        curPantType = pantType;
        if(curPantType == PantType.None)
        {
            pantMaterialCharacter.enabled = false;
        }    
        else
        {
            pantMaterialCharacter.enabled = true;
        }
        pantMaterialCharacter.material = pantSODatas.GetMat(pantType);
    }

    public void ChangeShield(ShieldType shieldType)
    {
        curShieldType = shieldType;
        if (curShield != null)
        {
            Destroy(curShield.gameObject);
        }
        if (curShieldType == ShieldType.None) return;
        curShield = Instantiate(shieldSODatas.GetPrefab(shieldType), shieldHolder);
    }

    public virtual void OnHit(int damage)
    {

    }    

    public virtual void OnDeath()
    {
        isDead = true;
        targetIndicator.OnDespawn();
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
        characterTF.LookAt(target.characterTF);
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
                characterTF.LookAt(target.characterTF);
                int poolIndex = (int)curWeaponType;
                BulletBase B = SimplePool.Spawn<BulletBase>((PoolType)poolIndex, attackPoint.position, attackPoint.rotation);
                B.SetOwner(this);
            }    
        }
        yield return new WaitForSeconds(.4f);
    } 

    public void RemoveCharacterInRange(Character C)
    {
        sightCharacter.RemoveCInRange(C);
    }

    public void SetRotation(Vector3 eulerAngles)
    {
        Quaternion newRotation = Quaternion.Euler(eulerAngles);
        characterTF.rotation = newRotation;
    }

    public void ActiveTargetIndicator()
    {
        targetIndicator = PoolingTargetIndicator.ins.SpawnFromPool(Constain.TAG_TARGET);
        targetIndicator.SetTarget(characterTF);
        targetIndicator.SetName(NameCharacter);
        targetIndicator.gameObject.SetActive(false);
    }

    public void HideTargetIndicator()
    {
        if (targetIndicator != null && !targetIndicator.gameObject.activeSelf && GameManager.IsState(GameState.GamePlay))
        {
            targetIndicator.gameObject.SetActive(true);
        }
    }

    public void UpScore(int index)
    {
        score += index;
        targetIndicator.SetScore(score);
    }    

    public bool IsDead
    {
        get { return isDead; }
        private set { isDead = value; }
    }

    public string NameCharacter
    {
        get { return nameCharacter; }
        set { nameCharacter = value; }
    }

    public WeaponType CurWeaponType
    {
        get { return curWeaponType; }
        set { curWeaponType = value; }
    }

    public Transform CharacterTF() => characterTF;
    public Character Target => target;
    public float AttackRange() => attackRange;
}
