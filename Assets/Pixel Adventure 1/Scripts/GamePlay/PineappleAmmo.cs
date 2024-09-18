using System.Collections;
using System.Collections.Generic;
using Pixel_Adventure_1.Scripts;
using UnityEngine;

public class PineappleAmmo : FruitAmmoBase
{
   public override void Disable()
   {
      base.Disable();
      AudioManager.I.Shot("Explosion");
   }
}
