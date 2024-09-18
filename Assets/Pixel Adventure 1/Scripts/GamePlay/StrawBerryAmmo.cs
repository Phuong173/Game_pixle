using System.Collections;
using System.Collections.Generic;
using GameTool;
using Pixel_Adventure_1.Scripts;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class StrawBerryAmmo : FruitAmmoBase
{
    
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Detached());
    }
    
    
    public IEnumerator Detached()
    {
        yield return new WaitForSecondsRealtime(1f);
        StrawberryChild strawBerryChild1 = (StrawberryChild)PoolingManager.I.GetObject(ePooling.StrawberryChild, transform.position, transform.rotation);
        StrawberryChild strawBerryChild2 = (StrawberryChild)PoolingManager.I.GetObject(ePooling.StrawberryChild, transform.position, transform.rotation);
        AudioManager.I.Shot("Explosion");
        Vector2 vct1, vct2;
        if (transform.rotation.y == 0)
        {
            vct1 = new Vector2(0.7f, 0.3f);
            vct2 = new Vector2(0.7f, -0.3f);
        }
        else
        {
            vct1 = new Vector2(-0.7f, 0.3f);
            vct2 = new Vector2(-0.7f, -0.3f);
        }
        
        vct1 = vct1.normalized;
        vct2 = vct2.normalized;
        strawBerryChild1.m_Rigidbody2D.velocity = vct1 * 10f;
        strawBerryChild2.m_Rigidbody2D.velocity = vct2 * 10f;
    }
}
