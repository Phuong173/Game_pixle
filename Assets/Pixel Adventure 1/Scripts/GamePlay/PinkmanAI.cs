using System.Collections;
using System.Collections.Generic;
using GameTool;
using Pixel_Adventure_1.Scripts;
using UnityEngine;

public class PinkmanAI : MonsterBase
{
    public LayerMask LayerMask;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(nameof(Shoot));
    }

    public IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(3f);
            StartCoroutine(nameof(DropBombs));
        }
    }

    public IEnumerator DropBombs()
    {
        var hit = Physics2D.Raycast(transform.position, Character.I.transform.position - transform.position, (Character.I.transform.position - transform.position).magnitude,
            LayerMask);
        if (hit.collider) yield break;
        Rocket rocket = (Rocket) PoolingManager.I.GetObject(ePooling.EnemyRocket, firePoint.position, Quaternion.identity);
        rocket.target = Character.I.transform;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
}
