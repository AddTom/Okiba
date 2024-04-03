using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//オンラインで魔法が当たった時の処理
public class Hit : MonoBehaviourPunCallbacks
{
    public int MyId { get; private set; }// 自身のIDを保持する変数
    public  bool flag=false;// ダメージが与えられたかどうかを示すフラグ
    public  bool Cflag=false;// 衝突があったかどうかを示すフラグ
    float pow=10f; // ダメージの量
    
    private Collider2D MyCollider;// 自身のCollider2Dコンポーネントへの参照
    public Collider2D otherCollider=null; // 衝突した相手のCollider2Dコンポーネントへの参照

    public GameObject Pearent= default; // ダメージを与える対象の親オブジェクトへの参照
     
    
    // Start is called before the first frame update
    void Start()
    {
         MyCollider = GetComponent<Collider2D>();// 自身のCollider2Dコンポーネントを取得
        Debug.Log(MyCollider); // デバッグログに自身のCollider2Dコンポーネントを出力
        
    }


    void OnTriggerEnter2D(Collider2D other) {
   
        otherCollider=other;// 衝突した相手のCollider2Dコンポーネントを取得
        // 自身がローカルプレイヤーの場合
        if (photonView.IsMine) {
             // 衝突した相手がMagicDamageコンポーネントを持っている場合
            if(other.TryGetComponent<MagicDamage>(out var MagicDamage)){
                     // 衝突した相手のIDが自身のIDと異なる場合
                if (MagicDamage.IDA!= PhotonNetwork.LocalPlayer.ActorNumber) {
                      // ショットが命中している場合
                    if(Shot.caun==true){
                        Cflag=true;// 衝突フラグをtrueに設定
                    }
                    else{
                        MagicDamage.Damage(MyCollider, pow); // 相手にダメージを与える
                        flag = true; // ダメージフラグをtrueに設定
                       
                    }
                    
                 }   
            }
           
        }
         
    }


}
