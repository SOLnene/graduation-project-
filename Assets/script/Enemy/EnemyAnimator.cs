using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    EnemyController enemyController;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyController = GetComponentInChildren<EnemyController>();
        enemyController.OnAttack += OnAttack;
    }
    
    void Update()
    {
        animator.SetFloat("Speed", enemyController.getSpeed());

    }
    void OnAttack()
    {
        animator.SetTrigger("Attack");
    }
}
