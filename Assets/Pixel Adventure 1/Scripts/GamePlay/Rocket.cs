using System;
using System.Collections;
using System.Collections.Generic;
using GameTool;
using Pixel_Adventure_1.Scripts;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rb2D;
    [SerializeField] private float speed;
    [SerializeField] private float timeExplode = 1f;

    private void Update()
    {
        rb2D.velocity = transform.up * speed;
        if (target)
        {
            float angle = Vector2.Angle(Vector2.up, target.position - transform.position);
            if ((target.position - transform.position).x > 0)
            {
                angle = -angle;
            }

            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 135);
        }
        else
        {
            target = Character.I.transform;
        }
    }

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
            AudioManager.I.Shot("Explosion");
            PoolingManager.I.GetObject(ePooling.FruitCollected, transform.position, Quaternion.identity);
        }
    }

    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(timeExplode);
        gameObject.SetActive(false);
        AudioManager.I.Shot("Explosion");
        PoolingManager.I.GetObject(ePooling.FruitCollected, transform.position, Quaternion.identity);
    }

    public void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }
}