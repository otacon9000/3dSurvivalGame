using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private EnemyAI _ai;
    

    private void Start()
    {
        _ai = GetComponentInParent<EnemyAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _ai.StartAttack();
            _ai.AttackTarget = other.GetComponent<IDamageable>();

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _ai.StopAttack();

        }
    }
}
