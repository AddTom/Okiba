using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove1 : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D; // Rigidbody2Dコンポーネントの参照
    public Transform target; // プレイヤーのTransform
    public float moveSpeed = 5f; // 移動速度

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーのTransformを取得
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // 目標の方向を計算
            Vector3 direction = target.position - transform.position;
            direction.Normalize(); // ベクトルを正規化

            // 目標の方向に移動
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}
