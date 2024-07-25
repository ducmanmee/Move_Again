using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        t.targetMove = LevelManager.ins.GetPosEnemyMove(t.transform.position);
    }

    public void OnExecute(Enemy t)
    {
        t.Move();
    }

    public void OnExit(Enemy t)
    {

    }
}
