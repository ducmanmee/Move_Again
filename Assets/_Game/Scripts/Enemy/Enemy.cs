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
        /*ChangeAnim(Constain.ANIM_RUN);

        agent.SetDestination(Player.ins.transform.position);
        transform.LookAt(Player.ins.transform.position);*/
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
        ChangeState(Enemy.IdleStateE);
    }

    public override void Move()
    {
        ChangeAnim(Constain.ANIM_RUN);
        transform.LookAt(targetMove);
        agent.SetDestination(targetMove);
        if (Vector3.Distance(transform.position, targetMove) < .2f)
        {
            ChangeState(Enemy.IdleStateE);
        }
    }

    public void GetCharacterNearest()
    {
        SetTargetCharacter(sightCharacter.GetNearestTarget(this.transform));
    }

    public override void Attack()
    {
        base.Attack();  
    }
}
