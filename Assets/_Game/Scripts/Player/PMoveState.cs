using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMoveState : IState<Player>
{
    public void OnEnter(Player t)
    {
        t.CharacterTF().rotation = Quaternion.LookRotation(t.movementDirection);
        t.ChangeAnim(Constain.ANIM_RUN);
        t.StopAllCoroutines();
        t.canAttack = true;
        t.delayAttackTimer = 0;
    }

    public void OnExecute(Player t)
    {
        t.CharacterTF().Translate(t.movementDirection * t.moveSpeed * Time.deltaTime, Space.World);
    }

    public void OnExit(Player t)
    {

    }
}
