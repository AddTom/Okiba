using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D;//自身のRigidbody2Dを保存するためのもの
    public float maxSpeed = 10f;//壁の移動

    // Start is called before the first frame update
    void Start()
    {
         m_rigidbody2D = GetComponent<Rigidbody2D>();//自身のRigidbody2Dを保存
    }

    // Update is called once per frame
    void Update()
    {
        
            m_rigidbody2D.velocity = new Vector2(maxSpeed, m_rigidbody2D.velocity.y);//壁を移動させる
            

    }

    void OnCollisionEnter2D(Collision2D other)
     {

          //当たったオブジェクトをすべて消去する
           if(other.gameObject.CompareTag("Block"))
           {
                Destroy(other.gameObject);
           }
           if(other.gameObject.CompareTag("enemy"))
           {
                Destroy(other.gameObject);
           }
           if(other.gameObject.CompareTag("coin"))
           {
                Destroy(other.gameObject);
           }
          

     }
}
