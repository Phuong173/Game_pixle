using System.Collections;
using System.Collections.Generic;
using Pixel_Adventure_1.Scripts;
using UnityEngine;

public class OrangeAmmo : FruitAmmoBase
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            MonsterBase monster = other.GetComponent<MonsterBase>();
            if (monster)
            {
                var tmp = monster.transform.position;
                monster.transform.position = Character.I.transform.position;
                Character.I.transform.position = tmp;
                monster.TakeDamage(damage);
            }
        }
        Disable();
        AudioManager.I.Shot("Spawn");
    }
}
