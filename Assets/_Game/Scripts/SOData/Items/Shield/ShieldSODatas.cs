using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldSODatas", menuName = "Scriptable Objects/ShieldSODatas")]

public class ShieldSODatas : ScriptableObject
{
    [SerializeField] ShieldSO[] shieldDatas;

    public ShieldBase GetPrefab(ShieldType type)
    {
        for (int i = 0; i < shieldDatas.Length; i++)
        {
            if (shieldDatas[i].shieldType == type)
            {
                return shieldDatas[i].shieldPrefab;
            }
        }
        return null;
    }
}
public enum ShieldType
{
    None = 0,
    America_Shield = 10,
    Batman_Shield = 20
}


