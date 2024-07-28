using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pant", menuName = "Scriptable Objects/Pants")]

public class PantSO : ItemsSO
{
    public Material pantMat;
    public PantType pantType;
}
