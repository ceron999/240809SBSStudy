using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MineGimmick : MonoBehaviour
{
    public float damage;
    public float timer;
    public bool isCountDown;

    // ���� ��ü
    GameObject target;

    private void Update()
    {
        if (isCountDown)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                // To do : Explosion => Damage To Near Player
                ExplodeMine();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            isCountDown = true;
            timer = 3f;
            target = other.transform.root.gameObject;

            StartCoroutine(CountDown());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            isCountDown = false;
            timer = 3f;
            target = null;

            StopCoroutine(CountDown());
        }
    }

    IEnumerator CountDown()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        bool isColorful = false;

        // While ��(�ݺ���) : ( ) ���� ������ ���̸� { } ���� �ڵ带 ��� �ݺ��Ѵ�.
        while (timer > 0)
        {
            renderer.material.color = isColorful ? Color.red : Color.white;
            isColorful = !isColorful;

            yield return new WaitForSeconds(0.1f);
        }
    }

    void ExplodeMine()
    {
        if(target.CompareTag("Player"))
        {
            target.GetComponent<CharacterBase>().CurrHp -= damage;
            Debug.Log("Now Hp : " +  target.GetComponent<CharacterBase>().CurrHp);
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Player �ƴ�");
        }
    }
}
