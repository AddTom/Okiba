using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove1 : MonoBehaviour
{

    private Rigidbody2D m_rigidbody2D;//
     public Transform target; // 追いかける対象のTransform
   public float moveSpeed = 5f;
     public float playerRotate;
    
    private float pow1=45;
   
    

    // Start is called before the first frame update
    void Start()
    {
       
         target = GameObject.Find("Player").transform;//Palyerのtransformを保存
        
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
