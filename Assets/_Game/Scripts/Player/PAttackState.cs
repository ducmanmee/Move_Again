using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAttackState : IState<Player>
{
    public void OnEnter(Player t)
    {
        t.Attack();
        t.canAttack = false;
    }

    public void OnExecute(Player t)
    {
        t.delayAttackTimer += Time.deltaTime;
        if (t.delayAttackTimer > 1f)
        {
            t.ChangeState(Player.IdleStateP);
        }
    }

    public void OnExit(Player t)
    {

    }
}
