using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDeadState : IState<Player>
{
    public void OnEnter(Player t)
    {
        GameManager.ins.LoseGame();
    }

    public void OnExecute(Player t)
    {
        
    }

    public void OnExit(Player t)
    {
        
    }
}
