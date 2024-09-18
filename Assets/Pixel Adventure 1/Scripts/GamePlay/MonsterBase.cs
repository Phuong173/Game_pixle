using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameTool;
using Pixel_Adventure_1.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBase : MonoBehaviour, ITakeDamageable
{
    protected readonly WaitForSecondsRealtime wait2s = new WaitForSecondsRealtime(0.5f);
    
    [SerializeField] protected float m_RunSpeed;
    [SerializeField] protected float m_MaxHealth = 100;
    [SerializeField] protected float m_CurentHealth = 100;

    [SerializeField] protected Image currentHealthImg;
    [SerializeField] protected Transform m_CheckGroundPoint;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected LayerMask m_CheckGroundLayerMask;

    protected float m_CheckGroundRadius;
    protected float m_JumpForce = 12f;
    protected bool m_IsOnGround;
    protected bool isDead;
    
    protected Rigidbody2D m_Rigidbody2D;
    protected Animator m_Animator;
    protected CapsuleCollider2D capsuleCollider2D;
    
    protected static readonly int FSpeedY = Animator.StringToHash("f_SpeedY");
    protected static readonly int TDoubleJump = Animator.StringToHash("t_DoubleJump");
    protected static readonly int BRun = Animator.StringToHash("b_Run");
    private static readonly int THurt = Animator.StringToHash("t_Hurt");

    protected eStateMonster runState = eStateMonster.DontRun;
    protected eStateMonster jumpState = eStateMonster.DontJump;


    protected IEnumerator SetStateRoutine()
    {
        while (true)
        {
            SetState();
            yield return wait2s;
        }
    }

    protected void SetState()
    {
        runState = (eStateMonster) Random.Range((int) eStateMonster.Right, (int) eStateMonster.DontRun + 1);
        jumpState = (eStateMonster) Random.Range((int) eStateMonster.Jump, (int) eStateMonster.DontJump + 1);

        switch (runState)
        {
            case eStateMonster.Right:
            {
                m_Rigidbody2D.velocity = new Vector2(m_RunSpeed, m_Rigidbody2D.velocity.y);
                m_Animator.SetBool(BRun, true);
                transform.eulerAngles = new Vector3(0, 0);
                break;
            }
            case eStateMonster.Left:
            {
                m_Rigidbody2D.velocity = new Vector2(-m_RunSpeed, m_Rigidbody2D.velocity.y);
                m_Animator.SetBool(BRun, true);
                transform.eulerAngles = new Vector3(0, 180);
                break;
            }
            case eStateMonster.DontRun:
            {
                m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
                m_Animator.SetBool(BRun, false);
                break;
            }
        }

        switch (jumpState)
        {
            case eStateMonster.Jump:
            {
                Jump();
                break;
            }
            case eStateMonster.DoubleJump:
            {
                Jump();
                Invoke(nameof(Jump), 0.3f);
                break;
            }
            case eStateMonster.DontJump:
            {
                break;
            }
            case eStateMonster.JumpDown:
            {
                Jump();
                Invoke(nameof(SwipeDown), 0.3f);
                break;
            }
        }
    }

    protected virtual void Awake()
    {
        m_CurentHealth = m_MaxHealth;
        isDead = false;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        var _size = capsuleCollider2D.size;
        m_CheckGroundPoint.position = transform.position + (Vector3) capsuleCollider2D.offset +
                                      Vector3.down * _size.y / 2;
        m_CheckGroundRadius = _size.x / 2;
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(nameof(SetStateRoutine));
    }

    protected virtual void Update()
    {
        m_Animator.SetFloat(FSpeedY, m_Rigidbody2D.velocity.y);
    }


    private void FixedUpdate()
    {
        Collider2D _collider2D =
            Physics2D.OverlapCircle(m_CheckGroundPoint.position, m_CheckGroundRadius, m_CheckGroundLayerMask);

        m_IsOnGround = _collider2D;
        if (_collider2D)
        {
            m_IsOnGround = !_collider2D.isTrigger;
        }
    }

    public void SwipeDown()
    {
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -3 * m_JumpForce);
    }

    public void Jump()
    {
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
        if (!m_IsOnGround)
        {
            m_Animator.SetTrigger(TDoubleJump);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }
        
        m_Animator.SetTrigger(THurt);
        AudioManager.I.Shot("Hurt");
        m_CurentHealth -= damage;
        currentHealthImg.DOFillAmount(m_CurentHealth / m_MaxHealth, 0.25f);
        currentHealthImg.DOColor(Color.Lerp(Color.red, Color.green, m_CurentHealth / m_MaxHealth) , 0.25f);

        if (m_CurentHealth <= 0)
        {
            isDead = true;
            DropFruit();
            AudioManager.I.Shot("Death");
            gameObject.SetActive(false);
            this.PostEvent(eEventType.MonsterDie);
        }
    }

    public virtual void DropFruit()
    {
        for (int i = 0; i < 3; i++)
        {
            int id = GameData.I.ListFruitUse[Random.Range(0, GameData.I.ListFruitUse.Count)].ID;
            var item = (Fruit)PoolingManager.I.GetObject(GameData.I.StoFruitsData.FruitItemData[id].PoolingNameFruit, transform.position, Quaternion.identity);
            item.AddForce();
        }
    }
}


public enum eStateMonster
{
    Right,
    Left,
    DontRun,
    Jump,
    DoubleJump,
    JumpDown,
    DontJump,
}