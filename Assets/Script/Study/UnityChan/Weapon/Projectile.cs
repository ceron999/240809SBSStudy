using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime; // �Ѿ��� Ȱ��ȭ �ǰ�, ����ִ� ���� �ð�[���� �ð�]
    public float speed; // �Ѿ��� �ӵ�


    private void Start()
    {
        Destroy(gameObject, lifeTime); // ���� GameObject�� LifeTime ���Ŀ� �ı� �ǵ��� ����
    }

    private void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO : �浹 �� � ������Ʈ�� �ε��ƴ°�?
        // => ����/�Ǵ�
        //  => �Ϲ� ������Ʈ�̸� ����Ʈ�� ��� || ĳ���� ������Ʈ�̸� �������� �ش� + ����Ʈ ���
        Debug.Log(
                "<color=yellow> Bullet Impact !! </color> " +
                $"name : <color=red>{collision.gameObject.name}</color>");
        if(collision.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            Quaternion rotation = Quaternion.LookRotation(collision.contacts[0].normal);
            EffectManager.Instance.CreateEffect(EffectType.Impact_Wood, collision.contacts[0].point, rotation);

            if(collision.transform.root.TryGetComponent(out IDamage damageInterface))
            {
                damageInterface.ApplyDamage(10);
            }
        }
        else    // ĳ���Ͱ� �ƴ� �Ϳ� �ε����� ��
        {
            EffectType targetEffectType = EffectType.Impact_Dirt;
            Vector3 position = collision.contacts[0].point;
            Quaternion rotation = Quaternion.LookRotation(collision.contacts[0].normal);

            if (collision.collider.material.name.Contains("Wood"))
            {
                targetEffectType = EffectType.Impact_Wood;
            }
            else if (collision.collider.material.name.Contains("Metal"))
            {
                targetEffectType = EffectType.Impact_Metal;
            }
            else if (collision.collider.material.name.Contains("Concrete"))
            {
                targetEffectType = EffectType.Impact_Concrete;
            }
            EffectManager.Instance.CreateEffect(targetEffectType, collision.contacts[0].point, rotation);
        }

        Destroy(gameObject);
    }
}