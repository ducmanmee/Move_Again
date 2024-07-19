using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public float rangeAttack;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constain.TAG_PLAYER) || other.CompareTag(Constain.TAG_PLAYER))
        {
            Character target = Cache.GetCharacter(other);

        }    
    }
}
