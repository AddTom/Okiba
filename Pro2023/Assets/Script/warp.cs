using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class warp : MonoBehaviour
{
     public GameObject obj; //ワープ先
     private Vector2 wrp; //ワープ先の座標
      private Transform target;//ワープ対象
       private Transform Block;//ブロック
        private Transform enemy;//敵
 
    


    // Start is called before the first frame update
    void Start()
    {
        wrp=obj.transform.position;//ワープ先の座標の保存
       
         
    }

     // Update is called once per frame
    void Update()
    {
           target = GameObject.Find("wall").transform;//壁のトランスフォームを保存
          // Block=GameObject.FindWithTag("Block").transform;
         //  enemy=GameObject.FindWithTag("enemy").transform;
    }


    void OnCollisionEnter2D(Collision2D other)
     {
           Vector2 a= other.gameObject.transform.position;
           other.gameObject.transform.position=new Vector2(wrp.x,a.y);
           if(other.gameObject.CompareTag("Player"))//プレイヤーが触れると指定された座標にワープさせる
           {
               target.transform.position = new Vector3((a.x-target.position.x), 9.6f, 0f);//プレイヤーをワープさせる
               //Block.transform.position = new Vector3((other.gameObject.transform.position.x+Block.position.x), Block.position.y, 0f);
                //enemy.transform.position = new Vector3((other.gameObject.transform.position.x+enemy.position.x), enemy.position.y, 0f);
           }
          

     }
    

   
    

}
