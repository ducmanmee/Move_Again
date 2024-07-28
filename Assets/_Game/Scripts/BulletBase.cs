using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletBase : GameUnit
{
    float speed = 5.5f;
    Character owner;
    Transform bulletTF;
    [SerializeField] private float duration = 1f;

    private void Awake()
    {
        bulletTF = transform;
    }

    private void Update()
    {
        bulletTF.Translate(Vector3.up * speed * Time.deltaTime);
        if(Vector3.Distance(owner.CharacterTF().position, bulletTF.position) > owner.AttackRange())
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
                owner.UpScore(1);
                if(C is Enemy)
                {
                    if (LevelManager.ins.RemainingEnemy == 0)
                    {
                        GameManager.ins.WinGame();
                    }
                    LevelManager.ins.SwarmEnemy();
                } 
                CanvasGameplay.ins.SetNumberText(LevelManager.ins.RemainingEnemy+ 1);
                OnDespawn();
            }
        }    
    }

}
