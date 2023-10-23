using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform target;
    private int damage;

    public void Setup(Transform target, int damage)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target;                           //Ÿ���� �������� target
        this.damage = damage;                           //Ÿ���� �������� ���ݷ�
    }

    private void Update()
    {
        if ( target != null )
        {
            Vector3 direction = (target.position-transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        if ( collision.transform != target)

        //collision.GetComponent<Enemy>().OnDamage();               //�� ��� �Լ� ȣ��
        collision.GetComponent<EnemyHP>().TakeDamage(damage);       //�� ü���� damage��ŭ ����
        Destroy(gameObject);
    }
    
        
    }

    


