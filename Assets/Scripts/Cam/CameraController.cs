﻿using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController: Singleton<CameraController>
{
    public CinemachineBrain brain;

    public CinemachineCamera cinemachineCamera;

    public GameObject moveCamera;
    public GameObject lockCamera;
    public GameObject facingRoomCamera;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        
        if (brain != null)
        {
            brain.enabled = false;
        }

        moveCamera.SetActive(true);
        lockCamera.SetActive(false);
        facingRoomCamera.SetActive(false);
    }


    public void SetWorldCamera(Transform camFollowTarget)
    {
        moveCamera.SetActive(true);
        lockCamera.SetActive(false);
        facingRoomCamera.SetActive(false);

        Debug.Log("Set World Camera");
        brain.enabled = true;

        cinemachineCamera.Follow = camFollowTarget.transform;
        
        // camFollow.target = camPosition;
        // camRotate.enabled = true;
        // camFollow.enabled = true;
    }
    
    

    public void SetGroupMeetingRoomCamera()  
    {
        //moveCamera.SetActive(false);
        lockCamera.SetActive(true);
        facingRoomCamera.SetActive(false);

        
        brain.enabled = false;
    }

    public void SetFacingRoomCamera()
    {
        moveCamera.SetActive(false);
        lockCamera.SetActive(false);
        facingRoomCamera.SetActive(true);
    }
}
