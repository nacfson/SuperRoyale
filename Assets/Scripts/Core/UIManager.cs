using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>, IInstanceable
{
    [SerializeField] private List<Canvas> _canvasList = new List<Canvas>();

    public void CreateInstance()
    {
        foreach(Canvas canvas in _canvasList)
        {
            for(int i =0; i < canvas.transform.childCount; i++)
            {
                if(canvas.transform.GetChild(i).TryGetComponent(out UIComponent component))
                {
                    component.Init();
                }
            }
        }
    }
}
