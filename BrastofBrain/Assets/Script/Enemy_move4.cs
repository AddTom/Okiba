using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_move4 : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D;
    public Transform target;
    public float moveSpeed = 5f;

    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>(); // Rigidbody2Dを取得
        target = GameObject.Find("Player").transform; // Playerのtransformを保存
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // 目標の方向を計算
            Vector3 direction = target.position - transform.position;
            direction.Normalize(); // ベクトルを正規化

            // Rigidbody2Dを使用して物理演算を適用
            m_rigidbody2D.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          Destroy (this.gameObject);
        }
    }
}
