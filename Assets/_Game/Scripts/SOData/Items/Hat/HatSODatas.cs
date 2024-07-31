using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HatSODatas", menuName = "Scriptable Objects/HatSODatas")]

public class HatSODatas : ScriptableObject
{
    [SerializeField] HatSO[] hatDatas;

    public HatBase GetPrefab(HatType type)
    {
        for (int i = 0; i < hatDatas.Length; i++)
        {
            if (hatDatas[i].hatType == type)
            {
                return hatDatas[i].hatPrefab;
            }
        }
        return null;
    }

    public int GetPrice(HatType type)
    {
        for (int i = 0; i < hatDatas.Length; i++)
        {
            if (hatDatas[i].hatType == type)
            {
                return hatDatas[i].itemPrice;
            }
        }
        return -1;
    }
}
public enum HatType
{
    None = 0,
    Arrow = 10,
    Cowboy = 20,
    Crown = 30,
    Ear = 40,
    Hat = 50,
    Hat_cap = 60,
    Hat_yellow = 70,
    Headphone = 80,
    Horn = 90,
    Rau = 100
}


