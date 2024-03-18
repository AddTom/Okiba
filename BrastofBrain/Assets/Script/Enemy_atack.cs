using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_atack : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    private Rigidbody2D rb;
   


    void Start()
    {
         rb = GetComponent<Rigidbody2D>();

        player = GameObject.Find("Player").transform; // Playerのtransformを保存

        if (player != null)
        {
            // 自機の方向を向く
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = targetRotation;
        }
    }

    void FixedUpdate()
    {
        // 自機の方向に進む
        Vector2 movement = transform.right * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
}