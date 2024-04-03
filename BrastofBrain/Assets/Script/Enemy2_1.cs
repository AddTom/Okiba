using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_1 : MonoBehaviour
{
    public float moveSpeed = 2f; // 横方向の移動速度
    public float initialFallSpeed = 5f; // 初期の落下速度
    public float bounceForce = 5f; // バウンド時の上向きの力

    private Rigidbody2D rb2D; // Rigidbody2Dコンポーネントへの参照
    private bool isBouncing = false; // バウンド中かどうかを管理するフラグ
    public Transform player; // プレイヤーの位置情報

    void Start()
    {
        player = GameObject.Find("Player").transform; // プレイヤーのTransformコンポーネントを取得
        rb2D = GetComponent<Rigidbody2D>(); // Rigidbody2Dコンポーネントを取得
        rb2D.velocity = new Vector2(moveSpeed, -initialFallSpeed); // 初期の速度を設定
    }

    void Update()
    {
        if (!isBouncing)
        {
            Move(); // バウンド中でない場合は通常の移動を行う
        }
    }

    void Move()
    {
        Vector2 movement = new Vector2(moveSpeed, rb2D.velocity.y); // 横方向の速度を設定
        rb2D.velocity = movement; // Rigidbody2Dに速度を設定
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトがプレイヤーであれば、敵キャラクターを破壊する
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
        // 衝突したオブジェクトが地面であれば、バウンドを実行する
        if (collision.gameObject.CompareTag("isGround"))
        {
            Bounce();
        }
    }

    void Bounce()
    {
        // 上向きの力を加えてバウンドさせる
        rb2D.velocity = new Vector2(0f, bounceForce);
        isBouncing = true; // バウンド中のフラグを立てる
        Invoke("ResumeMovement", 0.5f); // 0.5秒後に移動を再開する
    }

    void ResumeMovement()
    {
        isBouncing = false; // バウンド終了
        Move(); // 移動を再開
    }
}