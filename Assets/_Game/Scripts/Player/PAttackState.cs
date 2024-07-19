using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAttackState : IState<Player>
{
    float timer;
    public void OnEnter(Player t)
    {
        t.Attack();
    }

    public void OnExecute(Player t)
    {
        
    }

    public void OnExit(Player t)
    {

    }
}
