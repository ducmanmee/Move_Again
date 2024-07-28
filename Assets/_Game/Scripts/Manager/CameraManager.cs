using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager ins;
    public Transform player;   
    public Vector3 offset;
    public float smooth;
    Transform cameraTF;
    public Camera cam;
    Vector3 velocity = Vector3.zero;

    public Vector3 menuPos;
    public Vector3 gamePlayPos;

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
        cameraTF = transform;
        gamePlayPos = offset;
    }

    void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = cameraTF.position - player.position;
        }
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        cameraTF.position = Vector3.SmoothDamp(cameraTF.position, desiredPosition, ref velocity, smooth);
    } 

    public void SetCamMainMenu()
    {
        offset = menuPos;
    }

    public void SetCamGamePlay()
    {
        offset = gamePlayPos;
    }
}
