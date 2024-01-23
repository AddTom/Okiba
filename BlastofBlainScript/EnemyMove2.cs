using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove2 : MonoBehaviour
{
   private Rigidbody2D m_rigidbody2D;//
    public float maxSpeed = 10f;
    private Transform target;
     public float playerRotate;
    private int flag=0;
   
   
    

    // Start is called before the first frame update
    void Start()
    {
         m_rigidbody2D = GetComponent<Rigidbody2D>();//Rigidbody2Dの読み取り
         target = GameObject.Find("Player").transform;//Palyerのtransformを保存
         playerRotate=Rota.playerRotate;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flag==0)//1回だけ呼び出す
          {
               

               if(playerRotate==0)//Playerが右を向いているとき
               {
                     m_rigidbody2D.velocity = new Vector2(-maxSpeed, 0f);//右に移動
                     transform.rotation = transform.rotation * Quaternion.Euler (0, 180, 0);
                     flag=1;//flagを1に
               }
               else
               {
                     m_rigidbody2D.velocity = new Vector2(maxSpeed, 0f);//左に移動
                     flag=-1;//flagを-1に

               }
                    
            
          }

          if(flag==1)
                m_rigidbody2D.velocity = new Vector2(-maxSpeed, 0f);//Playerの向きに依存せず右に移動
          else if(flag==-1)
                m_rigidbody2D.velocity = new Vector2(maxSpeed, 0f);//Playerの向きに依存せず左に移動
            

    }
      private void OnTriggerEnter2D(Collider2D other) {
         Vector2 force=new Vector2(0,10);
         
          if(other.gameObject.CompareTag("isGround")){
             m_rigidbody2D.AddForce(Vector2.up * 30);
      
        }
        
          
    }
}
