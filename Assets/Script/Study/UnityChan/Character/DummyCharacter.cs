using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCharacter : MonoBehaviour, IDamage
{
    public float health = 100;
    public Animator animator;
    public Collider rootCollider;

    public Collider[] ragdollColliders;
    public Rigidbody[] ragdollRigidbodies;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rootCollider = GetComponent<Collider>();

        Transform hipTransform = animator.GetBoneTransform(HumanBodyBones.Hips);
        ragdollColliders = hipTransform.GetComponentsInChildren<Collider>();
        ragdollRigidbodies = hipTransform.GetComponentsInChildren<Rigidbody>();
    }

    public void SetActiveRagdoll(bool isActive)
    {
        animator.enabled = !isActive;
        for(int i = 0; i< ragdollRigidbodies.Length; i++)
        {
            ragdollRigidbodies[i].isKinematic = !isActive;
        }
        if(isActive)
        {
            rootCollider.enabled = false;
        }
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            SetActiveRagdoll(true);
            //animator.SetTrigger("Dead Trigger");
            //Destroy(gameObject, 5f);
        }
    }
}