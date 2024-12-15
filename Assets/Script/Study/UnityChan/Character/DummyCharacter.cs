using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCharacter : MonoBehaviour, IDamage
{
    public float health = 100;
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            animator.SetTrigger("Dead Trigger");
            Destroy(gameObject, 5f);
        }
    }
}