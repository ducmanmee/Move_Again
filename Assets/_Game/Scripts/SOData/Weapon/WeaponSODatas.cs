using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSODatas", menuName = "Scriptable Objects/WeaponSODatas")]

public class WeaponSODatas : ScriptableObject
{
    [SerializeField] WeaponSOData[] weaponDatas;

    public WeaponBase GetPrefab(WeaponType type)
    {
        for (int i = 0; i < weaponDatas.Length; i++)
        {
            if (weaponDatas[i].weaponType == type)
            {
                return weaponDatas[i].weaponPrefab;
            }
        }
        return null;
    }

    public WeaponShop GetPrefabShop(WeaponType type)
    {
        for (int i = 0; i < weaponDatas.Length; i++)
        {
            if (weaponDatas[i].weaponType == type)
            {
                return weaponDatas[i].weaponShop;
            }
        }
        return null;
    }

    public int GetWeaponCount() => weaponDatas.Length;

    public int GetPriceWeapon(WeaponType type)
    {
        for (int i = 0; i < weaponDatas.Length; i++)
        {
            if (weaponDatas[i].weaponType == type)
            {
                return weaponDatas[i].priceWeapon;
            }
        }
        return -1;
    }
}

public enum WeaponType
{
    None = 0,
    Arrow = 10,
    Axe_Red = 20,
    Axe_Yellow = 30,
    Boomerang = 40,
    Candy_Watermelon = 50,
    Candy_Yellow = 60,
    Candy_IceCream = 70,
    Candy_Orange = 80,
    Hammer = 90,
    Knife = 100,
    Uzi = 110,
    Z = 120
}