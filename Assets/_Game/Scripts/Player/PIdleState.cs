using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIdleState : IState<Player>
{
    public void OnEnter(Player t)
    {
        t.ChangeAnim(Constain.ANIM_IDLE);
        if(t.Target != null)
        {
            t.transform.LookAt(t.Target.transform);
        }    
    }

    public void OnExecute(Player t)
    {

    }

    public void OnExit(Player t)
    {
        
    }
}
