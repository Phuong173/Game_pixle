using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pixel_Adventure_1.Scripts;
using UnityEngine;

public class KiwiAmmo : FruitAmmoBase
{
    protected override void OnEnable()
    {
        base.OnEnable();
        transform.DOScale(new Vector3(3, 3, 3), 2f);
    }

    public override void Disable()
    {
        AudioManager.I.Shot("Explosion");
        transform.DOKill();
        transform.localScale = Vector3.one;
        base.Disable();
    }
}
