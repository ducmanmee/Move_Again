using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;

public class AttackState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        t.Attack();
    }

    public void OnExecute(Enemy t)
    {
        t.delayAttackState += Time.deltaTime;
        if(t.delayAttackState > 2f)
        {
            t.ChangeState(Enemy.IdleStateE);
        } 
    }

    public void OnExit(Enemy t)
    {

    }
}
