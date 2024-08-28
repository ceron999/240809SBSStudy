using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick : MonoBehaviour
{
    public Transform gimmickParent;

    public GameObject originalPrefab;

    public Vector3 spawnPositionAsLocal;

    private void Start()
    {
        gimmickParent = GameObject.Find("GimmickParent").transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlayerRoot")
        {
            GameObject instancePrefab = Instantiate(originalPrefab, transform.position + spawnPositionAsLocal, Quaternion.identity);
            instancePrefab.transform.SetParent(gimmickParent);
        }
    }
}
