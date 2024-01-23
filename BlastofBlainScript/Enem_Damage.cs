using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enem_Damage : MonoBehaviour
{
    //[SerializeField] private EnemyDataBase enemyDataBase;//データベース呼び出し
    Enemy enemy;        //保存用
    public float life;
    private float pow1=45;
    
    // Start is called before the first frame update
    void Start()
    {
        //enemy=Enemy.besic1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void Damage(Collision2D other,float pow)
    {
          
          IDdamage damageable = other.gameObject.GetComponent<IDdamage>();//プレイヤーのダメージ計算式を取得
          if(damageable != null)
          {
               damageable.Damagea(pow);
                Destroy (this.gameObject);
               
          }    
          

    }

}
