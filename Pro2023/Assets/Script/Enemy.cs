using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // 変数の定義
    private Transform target;
    public GameObject[] enemy;
    private float time=0f;
    private Vector2 hafe=new Vector2(0.5f,0.5f);

    // シーン開始時に一度だけ呼ばれる関数
    void Start(){
        // 変数にPlayerオブジェクトのtransformコンポーネントを代入
        target = GameObject.Find("Player").transform;
    }
 
    // シーン中にフレーム毎に呼ばれる関数
    void Update () {
        // カメラのx座標をPlayerオブジェクトのx座標から取得y座標とz座標は現在の状態を維持
        //transform.position = new Vector3(target.position.x+20, transform.position.y, transform.position.z);
        time=time+Time.deltaTime;

        //敵とブロックをランダム生成
        if(time>0.6f&&target!=null){
            float x=Random.Range(target.position.x+20,target.position.x+60);
            float y=Random.Range(0f,7.5f);
            int r=Random.Range(0,enemy.Length);
            Instantiate(enemy[r],new Vector2(x,y),Quaternion.identity);
            time=0f;
        }

    }
}
