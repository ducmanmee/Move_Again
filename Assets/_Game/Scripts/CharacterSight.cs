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
            targets.Add(character);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constain.TAG_ENEMY) || other.CompareTag(Constain.TAG_PLAYER))
        {
            Character character = Cache.GetCharacter(other);
            targets.Remove(character);
        }
    }

    public Character GetNearestTarget(Transform playerTransform)
    {
        Character nearestTarget = null;
        float minDistance = float.MaxValue;

        foreach (Character target in targets)
        {
            float distance = Vector3.Distance(playerTransform.position, target.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTarget = target;
            }
        }

        return nearestTarget;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 4.3f);
    }
}
