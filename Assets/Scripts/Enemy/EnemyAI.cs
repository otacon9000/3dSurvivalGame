using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamageable
{

    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }


    private CharacterController _enemyCC;

    private Vector3 _velocity = Vector3.zero;
    [Header("Enemy Settings")]
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _minHealth;
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _gravity = 20f;
    [SerializeField]
    private int _enemyDamage = 1;
    [SerializeField]
    private float _attackDelay = 1.0f;
    private float _nextAttack;


    public IDamageable AttackTarget { get; set; }
    [SerializeField]
    private EnemyState _currentState = EnemyState.Chase;

    //only for test
    public GameObject target;

    public int Health { get; set; }

    private void Start()
    {
        _enemyCC = GetComponent<CharacterController>();
        Health = _maxHealth;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                CalculateMovement();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            default:
                break;
        }
    }

    void CalculateMovement()
    {
        if (_enemyCC.isGrounded)
        {
            if (target != null)
            {
                Vector3 direction = target.transform.position - transform.position;
                direction.Normalize();
                direction.y = 0;
                _velocity = direction * _speed;
                //transform.rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _speed);
            }
            else
            {
                return;
            }

        }
       
        _velocity.y -= _gravity * Time.deltaTime;
        _enemyCC.Move(_velocity * Time.deltaTime);

    }

    void Attack()
    {
        if (AttackTarget != null)
        {
            if (Time.time > _nextAttack)
            {
                AttackTarget.Damage(_enemyDamage);
                _nextAttack = Time.time + _attackDelay;
            }
        }
    }

    public void Damage(int damageAmount)
    {
        Health-=damageAmount;

        if (Health < _minHealth)
        {
            Debug.Log(gameObject.name + " is dead");
            Destroy(this.gameObject);
        }
    }

    public void StartAttack()
    {
        _currentState = EnemyState.Attack;
    }

    public void StopAttack()
    {
        _currentState = EnemyState.Chase;
    }




}

