using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        t.ChangeAnim(Constain.ANIM_IDLE);
        t.delayIdleState = 0f;
        t.delayAttackState = 0f;
    }

    public void OnExecute(Enemy t)
    {
        t.GetCharacterNearest();
        t.delayIdleState += Time.deltaTime;
        if(t.Target != null)
        {
            t.ChangeState(Enemy.AttackStateE);
        } 
        else 
        {
            if (t.delayIdleState > 3f)
            {
                t.ChangeState(Enemy.MoveStateE);
            }
        } 
    }

    public void OnExit(Enemy t)
    {
        
    }
}
