using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletBase : GameUnit
{
    float speed = 5.5f;
    Character owner;
    [SerializeField] private float duration = 1f;

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if(Vector3.Distance(owner.gameObject.transform.position, transform.position) > owner.AttackRange())
        {
            OnDespawn();
        }    
    }

    public void OnInit()
    {

    }   
    
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public void SetOwner(Character C)
    {
        owner = C;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constain.TAG_ENEMY) || other.CompareTag(Constain.TAG_PLAYER))
        {
            if(other.gameObject != owner.gameObject)
            {
                Character C = Cache.GetCharacter(other);
                if(C.IsDead) return;
                C.OnDeath();
                owner.RemoveCharacterInRange(C);
                if(C is Enemy)
                {
                    if (LevelManager.ins.RemainingEnemy == 0)
                    {
                        GameManager.ins.WinGame();
                    }
                    LevelManager.ins.SwarmEnemy();
                }    
                OnDespawn();
            }
        }    
    }

}
