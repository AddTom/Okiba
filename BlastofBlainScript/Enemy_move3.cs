using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_move3 : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float attackRange = 5f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float attackCooldown = 1f;

    private float lastAttackTime;

 void Start()
    {

         player = GameObject.Find("Player").transform;//Palyerのtransformを保存
         
        
    }

      void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            // プレイヤーに近づく
            MoveTowardsPlayer();
        }
        else if (CanAttack())
        {
            // プレイヤーが攻撃範囲内にいるなら攻撃する
            Attack();
        }
    }

    void MoveTowardsPlayer()
    {
        // // プレイヤーの方向を向く
        // Vector3 direction = player.position - transform.position;
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

         Vector3 direction = player.position - transform.position;
            direction.Normalize(); // ベクトルを正規化

            // 目標の方向に移動
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            
    
    }

    bool CanAttack()
    {
        return (Time.time - lastAttackTime) > attackCooldown;
    }

    void Attack()
    {
        // プレイヤーの方向を向く
        // Vector3 direction = player.position - transform.position;
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // 攻撃を発射
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = transform.right * projectileSpeed;

        // クールダウンの更新
        lastAttackTime = Time.time;
    }

      void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          Destroy (this.gameObject);
        }
    }
}