using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager ins;
    public Transform player;   
    public Vector3 offset;
    public float smooth;
    Vector3 velocity = Vector3.zero;

    private void MakeInstance()
    {
        if(ins == null)
        {
            ins = this;
        }    
    }

    private void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smooth);
    } 
}
