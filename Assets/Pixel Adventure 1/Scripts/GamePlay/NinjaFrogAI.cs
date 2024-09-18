using System.Collections;
using System.Collections.Generic;
using GameTool;
using Pixel_Adventure_1.Scripts;
using UnityEngine;

public class NinjaFrogAI : MonsterBase
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
        yield return new WaitForSecondsRealtime(1);
        PoolingManager.I.GetObject(ePooling.EnemyRocket, firePoint.position, Quaternion.identity);
        yield return new WaitForSecondsRealtime(0.2f);
        PoolingManager.I.GetObject(ePooling.EnemyRocket, firePoint.position, Quaternion.identity);
        yield return new WaitForSecondsRealtime(0.2f);
        PoolingManager.I.GetObject(ePooling.EnemyRocket, firePoint.position, Quaternion.identity);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
}
