using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : Character
{
    [SerializeField] NavMeshAgent agent;
    public float delayIdleState;
    public float delayAttackState;
    public Vector3 targetMove;

    public static IdleState IdleStateE = new IdleState();
    public static AttackState AttackStateE = new AttackState();
    public static MoveState MoveStateE = new MoveState();
    public static DeadState DeadStateE = new DeadState();

    private IState<Enemy> currentState;

    public override void OnDespawn()
    {
        PoolingEnemy.ins.EnQueueObj(Constain.TAG_ENEMY, this);
        targetIndicator.OnDespawn();
    }

    public override void OnDeath()
    {
        base.OnDeath();
        ChangeState(Enemy.DeadStateE);
        LevelManager.ins.RemainingEnemy--;   
    }

    public IEnumerator Dead()
    {
        yield return Cache.GetWFS(Constain.TIMER_DEAD);
        OnDespawn();
    }    

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        HideTargetIndicator();
    }

    public void ChangeState(IState<Enemy> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        CurWeaponType = GetRandomEnumValue<WeaponType>();
        ChangeWeapon(CurWeaponType);
        ChangePant(GetRandomEnumValue<PantType>());
        ChangeHat(GetRandomEnumValue<HatType>());
        ChangeShield(GetRandomEnumValue<ShieldType>());
        NameCharacter = NameUtility.GetRandomName();
        ActiveTargetIndicator();
        ChangeState(Enemy.IdleStateE);
    }  

    public override void Move()
    {
        if(!GameManager.IsState(GameState.GamePlay)) return;
        ChangeAnim(Constain.ANIM_RUN);
        CharacterTF().LookAt(targetMove);
        agent.SetDestination(targetMove);
        if (Vector3.Distance(CharacterTF().position, targetMove) < .2f)
        {
            ChangeState(Enemy.IdleStateE);
        }
    }

    public void GetCharacterNearest()
    {
        SetTargetCharacter(sightCharacter.GetNearestTarget(CharacterTF()));
    }

    public override void Attack()
    {
        base.Attack();  
    }

    T GetRandomEnumValue<T>() where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }
}
