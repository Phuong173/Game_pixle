using System;
using System.Collections;
using GameTool;
using Pixel_Adventure_1.Scripts.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pixel_Adventure_1.Scripts
{
    public class Fruit : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb2d;

        public int ID
        {
            get => m_ID;
            set => m_ID = value;
        }

        [SerializeField] private int m_ID;

        public void AddForce()
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            Vector3 force = new Vector3(Random.Range(-10f, 10f), 1);
            rb2d.AddForce(force.normalized * 10, ForceMode2D.Impulse);
        }

        IEnumerator ToStatic()
        {
            yield return new WaitForSeconds(2f);
            rb2d.bodyType = RigidbodyType2D.Static;
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PoolingManager.I.GetObject(ePooling.FruitCollected, transform.position, Quaternion.identity);
                AudioManager.I.Shot("Bonus");
                if (AmmoManagerMenu.Exists())
                {
                    FruitItem _item = FireController.I.listFruit.Find(item => item.m_ID == ID);
                    _item.amount += 3;
                    AmmoManagerMenu.I.UpdateAmmo();
                }

                gameObject.SetActive(false);
            }
        }
    }
}