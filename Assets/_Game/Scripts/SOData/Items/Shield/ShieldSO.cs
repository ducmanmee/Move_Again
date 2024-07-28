using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shield", menuName = "Scriptable Objects/Shields")]

public class ShieldSO : ItemsSO
{
    public ShieldBase shieldPrefab;
    public ShieldType shieldType;
}
