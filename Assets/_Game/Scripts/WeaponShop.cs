using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 2f; 
    [SerializeField] private float rotationAngle = 360f;  

    private void Start()
    {
        RotateAroundYAxis();
    }

    private void RotateAroundYAxis()
    {
        Vector3 rotationVector = new Vector3(0, rotationAngle, 0);

        transform.DORotate(rotationVector, rotationDuration, RotateMode.LocalAxisAdd)
                 .SetEase(Ease.Linear) 
                 .SetLoops(-1, LoopType.Restart); 
    }
}
