using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileDamage;
    public float lifeTime; // 총알이 활성화 되고, 살아있는 지속 시간[제한 시간]
    public float speed; // 총알의 속도


    private void Start()
    {
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * speed, ForceMode.Impulse);
        Destroy(gameObject, lifeTime); // 본인 GameObject를 LifeTime 이후에 파괴 되도록 명령
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO : 충돌 시 어떤 오브젝트에 부딪쳤는가?
        // => 구분/판단
        //  => 일반 오브젝트이면 이펙트만 출력 || 캐릭터 오브젝트이면 데미지를 준다 + 이펙트 출력
        Debug.Log(
                "<color=yellow> Bullet Impact !! </color> " +
                $"name : <color=red>{collision.gameObject.name}</color>");
        if(collision.gameObject.layer == LayerMask.NameToLayer("HitScanner"))
        {
            Quaternion rotation = Quaternion.LookRotation(collision.contacts[0].normal);
            //EffectManager.Instance.CreateEffect(EffectType.Impact_Wood, collision.contacts[0].point, rotation);

            if(collision.transform.root.TryGetComponent(out IDamage damageInterface))
            {
                float damageMultiple = 1f;
                if(collision.gameObject.TryGetComponent(out DamageMultiflier multiplier))
                {
                    damageMultiple = multiplier.DamageMultiplier;
                }
                damageInterface.ApplyDamage(10);
            }
        }
        else    // 캐릭터가 아닌 것에 부딛혔을 때
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
