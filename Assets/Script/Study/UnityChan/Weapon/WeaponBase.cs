using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public int RemainAmmo => currentAmmo;

    public Projectile bulletPrefab; // �Ѿ� ������ ��ü - �߻� �� => ������ �Ѿ��� ���� GameObject
    public Transform firePoint; // �Ѿ� �߻� ��ġ+������ �ǹ��ϴ� Transform

    public float weaponDamage;
    public float fireRate; // ���� �ӵ� (�ð� ��) => ex) 0.1: 0.1�ʿ� 1�߾� �߻� �� �� �ִ� ��
    public int clipSize; // źâ ũ��

    private float lastFireTime; // ������ �߻� ���� �ð�
    private int currentAmmo; // ���� źâ�� ���� �Ѿ� ��
    public float spread = 1f;

    private void Awake()
    {
        bulletPrefab.projectileDamage = weaponDamage;
        currentAmmo = clipSize;
        fireRate = Mathf.Max(fireRate, 0.1f); // ���� �ӵ��� 0.1���� �۴ٸ�, 0.1�� ����
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

        // Muzzle effect ���
        EffectManager.Instance.CreateEffect(EffectType.Muzzle_01, firePoint.position,firePoint.rotation);

        return true;
    }

    public void Reload()
    {
        currentAmmo = clipSize;
    }
}
