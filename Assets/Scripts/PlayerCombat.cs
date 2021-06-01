using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;

    public LayerMask enemyLayers;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    void Attack()
    {
        //animator.SetTrigger("Attack");

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Debug.Log("Attacco");

        foreach (Collider2D item in enemiesHit)
        {
            Debug.Log("Ho colpito " + item.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
            Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
