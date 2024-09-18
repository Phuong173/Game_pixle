using System.Collections.Generic;
using GameTool;
using Pixel_Adventure_1.DataSTO;
using UnityEngine;

namespace Pixel_Adventure_1.Scripts
{
    public class FruitAmmoBase : MonoBehaviour
    {
        [SerializeField] protected float damage = 30;
        public int ID
        {
            get => m_ID;
            set => m_ID = value;
        }

        [SerializeField] protected int m_ID;
        [SerializeField] protected EFruitType m_FruitType;

        public Rigidbody2D m_Rigidbody2D;
        protected Transform m_FirePoint;

        private void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_FirePoint = Character.I.m_FirePoint;
        }

        protected virtual void OnEnable()
        {
            Invoke(nameof(Disable), 5f);
            SetDirectionAndSpeed(10f);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Monster"))
            {
                MonsterBase monster = other.GetComponent<MonsterBase>();
                if (monster)
                {
                    monster.TakeDamage(damage);
                }
            }
            Disable();
        }

        public virtual void Disable()
        {
            if (gameObject.activeSelf)
            {
                CancelInvoke(nameof(Disable));
                gameObject.SetActive(false);
                PoolingManager.I.GetObject(ePooling.FruitCollected, transform.position, Quaternion.identity);
            }
        }
        
        public virtual void SetDirectionAndSpeed(float _speed)
        {
            var _transform = transform;
            _transform.rotation = m_FirePoint.rotation;
            _transform.position = m_FirePoint.position;
            var _right = _transform.right;
            Vector2 _sped = new Vector2(_right.x, _right.y) * _speed;
            m_Rigidbody2D.velocity = _sped;
        }
    }
}