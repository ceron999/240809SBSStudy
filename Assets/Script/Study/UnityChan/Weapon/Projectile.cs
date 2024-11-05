using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime; // 총알이 활성화 되고, 살아있는 지속 시간[제한 시간]
    public float speed; // 총알의 속도


    private void Start()
    {
        Destroy(gameObject, lifeTime); // 본인 GameObject를 LifeTime 이후에 파괴 되도록 명령
    }

    private void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO : 충돌 시 어떤 오브젝트에 부딪쳤는가?
        // => 구분/판단
        //  => 일반 오브젝트이면 이펙트만 출력 || 캐릭터 오브젝트이면 데미지를 준다 + 이펙트 출력
        Debug.Log(
                "<color=yellow> Bullet Impact !! </color> " +
                $"name : <color=red>{collision.gameObject.name}</color>");
        if(collision.gameObject.layer == 1<<10)
            Destroy(gameObject);
    }
}
