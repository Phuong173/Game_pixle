using System.Collections;
using System.Collections.Generic;
using Pixel_Adventure_1.Scripts;
using UnityEngine;

public class MelonAmmo : FruitAmmoBase
{
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(SpeedUp());
    }

    public IEnumerator SpeedUp()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        AudioManager.I.Shot("Whoosh");
        m_Rigidbody2D.velocity *= 2;
    }
}
