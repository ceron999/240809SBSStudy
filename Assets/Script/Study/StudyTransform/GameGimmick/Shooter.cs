using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bulletInstance = Instantiate(bullet, this.transform.position, Quaternion.identity);

        bulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * bulletSpeed);
    }
}
