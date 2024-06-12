using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>, IInstanceable
{
    [SerializeField] private List<Canvas> _canvasList = new List<Canvas>();
    private List<UIComponent> _components = new List<UIComponent>();

    public void CreateInstance()
    {
        foreach(Canvas canvas in _canvasList)
        {
            for(int i =0; i < canvas.transform.childCount; i++)
            {
                if(canvas.transform.GetChild(i).TryGetComponent(out UIComponent component))
                {
                    component.Init();
                    _components.Add(component);
                }
            }
        }

    }

    public UIComponent GetUIComponent<T>() where T : UIComponent
    {
        foreach (UIComponent component in _components)
        {
            if(component is T com)
            {
                return com;
            }
        }
        return null;
    }

    public void AppearUI(UIComponent uiComponent)
    {
        uiComponent.Appear();
    }

    public void DisappearUI(UIComponent uiComponent)
    {
        uiComponent.Disappear();
    }
}
