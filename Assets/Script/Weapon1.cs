using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon1 : MonoBehaviour
{
    public bool isAttack = false;//�Ƿ���Թ�����ȡ���ڷ�Χ�����޵���
    public bool isCooling = false;//������ȴ
    public float Range;

    public float AttackTimer = 0;//������ʱ��
    public Transform enemy;
    public float CD;
    public float originZ;//ԭʼz��
    public GameObject Dan;//�ӵ�
    public float DanSpeed;//����

    private void Awake()
    {

        originZ = transform.eulerAngles.z;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �Զ���׼
        Aiming();

        // �жϹ���
        // ������ȴ  
        if(isCooling)
        {
            AttackTimer += Time.deltaTime;
            if (AttackTimer >= CD)
            {
                AttackTimer = 0;
                isCooling = false;
            }
        }
        else
        {
            if(isAttack)
            {
                Fire();
            }
        }
    }

    private void Aiming()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position
            , Range, LayerMask.GetMask("Enemy"));

        if (enemiesInRange.Length > 0)
        {
            isAttack = true;
            Collider2D nearestEnemy = enemiesInRange
                .OrderBy(enemy => Vector2.Distance(transform.position, enemy.transform.position))
                .First();
            enemy = nearestEnemy.transform;

            Vector2 enemyPos = enemy.position;
            Vector2 direction = enemyPos - (Vector2)transform.position;
            float angleDegress = Mathf.Atan2 (direction.x, direction.y)*Mathf.Rad2Deg;
            
            transform.eulerAngles = 
                new Vector3(transform.eulerAngles.x,transform.eulerAngles.y, angleDegress+originZ);


        }
        else
        {
            isAttack = false;
            enemy = null;
            new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,originZ);
        }

    }

    public void Fire()
    {
        if (isCooling)
        {
            return;
        }

        if(Dan==null)
        {
            Debug.LogError("No Dan");
        }

        Dan=Instantiate(Dan,transform.position,transform.rotation);

        Rigidbody2D rb=Dan.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            Vector2 direction=(enemy.position-transform.position).normalized;
            rb.velocity = direction*DanSpeed;
        }

        isCooling = true;
    }

}
