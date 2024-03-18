using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_1 : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float initialFallSpeed = 5f; // 初期の落下速度
    public float bounceForce = 5f;

    private Rigidbody2D rb2D;
    private bool isBouncing = false;
     public Transform player;

    void Start()
    {
          player = GameObject.Find("Player").transform;//Palyerのtransformを保存
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(moveSpeed, -initialFallSpeed); // 初期の落下速度を設定
    }

    void Update()
    {
        if (!isBouncing)
        {
            Move();
        }
    }

    void Move()
    {
        Vector2 movement = new Vector2(moveSpeed, rb2D.velocity.y);
        rb2D.velocity = movement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          Destroy (this.gameObject);
        }
        if (collision.gameObject.CompareTag("isGround"))
        {
            Bounce();
        }
    }


    void Bounce()
    {
        rb2D.velocity = new Vector2(0f, bounceForce);
        isBouncing = true;
        Invoke("ResumeMovement", 0.5f); // 0.5秒後に移動を再開
    }

    void ResumeMovement()
    {
        isBouncing = false;
        Move();
    }
}