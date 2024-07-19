using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSODatas", menuName = "Scriptable Objects/WeaponSODatas")]

public class WeaponSODatas : ScriptableObject
{
    [SerializeField] WeaponSOData[] datas;

    public WeaponBase GetPrefab(WeaponType type)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i].weaponType == type)
            {
                return datas[i].weaponPrefab;
            }
        }
        return null;
    }

    public int GetWeaponCount() => datas.Length;

    public int GetPriceWeapon(WeaponType type)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i].weaponType == type)
            {
                return datas[i].priceWeapon;
            }
        }
        return -1;
    }
}

public enum WeaponType
{
    None = 0,
    Arrow = 1,
    Axe_Red = 2,
    Axe_Yellow = 3,
    Boomerang = 4,
    Candy_Watermelon = 5,
    Candy_Yellow = 6,
    Candy_IceCream = 7,
    Candy_Orange = 8,
    Hammer = 9,
    Knife = 10,
    Uzi = 11,
    Z = 12
}