using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomText : PoolableMono
{
    private TextMeshProUGUI _text;

    public override void Init()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        if(text.Length > 8) text = text.Substring(0,8);
        _text.SetText($"{text}");

        Debug.Log($"Text: {text}");
    }
}
