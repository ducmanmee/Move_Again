using UnityEngine;
using UnityEngine.AI;

public class NavMeshChecker : Singleton<NavMeshChecker>
{
    public float maxDistance = .1f;
    public bool IsPointOnNavMesh(Vector3 point)
    {
        NavMeshHit hit;
        bool isOnNavMesh = NavMesh.SamplePosition(point, out hit, maxDistance, NavMesh.AllAreas);
        return isOnNavMesh;
    }

    /*void Update()
    {
        Vector3 pointToCheck = transform.position;

        if (IsPointOnNavMesh(pointToCheck))
        {
            Debug.Log("Point is on NavMesh.");
        }
        else
        {
            Debug.Log("Point is NOT on NavMesh.");
        }
    }*/
}