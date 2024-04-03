using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack : MonoBehaviour
{
    public Transform player; // プレイヤーの位置情報を保持するためのTransform変数
    public float speed = 5f; // 移動速度
    private Rigidbody2D rb; // 敵キャラクターの Rigidbody2D コンポーネントへの参照

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D コンポーネントを取得

        // プレイヤーの位置情報を取得
        player = GameObject.Find("Player").transform;

        // プレイヤーが存在する場合
        if (player != null)
        {
            // 自機の方向を向く
            Vector3 direction = player.position - transform.position; // プレイヤーの方向を計算
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 自機の方向を計算
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward); // 回転角度を変換
            transform.rotation = targetRotation; // 敵キャラクターの向きを変更
        }
    }

    void FixedUpdate()
    {
        // 自機の方向に進む
        Vector2 movement = transform.right * speed * Time.fixedDeltaTime; // 移動ベクトルを計算
        rb.MovePosition(rb.position + movement); // Rigidbody2D を使用して移動する
    }
}