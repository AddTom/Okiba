using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MagickDestory : MonoBehaviour
{
   

    // 他のCollider2Dとの衝突時に呼ばれるメソッド
    void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突した相手のタグが"attack"であり、相手のオブジェクトが存在する場合
        if (other.gameObject.tag == "attack" && other.gameObject != null)
        {
            // 相手のオブジェクトを破棄する
            Destroy(other.gameObject);
        }
    }
}
