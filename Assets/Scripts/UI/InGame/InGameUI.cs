using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : UIComponent
{
    [SerializeField] private TMP_Text _bulletText;
    public override void Appear(Action Callback = null)
    {
        gameObject.SetActive(true);
    }

    public override void Disappear(Action Callback = null)
    {
        gameObject.SetActive(false);
    }

    public override void Init(params object[] param)
    {
        EventManager.AddListener<BulletCntEvent>(UpdateBulletUI);
    }

    private void UpdateBulletUI(BulletCntEvent cntEvent)
    {
        _bulletText.text = $"{cntEvent.currentCnt} / {cntEvent.maxCnt}";
    }
}
