using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//敵キャラクターをデータベース管理するために作成したもの
[CreateAssetMenu(fileName="Enemy",menuName="CreatEnemy")]
public class Enemy : ScriptableObject
{
    public int id;      // 敵キャラクターのID
    public string name; // 敵キャラクターの名前
    public float defense; // 敵キャラクターの防御力

    public float HP;   // 敵キャラクターの体力
    public float speed; // 敵キャラクターの移動速度
    public float jumppow; // 敵キャラクターのジャンプ力
    public string info; // 敵キャラクターの情報

    public GameObject sprite; // 敵キャラクターのスプライト

    // コンストラクター
    public Enemy(Enemy enemy)
    {
        this.HP = enemy.HP;
        this.info = enemy.info;
        this.name = enemy.name;
        this.id = enemy.id;
        this.defense = enemy.defense;
        this.jumppow = enemy.jumppow;
        this.speed = enemy.speed;
        this.sprite = enemy.sprite;
    }
}