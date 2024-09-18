using System.Collections;
using System.Collections.Generic;
using GameTool;
using Pixel_Adventure_1.Scripts;
using UnityEngine;

public class CherriesAmmo : FruitAmmoBase
{
    public override void Disable()
    {
        AppleAmmo apl1, apl2;
        apl1 = (AppleAmmo)PoolingManager.I.GetObject(ePooling.AppleAmmo);
        apl2 = (AppleAmmo) PoolingManager.I.GetObject(ePooling.AppleAmmo);
        if (transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            apl1.transform.position = (Vector2) transform.position + Vector2.left;
            apl2.transform.position = (Vector2) transform.position + Vector2.left;
        }
        else
        {
            apl1.transform.position = (Vector2) transform.position + Vector2.right;
            apl2.transform.position = (Vector2) transform.position + Vector2.right;
        }
        
        AudioManager.I.Shot("Explosion");

        apl1.m_Rigidbody2D.velocity = Vector2.up * 10f;
        apl2.m_Rigidbody2D.velocity = Vector2.down * 10f;
        base.Disable();
    }
}
