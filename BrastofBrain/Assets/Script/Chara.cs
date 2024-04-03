using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャラクターをデータベース管理するために作成したもの

[SerializeField]
[CreateAssetMenu(fileName="Chara",menuName="CreatChara")]

public class Chara : ScriptableObject{

    // キャラクターの属性を定義
    public int id;          // 番号
    public string name;     // 名前
    public float defense;   // 防御力
    public float speed;     // 移動速度
    public float jumppow;   // ジャンプ力
    public string info;     // 情報
    public Sprite sprite;   // スプライト

    // コンストラクタ
    public Chara(Chara chara)
    {
        // 引数で渡されたキャラクターの情報をコピーする
        this.info = chara.info;
        this.name = chara.name;
        this.id = chara.id;
        this.sprite = chara.sprite;
        this.defense = chara.defense;
        this.jumppow = chara.jumppow;
        this.speed = chara.speed;
    }
}