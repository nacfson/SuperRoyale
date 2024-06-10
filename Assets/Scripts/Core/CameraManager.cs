using Cinemachine;
using DG.Tweening;
using UnityEngine;
using System;

public class CameraManager : MonoSingleton<CameraManager>, IInstanceable
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

    private Camera _mainCam;
    public Camera MainCam
    {
        get
        {
            if(_mainCam == null)
            {
                _mainCam = FindObjectOfType<Camera>();
            }
            return _mainCam;
        }
    }

    private Vector2 _absolMousePos = new Vector3(1f,1f,1f); 
    [SerializeField] private float _aimingTime = 0.5f;

    public void CreateInstance()
    {
        GameManager.Instance.InputReader.OnMouseMoveEvent += SetMousePos;
    }

    public void SetFollowTarget(Transform target)
    {
        VCam.Follow = target;
    }

    public void ZoomCamera(float distance)
    {
        var framingTransposer = VCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        float originDistance = framingTransposer.m_CameraDistance;

        DOTween.To(() => originDistance, value => framingTransposer.m_CameraDistance = value, distance, _aimingTime);
    }

    public void SetCameraTrack(bool isAiming)
    {
        var framingTransposer = VCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        Vector3 start = framingTransposer.m_TrackedObjectOffset;
        Vector3 targetOffset = Vector3.zero;

        if (isAiming)
        {
            Vector3 centerOffset = MainCam.WorldToScreenPoint(Vector3.zero);
            Vector3 curMouseOffset = MainCam.WorldToScreenPoint(_absolMousePos); 

            targetOffset = (centerOffset - curMouseOffset).normalized * 3f;
        }
        DOTween.To(() => start, value => framingTransposer.m_TrackedObjectOffset = value, targetOffset, _aimingTime);
    }

    private void SetMousePos(Vector2 pos)
    {
        _absolMousePos = pos;
    }

    public Vector3 GetMousePos(int layer)
    {
        Ray ray = MainCam.ScreenPointToRay(_absolMousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer))
        {
            return hit.point;
        }
        return Vector3.one;
    }
}
