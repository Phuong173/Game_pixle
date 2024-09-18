using System;
using System.Collections;
using System.Collections.Generic;
using GameTool;
using Pixel_Adventure_1.Scripts;
using Unity.Mathematics;
using UnityEngine;

public class ShiedController : MonoBehaviour,ITakeDamageable
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(Character.I.transform.rotation);
            if (Character.I.transform.rotation == Quaternion.Euler(1, 0, 0))
            {
                Character.I.transform.position -= new Vector3(2, 0, 0);
            }
            else if(Character.I.transform.rotation == Quaternion.Euler(0, -180, 0))
            {
                Character.I.transform.position += new Vector3(2, 0, 0);
            }
            TakeDamage(5);
        }
        else if (other.gameObject.CompareTag("Fruit"))
        {
            other.gameObject.GetComponent<FruitAmmoBase>().Disable();
        }
    }


    private void OnEnable()
    {
        StartCoroutine(nameof(CloseWave));
    }

    public IEnumerator CloseWave()
    {
        yield return new WaitForSecondsRealtime(20);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }
}
