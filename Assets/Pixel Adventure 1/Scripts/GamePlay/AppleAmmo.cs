using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pixel_Adventure_1.Scripts;
using UnityEngine;

public class AppleAmmo : FruitAmmoBase
{
    [SerializeField] private Collider2D collider;
    [SerializeField] private float radius;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!collider.enabled)
        {
            return;
        }
        collider.enabled = false;
        m_Rigidbody2D.velocity = Vector2.zero;
        
        transform.DOScale(3f, 0.25f).OnComplete(() =>
        {
            var cols = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (var col in cols)
            {
                if (col.CompareTag("Monster"))
                {
                    MonsterBase monster = other.GetComponent<MonsterBase>();
                    if (monster)
                    {
                        monster.TakeDamage(70);
                    }
                }
            }
            AudioManager.I.Shot("Explosion");
            Disable();
        });
    }

    public override void Disable()
    {
        base.Disable();
        transform.localScale = Vector3.one;
        collider.enabled = true;
    }
}
