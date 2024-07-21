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

    private IState<Enemy> currentState;

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
        ChangeState(new IdleState());
    }

    public override void Move()
    {
        ChangeAnim(Constain.ANIM_RUN);
        transform.LookAt(targetMove);
        agent.SetDestination(targetMove);
        if (Vector3.Distance(transform.position, targetMove) < .1f)
        {
            ChangeState(new IdleState());
        }
    }

    public void GetCharacterNearest()
    {
        SetTargetCharacter(sightCharacter.GetNearestTarget(this.transform));
    }

    public override void Attack()
    {
        base.Attack();
        Debug.Log("attack");
    }
}
