using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
   public float gravity = 9.8f; // 重力の強さ

    private Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        if (rigidbody2D == null)
        {
            Debug.Log("Rigidbody2Dコンポーネントが見つかりません。");
        }
    }

    void FixedUpdate()
    {
        ApplyGravity();
    }

    void ApplyGravity()
    {
        if (rigidbody2D != null)
        {
            // 重力ベクトルを計算し、物体に適用
            Vector2 gravityVector = new Vector2(0, -gravity);
            rigidbody2D.AddForce(gravityVector, ForceMode2D.Force);
        }
    }
}