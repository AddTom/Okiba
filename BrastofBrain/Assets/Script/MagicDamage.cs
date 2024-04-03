using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MagicDamage : MonoBehaviourPunCallbacks
{
    public int MyID { get; private set; } // 自身のIDを保持するプロパティ
    public int IDA; // ダメージを受けた相手のIDを保持する変数
    private float pow1 = 45; // ダメージの基礎値

    // プレイヤーのIDを取得するメソッド
    public void getID(int ID)
    {
        MyID = ID; // 自身のIDを設定
        IDA = MyID; // ダメージを受けた相手のIDに自身のIDを設定
        Debug.Log(IDA); // デバッグログにダメージを受けた相手のIDを出力
    }

    // ダメージを与えるメソッド
    public void Damage(Collider2D other, float pow)
    {
        IDdamage damageable = other.gameObject.GetComponent<IDdamage>(); // プレイヤーのダメージ計算式を取得
        if (damageable != null)
        {
            damageable.Damagea(pow1); // プレイヤーにダメージを与える
        }
    }
}
