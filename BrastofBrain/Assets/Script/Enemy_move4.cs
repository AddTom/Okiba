using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_move4 : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D; // Rigidbody2Dコンポーネントへの参照
    public Transform target; // 目標となるプレイヤーのTransformコンポーネント
    public float moveSpeed = 5f; // 移動速度

    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>(); // 自身のGameObjectからRigidbody2Dコンポーネントを取得
        target = GameObject.Find("Player").transform; // プレイヤーのTransformコンポーネントを取得
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // 目標の方向を計算
            Vector3 direction = target.position - transform.position;
            direction.Normalize(); // ベクトルを正規化

            // 敵キャラクターの速度を設定してプレイヤーに向かって移動する
            m_rigidbody2D.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトがプレイヤーであれば、敵キャラクターを破壊する
        if (collision.gameObject.CompareTag("Player"))
        {
          Destroy (this.gameObject);
        }
    }
}
