using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameTool;
using Pixel_Adventure_1.Scripts;
using UnityEngine;

public class BombVitualGuyController : MonoBehaviour, ITakeDamageable
{
    public Rigidbody2D rb2D;
    [SerializeField] private float speed;
    [SerializeField] private float timeExplode = 1f;


    private void OnEnable()
    {
        StartCoroutine(nameof(Explode));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Character charac = other.gameObject.GetComponent<Character>();
            if (charac)
            {
                charac.TakeDamage(20);
            }
        }

        if (!other.gameObject.CompareTag("Monster"))
        {
            gameObject.SetActive(false);
        }
    }

    public IEnumerator Explode()
    {
        rb2D.velocity = (Character.I.transform.position - transform.position).normalized * speed;
        yield return new WaitForSeconds(timeExplode);
        gameObject.SetActive(false);
        PoolingManager.I.GetObject(ePooling.FruitCollected, transform.position, Quaternion.identity);
    }

    public void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }
}