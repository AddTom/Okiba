using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enem_Damage : MonoBehaviour
{
    Enemy enemy;        // 保存用のEnemyクラスのインスタンス
    public float life;  // 敵キャラクターの体力
    private float pow1 = 45; // ダメージの基礎値

    // ダメージを受ける処理
    public void Damage(Collision2D other, float pow)
    {
        // ダメージを受けるオブジェクトがIDdamageインターフェースを持っているかをチェック
        IDdamage damageable = other.gameObject.GetComponent<IDdamage>();
        if (damageable != null)
        {
            // プレイヤーのダメージ計算式を取得し、ダメージを与える
            damageable.Damagea(pow);
            // 敵キャラクターを破壊
            Destroy(this.gameObject);
        }
    }
}
