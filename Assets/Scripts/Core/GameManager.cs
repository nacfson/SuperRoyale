using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoSingleton<GameManager>
{
    [field:SerializeField] public InputReader InputReader {get; private set; }
    private Camera _mainCam;
    public Vector3 CurrentMousePos { get; private set; }
    private void Awake()
    {
        _mainCam = Camera.main;

        InputReader.OnMouseMoveEvent += SetMousePos;


        List<IInstanceable> instanceList = GetComponentsInChildren<IInstanceable>().ToList();
        instanceList.ForEach(i => i.CreateInstance());
    }

    private void SetMousePos(Vector2 pos)
    {
        Ray ray = _mainCam.ScreenPointToRay(pos);
        if(Physics.Raycast(ray, out RaycastHit hit ,Mathf.Infinity))
        {
            CurrentMousePos = hit.point;
        }
    }
}
