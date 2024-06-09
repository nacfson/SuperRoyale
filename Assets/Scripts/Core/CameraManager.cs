using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private CinemachineVirtualCamera _cvCam;
    public CinemachineVirtualCamera VCam
    {
        get
        {
            if(_cvCam == null)
            {
                _cvCam = FindObjectOfType<CinemachineVirtualCamera>();
            }
            return _cvCam;
        }
    }

    public void SetFollowTarget(Transform target)
    {
        VCam.Follow = target;
    }
}
