using System.Collections;
using Pixel_Adventure_1.Scripts;
using UnityEngine;

public class BananaAmmo : FruitAmmoBase
{
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(BackRotate());
    }

    public IEnumerator BackRotate()
    {
        yield return new WaitForSecondsRealtime(1.7f);
        AudioManager.I.Shot("Whoosh");
        m_Rigidbody2D.velocity = -m_Rigidbody2D.velocity;
    }
}