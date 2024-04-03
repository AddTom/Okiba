using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_move3 : MonoBehaviour
{
    public Transform player;            // プレイヤーの位置情報を保持する変数
    public float moveSpeed = 3f;        // 移動速度
    public float attackRange = 5f;      // 攻撃範囲
    public GameObject projectilePrefab; // 攻撃時に発射する弾のプレハブ
    public float projectileSpeed = 10f; // 弾の速度
    public float attackCooldown = 1f;   // 攻撃のクールタイム

    private float lastAttackTime;       // 最後に攻撃した時間

    void Start()
    {
        player = GameObject.Find("Player").transform; // プレイヤーの位置情報を取得
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
        // プレイヤーの方向を向く
        Vector3 direction = player.position - transform.position;
        direction.Normalize(); // ベクトルを正規化

        // 目標の方向に移動
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    bool CanAttack()
    {
        // 前回の攻撃からクールタイムを経過しているかを判定
        return (Time.time - lastAttackTime) > attackCooldown;
    }

    void Attack()
    {
        // 攻撃を発射
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = transform.right * projectileSpeed;

        // クールダウンの更新
        lastAttackTime = Time.time;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーと衝突した場合、自身を破壊する
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}