using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSight : Singleton<CharacterSight>
{
    public List<Character> targets = new List<Character>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constain.TAG_ENEMY) || other.CompareTag(Constain.TAG_PLAYER))
        {
            Character character = Cache.GetCharacter(other);
            if(!character.IsDead)
            {
                targets.Add(character);
            }    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constain.TAG_ENEMY) || other.CompareTag(Constain.TAG_PLAYER))
        {
            Character character = Cache.GetCharacter(other);
            RemoveCInRange(character);
        }
    }

    public Character GetNearestTarget(Transform TF)
    {
        Character nearestTarget = null;
        float minDistance = float.MaxValue;

        foreach (Character target in targets)
        {
            float distance = Vector3.Distance(TF.position, target.CharacterTF().position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTarget = target;
            }
        }

        return nearestTarget;
    }

    public void RemoveCInRange(Character character)
    {
        targets.Remove(character);
    }

    public void ClearCInRange()
    {
        targets.Clear();
    }    
}
