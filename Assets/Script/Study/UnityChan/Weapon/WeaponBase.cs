using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public int RemainAmmo => currentAmmo;

    public Projectile bulletPrefab; // 총알 프리팹 객체 - 발사 시 => 복사할 총알의 원본 GameObject
    public Transform firePoint; // 총알 발사 위치+방향을 의미하는 Transform

    public float weaponDamage;
    public float fireRate; // 연사 속도 (시간 값) => ex) 0.1: 0.1초에 1발씩 발사 할 수 있는 값
    public int clipSize; // 탄창 크기

    private float lastFireTime; // 마지막 발사 실제 시간
    private int currentAmmo; // 현재 탄창의 남은 총알 수
    public float spread = 1f;

    private void Awake()
    {
        bulletPrefab.projectileDamage = weaponDamage;
        currentAmmo = clipSize;
        fireRate = Mathf.Max(fireRate, 0.1f); // 연사 속도가 0.1보다 작다면, 0.1로 설정
    }

    public bool Fire()
    {
        if (currentAmmo <= 0 || Time.time - lastFireTime < fireRate)
            return false;

        Vector2 randomSpread = Random.insideUnitSphere;
        Vector2 spreadRotaion = randomSpread * spread;  

        Projectile bullet = Instantiate(bulletPrefab, firePoint.position,
            firePoint.rotation * Quaternion.Euler(spreadRotaion.x, 0f, spreadRotaion.y));
        bullet.gameObject.SetActive(true);
        lastFireTime = Time.time;
        currentAmmo--;

        // Muzzle effect 출력
        EffectManager.Instance.CreateEffect(EffectType.Muzzle_01, firePoint.position,firePoint.rotation);

        return true;
    }

    public void Reload()
    {
        currentAmmo = clipSize;
    }
}
