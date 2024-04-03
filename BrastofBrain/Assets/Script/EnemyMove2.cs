using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove2 : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D; // Rigidbody2Dコンポーネントの参照
    public float maxSpeed = 10f; // 移動の最大速度
    private Transform target; // 追跡対象のTransform
    public float playerRotate; // プレイヤーの向き
    private int flag = 0; // 移動方向を切り替えるためのフラグ

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>(); // Rigidbody2Dの取得
        target = GameObject.Find("Player").transform; // プレイヤーのTransformを取得
        playerRotate = Rota.playerRotate; // プレイヤーの向きを取得
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == 0) // 初回のみ実行
        {
            if (playerRotate == 0) // プレイヤーが右を向いている場合
            {
                m_rigidbody2D.velocity = new Vector2(-maxSpeed, 0f); // 右方向に移動
                transform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0); // 180度回転させて逆向きにする
                flag = 1; // フラグを1にする
            }
            else // プレイヤーが左を向いている場合
            {
                m_rigidbody2D.velocity = new Vector2(maxSpeed, 0f); // 左方向に移動
                flag = -1; // フラグを-1にする
            }
        }

        // フラグに応じて移動方向を決定
        if (flag == 1)
            m_rigidbody2D.velocity = new Vector2(-maxSpeed, 0f); // 右方向に移動
        else if (flag == -1)
            m_rigidbody2D.velocity = new Vector2(maxSpeed, 0f); // 左方向に移動
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("isGround")) // 地面に衝突した場合
        {
            m_rigidbody2D.AddForce(Vector2.up * 30); // 上方向に力を加えてジャンプさせる
        }
    }
}