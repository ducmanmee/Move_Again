using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        t.ChangeAnim(Constain.ANIM_IDLE);
        t.GetCharacterNearest();
        t.delayIdleState = 0f;
        t.delayAttackState = 0f;
    }

    public void OnExecute(Enemy t)
    {
        t.delayIdleState += Time.deltaTime;
        if(t.Target != null)
        {
            t.ChangeState(new AttackState());
        } 
        else 
        {
            if (t.delayIdleState > 3f)
            {
                t.ChangeState(new MoveState());
            }
        } 
    }

    public void OnExit(Enemy t)
    {
        
    }
}
