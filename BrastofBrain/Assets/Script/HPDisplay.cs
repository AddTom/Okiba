using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HPバーが同じ向きを向く様子にするためのスクリプト

public class HPDisplay : MonoBehaviour
{
    public GameObject Player;// プレイヤーキャラクターの参照を保持する変数
   


    // Update is called once per frame
    void Update()
    {
        // HPバーの位置を更新する
         Transform transform=this.transform;// HPバーのTransformコンポーネントを取得
         Vector3 pos =transform.position;// HPバーの現在の位置を取得

         // プレイヤーキャラクターの回転を判別してHPバーの位置を更新
        if(Player.transform.rotation.y>=-180f){

         // プレイヤーキャラクターが180度未満回転している場合、HPバーを左側に配置
            this.transform.position = new Vector2(Player.transform.position.x - 1.0f, Player.transform.position.y + 2f);
        }
        else if (Player.transform.rotation.y <= 0f)
        {
            // プレイヤーキャラクターが0度以下回転している場合、HPバーを右側に配置
            this.transform.position = new Vector2(Player.transform.position.x + 1.0f, Player.transform.position.y + 2f);
        }
    }
}







