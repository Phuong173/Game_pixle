using System;
using GameTool;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pixel_Adventure_1.Scripts
{
    public class Character : SingletonMonoBehavior<Character>, ITakeDamageable
    {
        private static readonly int FSpeedY = Animator.StringToHash("f_SpeedY");
        private static readonly int TDoubleJump = Animator.StringToHash("t_DoubleJump");
        private static readonly int BRun = Animator.StringToHash("b_Run");
        private static readonly int THurt = Animator.StringToHash("t_Hurt");

        [SerializeField] private Transform m_CheckGroundPoint;
        [SerializeField] public Transform m_FirePoint;
        [SerializeField] private LayerMask m_CheckGroundLayerMask;
        
        [SerializeField] private float m_RunSpeed = 5;
        [SerializeField] private float m_JumpForce = 12f;
        [SerializeField] public float maxHealth = 100;
        [SerializeField] public float currentHealth;
        
        private Rigidbody2D m_Rigidbody2D;
        private Animator m_Animator;
        private CapsuleCollider2D m_CapsuleCollider2D;
        
        private bool m_UsedDoubleJump;
        private bool m_IsOnGround;
        private float m_CheckGroundRadius;

        protected override void Awake()
        {
            base.Awake();
            maxHealth = 100 + (GameData.I.CurrentLevel - 1) * 50;
            currentHealth = maxHealth;
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_Animator = GetComponent<Animator>();
            m_CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        }

        private void Start()
        {
            var _size = m_CapsuleCollider2D.size;
            m_CheckGroundPoint.position = transform.position + (Vector3) m_CapsuleCollider2D.offset +
                                          Vector3.down * _size.y / 2;
            m_CheckGroundRadius = _size.x / 2.5f;
        }

        private void Update()
        {
            m_Animator.SetFloat(FSpeedY, m_Rigidbody2D.velocity.y);

            if (Input.GetButtonDown("Jump"))
            {
                if (m_IsOnGround)
                {
                    Jump();
                }
                else if (!m_UsedDoubleJump)
                {
                    Jump();
                    m_UsedDoubleJump = true;
                    m_Animator.SetTrigger(TDoubleJump);
                }
            }
            else if (Input.GetButtonDown("SwipeDown"))
            {
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -3 * m_JumpForce);
            }
        }

        private void FixedUpdate()
        {
            Run();

            Collider2D _collider2D =
                Physics2D.OverlapCircle(m_CheckGroundPoint.position, m_CheckGroundRadius, m_CheckGroundLayerMask);

            m_IsOnGround = _collider2D;
            if (_collider2D)
            {
                m_IsOnGround = !_collider2D.isTrigger;
            }

            if (m_IsOnGround)
            {
                m_UsedDoubleJump = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Die"))
            {
                TakeDamage(10);
                m_Rigidbody2D.AddForce((transform.position- other.transform.position).normalized * 15, ForceMode2D.Impulse);
            }
        }

        public void Jump()
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
        }

        public void Run()
        {
            float _Horizontal = Input.GetAxisRaw("Horizontal");
            m_Rigidbody2D.velocity = new Vector2(_Horizontal * m_RunSpeed, m_Rigidbody2D.velocity.y);

            m_Animator.SetBool(BRun, _Horizontal != 0);

            if (_Horizontal > 0)
            {
                transform.eulerAngles = new Vector3(0, 0);
            }
            else if (_Horizontal < 0)
            {
                transform.eulerAngles = new Vector3(0, 180);
            }
        }

        public void TakeDamage(float damage)
        {
            m_Animator.SetTrigger(THurt);
            AudioManager.I.Shot("Hurt");
            if (!GameManager.Exists())
            {
                return;
            }
            currentHealth -= damage;
            this.PostEvent(eEventType.CharacterTakeDamage);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }

        private void Die()
        {
            AudioManager.I.Shot("Death");
            gameObject.SetActive(false);
            this.PostEvent(eEventType.CharacterDie);
        }
    }
}