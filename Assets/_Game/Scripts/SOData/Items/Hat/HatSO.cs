using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hat", menuName = "Scriptable Objects/Hats")]

public class HatSO : ItemsSO
{
    public HatBase hatPrefab;
    public HatType hatType;
}

