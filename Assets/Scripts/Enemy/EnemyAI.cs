using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamageable
{
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
        CalculateMovement();
    }

    void CalculateMovement()
    {
        if (_enemyCC.isGrounded)
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.Normalize();
            direction.y = 0;
            _velocity = direction * _speed;
            transform.rotation = Quaternion.LookRotation(direction);
        }
       
        _velocity.y -= _gravity * Time.deltaTime;
        _enemyCC.Move(_velocity * Time.deltaTime);

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {   
            Debug.Log("collision with " + other.gameObject.name);
            IDamageable hit = other.transform.GetComponent<IDamageable>();

            if (hit != null)
            {
                hit.Damage(_enemyDamage);
            }
        }
    }

}

