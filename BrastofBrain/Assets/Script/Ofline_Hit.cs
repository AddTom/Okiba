using UnityEngine;

public class Ofline_Hit : MonoBehaviour
{
    public bool flag = false; // ヒットフラグ
    public bool Cflag = false; // カウンターフラグ
    public float pow = 10f; // ダメージ量

    public Collider2D MyCollider; // 自身のコライダー
    public Collider2D otherCollider = null; // 衝突した相手のコライダー

    // Start is called before the first frame update
    void Start()
    {
        MyCollider = GetComponent<Collider2D>(); // 自身のコライダーを取得
        Debug.Log(MyCollider);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        otherCollider = other; // 衝突した相手のコライダーを保存
        if (MyCollider != otherCollider)
        {
            if (Shot.caun == true)
            {
                Cflag = true; // カウンターフラグを立てる
            }
            else
            {
                flag = true; // ヒットフラグを立てる
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            if (Offline_Shot.caun == true)
            {
                Cflag = true; // カウンターフラグを立てる
            }
            else
            {
                Enem_Damage enemDamageInstance = new Enem_Damage();
                enemDamageInstance.Damage(collision, pow); // ダメージを与える
                flag = true; // ヒットフラグを立てる
                Destroy(collision.gameObject); // 衝突した相手を破棄
            }
        }
    }
}

         
