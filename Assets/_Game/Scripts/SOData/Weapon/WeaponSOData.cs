using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/Weapon")]

public class WeaponSOData : ScriptableObject
{
    public WeaponBase weaponPrefab;
    public WeaponType weaponType;

    [Header("Weapon Name")]
    public string weaponName;
    public int priceWeapon;

    [Header("Weapon Stats")]
    public int damage;
    public int speed;
}
